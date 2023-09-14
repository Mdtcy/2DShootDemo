using System;
using System.Collections.Generic;
using Animancer;
using Damages;
using DefaultNamespace.GameMain.Scripts.HpBar;
using LWShootDemo.Entities;
using LWShootDemo.Entities.Player;
using UnityEngine;
using UnityEngine.Assertions;
using UnityGameFramework.Runtime;

namespace GameMain
{
    public class Character : EntityLogicBase
    {
        public BuffComponent Buff;
        protected MovementComponent _movementComponent;
        private CharacterFeedBackComponent _characterFeedBackComponent;
        private NumericComponent _numericComponent;
        public UnitBindManager UnitBindManager;
        private FaceController _faceController;

        // private bool _canMove = true;
        public AnimancerComponent CachedAnimancer { get; private set; }
        
        // 是否死亡
        private bool isDead;
        
        /// <summary>
        /// 是否死亡
        /// </summary>z
        public bool IsDead => isDead;
        
        ///<summary>
        ///角色的无敌状态持续时间，如果在无敌状态中，子弹不会碰撞，DamageInfo处理无效化
        ///单位：秒
        ///</summary>
        public float ImmuneTime
        {
            get => _immuneTime;
            set => _immuneTime = Mathf.Max(_immuneTime, value);
        }
        private float _immuneTime = 0.00f;

        /// <summary>
        /// 死亡事件
        /// </summary>
        public Action ActOnDeath;

        private Side _side;
        public Side Side => _side;

        public Action<int> ActOnHpChanged;

        public float Speed => _numericComponent.GetAsInt(NumericType.Speed);
        
        public float Atk => _numericComponent.GetAsInt(NumericType.Attack);
        
        // 最大血量
        public int MaxHp => _numericComponent.GetAsInt(NumericType.MaxHp);
        
        public float HpRegen => _numericComponent.GetAsFloat(NumericType.HpRegen);

        // 当前血量
        public int CurHp
        {
            get => _numericComponent.GetAsInt(NumericType.Hp);
            set
            {
                if (!value.Equals(CurHp))
                {
                    _numericComponent.Set(NumericType.Hp, value);
                    ActOnHpChanged?.Invoke(value);
                    _hpBar.UpdateProgress(value, MaxHp);
                }
            }
        }

        public void UpdateAttr(NumericType numericType, float value)
        {
            if (numericType < NumericType.Max)
            {
                Log.Error("UpdateAttr error, numericType < NumericType.Max");
                return;
            }

            if (numericType > NumericType.Float)
            {
                float curValue = _numericComponent.GetAsFloat(numericType);
                _numericComponent.Set(numericType, curValue + value);    
            }
            else
            {
                int curValue = _numericComponent.GetAsInt(numericType);
                _numericComponent.Set(numericType, curValue + (int)value);
            }

        }

        // /// <summary>
        // /// 每秒回复血量
        // /// </summary>
        // public int HpRegen => _numericComponent[IntNumericType.HpRegen];

        public Direction FaceDirection => _faceController.FaceDirection;
        
        // local
        // tick次数 每秒tick一次
        private int _tickTimes;
        private float _totalElapsedSeconds;
        
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);

            // todo 
            _hpBar = GetComponentInChildren<HpBar>();
            CachedAnimancer = GetComponent<AnimancerComponent>();
            Buff = GetComponent<BuffComponent>();
            _movementComponent = GetComponent<MovementComponent>();
            _characterFeedBackComponent = GetComponent<CharacterFeedBackComponent>();
            UnitBindManager = GetComponent<UnitBindManager>();
            _numericComponent = GetComponentInChildren<NumericComponent>();
            _numericComponent.Init(Entity);
            _faceController = GetComponent<FaceController>();
        }

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            
            var characterData = userData as CharacterData;
            var characterProp = GameEntry.TableConfig.Get<CharacterTable>().Get(characterData.PropID);
#if UNITY_EDITOR
            name = characterProp.DefaultName;      
#endif
            _side = characterProp.Side;
            
            isDead = false;
            // _canMove = true;
            _tickTimes = 0;
            
            // numeric
            foreach (var initNumeric in characterProp.InitNumerics)
            {
                UpdateAttr(initNumeric.Type, initNumeric.Value);
            }
            
            // buff
            foreach (var addbuffData in characterProp.InitBuffs)
            {
                AddBuff(new AddBuffInfo(addbuffData.Buff, 
                    null, 
                    gameObject,
                    stack: addbuffData.Stack, 
                    duration: addbuffData.IsPermanent? 10f: addbuffData.Duration, 
                    durationSetTo:true,
                    permanent: addbuffData.IsPermanent));
            }
            Assert.IsTrue(Speed > 0, "Speed > 0");
            Assert.IsTrue(CurHp > 0, "CurHp > 0");
            Assert.IsTrue(MaxHp > 0, "MaxHp > 0");

            // _movementComponent.SetSpeed(_numericComponent[NumericType.Speed]);
            _hpBar.UpdateImmeadiatly(CurHp, MaxHp);
            _characterFeedBackComponent.Play("Reset");
        }

        protected override void OnHide(bool isShutdown, object userData)
        {
            base.OnHide(isShutdown, userData);
            _hpBar.Hide();
            Buff.OnHide();
            StopMotionClip();// todo movement
            _numericComponent.OnHide();
        }

        protected HpBar _hpBar;

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
            Buff.UpdateBuff(elapseSeconds);

            _totalElapsedSeconds += elapseSeconds;
            if (_totalElapsedSeconds > _tickTimes + 1)
            {
                _tickTimes++;
                float hpRegen = Mathf.Min(HpRegen, MaxHp - CurHp);
                RecoverHp((int)hpRegen);
            }
        }
        
        public void RecoverHp(int hp)
        {
            hp = Mathf.Min(hp, MaxHp - CurHp);
            // Log.Debug("<color=#FF0000>hpRegen:</color> " + hpRegen);
            if (hp > 0)
            {
                GameEntry.Damage.DoDamage(null, 
                    this, (int)hp, Vector3.zero, 0, 
                    new List<DamageInfoTag>(){DamageInfoTag.directHeal},
                    new List<AddBuffInfo>());
            } 
        }
        
        
        public void AddBuff(AddBuffInfo addBuffInfo, bool forceNew = false)
        {
            Buff.AddBuff(addBuffInfo, forceNew);
        }

        public void TriggerBuff<TBuffEvent, TEventActArgs>(TEventActArgs args) where TBuffEvent : BuffEvent<TEventActArgs> where TEventActArgs : BaseBuffEventActArgs
        {
            Buff.TriggerEvent<TBuffEvent, TEventActArgs>(args);
        }

        public bool CanBeKilledByDamageInfo(DamageInfo damageInfo)
        {
            if (this.ImmuneTime > 0 || damageInfo.IsHeal() == true)
            {
                return false;
            }

            int dValue = damageInfo.DamageValue(false);
            // return dValue >= this.resource.hp;
            return dValue >= this.CurHp;
        }
        
        public virtual void TakeDamage(int damage)
        { 
            // ActOnHurt?.Invoke(damageInfo);
            CurHp -= damage;
            
            if (CurHp <= 0)
            {
                Death();
            }
        }
        
        // 死亡
        private void Death()
        {
            isDead = true;
            ActOnDeath?.Invoke();
        }
        
        public void PlayMotionClip(MotionClip clip)
        {
            _movementComponent.PlayMotionClip(clip);
        }

        public void PlayFeedBack(string name)
        {
            _characterFeedBackComponent.Play(name);
        }

        public void InputMove(Vector2 input)
        {
            _movementComponent.InputMove(input);
        }
        
        public void StopMotionClip()
        {
            _movementComponent.StopCurrentMotionClip();
        }
    }
}