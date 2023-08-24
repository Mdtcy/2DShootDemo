using System;
using System.Collections.Generic;
using Animancer;
using Damages;
using DefaultNamespace.GameMain.Scripts.HpBar;
using LWShootDemo.Entities;
using LWShootDemo.Entities.Player;
using UnityEngine;
using UnityEngine.Assertions;

namespace GameMain
{
    public class Character : EntityLogicBase
    {
        public BuffComponent Buff;
        private MovementComponent _movementComponent;
        private CharacterFeedBackComponent _characterFeedBackComponent;
        private NumericComponent _numericComponent;
        public UnitBindManager UnitBindManager;
        
        private bool canMove = true;
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
        
        public float Speed => _numericComponent[NumericType.Speed];
        
        public float Attack => _numericComponent[NumericType.Attack];
        
        // 最大血量
        public int MaxHp => _numericComponent[NumericType.MaxHp];
        
        // 当前血量
        public int CurHp
        {
            get => _numericComponent[NumericType.Hp];
            set
            {
                if (!value.Equals(_numericComponent[NumericType.Hp]))
                {
                    _numericComponent[NumericType.Hp] = value;
                    ActOnHpChanged?.Invoke(value);
                    _hpBar.UpdateProgress(value, _numericComponent[NumericType.MaxHp]);
                }
            }
        }
        
        /// <summary>
        /// 每秒回复血量
        /// </summary>
        public int HpRegen => _numericComponent[NumericType.HpRegen];

        public Direction FaceDirection => _movementComponent.FaceDirection;
        
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
            canMove = true;
            _tickTimes = 0;
            
            // numeric
            // todo 现在配置的全是int, float每管
            foreach (var initNumeric in characterProp.InitNumerics)
            {
                _numericComponent.Set(initNumeric.Type, initNumeric.Value);
            }
            
            // buff
            foreach (var AddbuffData in characterProp.InitBuffs)
            {
                AddBuff(new AddBuffInfo(AddbuffData.Buff, 
                    null, 
                    gameObject,
                    stack: AddbuffData.Stack, 
                    duration: AddbuffData.IsPermanent? 10f: AddbuffData.Duration, 
                    durationSetTo:true,
                    permanent: AddbuffData.IsPermanent));
            }
            Assert.IsTrue(_numericComponent[NumericType.Speed] > 0);
            Assert.IsTrue(_numericComponent[NumericType.Hp] > 0);
            Assert.IsTrue(_numericComponent[NumericType.MaxHp] > 0);

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
            if (_totalElapsedSeconds > (_tickTimes + 1))
            {
                _tickTimes++;
                int hpRegen = Mathf.Min(HpRegen, MaxHp - CurHp);
                if (hpRegen > 0)
                {
                    GameEntry.Damage.DoDamage(null, 
                        this, (int)hpRegen, Vector3.zero, 0, 
                        new List<DamageInfoTag>(){DamageInfoTag.directHeal},
                        new List<AddBuffInfo>());
                } 
            }
        }
        
        
        public void AddBuff(AddBuffInfo addBuffInfo)
        {
            Buff.AddBuff(addBuffInfo);
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