using System;
using Animancer;
using DefaultNamespace.GameMain.Scripts.HpBar;
using LWShootDemo;
using LWShootDemo.BuffSystem.Buffs;
using LWShootDemo.BuffSystem.Event;
using LWShootDemo.Entities;
using LWShootDemo.Entities.Player;
using LWShootDemo.Motion;
using UnityEngine;
using UnityEngine.Assertions;

namespace GameMain
{
    public class Character : EntityLogicBase
    {
        public BuffComponent Buff;
        private MovementComponent _movementComponent;
        private FeedBackComponent _feedBackComponent;
        private NumericComponent _numericComponent;
        
        private bool canMove = true;
        public AnimancerComponent CachedAnimancer { get; private set; }
        
        // 是否死亡
        private bool isDead;
        
        /// <summary>
        /// 是否死亡
        /// </summary>
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
        
        
        public Direction FaceDirection => _movementComponent.FaceDirection;
        
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);

            // todo 
            _hpBar = GetComponentInChildren<HpBar>();
            CachedAnimancer = GetComponent<AnimancerComponent>();
            Buff = GetComponent<BuffComponent>();
            _movementComponent = GetComponent<MovementComponent>();
            _feedBackComponent = GetComponent<FeedBackComponent>();
            _numericComponent = GetComponentInChildren<NumericComponent>();
            _numericComponent.Init(Entity);
        }

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            
            var characterData = userData as CharacterData;
            var characterProp = GameEntry.TableConfig.Get<CharacterTable>().Get(characterData.PropID);
            _side = characterProp.Side;
            
            isDead = false;
            canMove = true;

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

            _movementComponent.SetSpeed(_numericComponent[NumericType.Speed]);
            _hpBar.UpdateImmeadiatly(CurHp, MaxHp);
            _feedBackComponent.Play("Reset");
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
            _feedBackComponent.Play(name);
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