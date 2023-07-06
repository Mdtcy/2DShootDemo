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

namespace GameMain
{
    public class Character : EntityLogicBase
    {
        public BuffComponent Buff;
        private MovementComponent _movementComponent;
        
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
        
        // 最大血量
        private int _maxHp;
        
        public int MaxHp => _maxHp;
        private Side _side;
        public Side Side => _side;

        public Action<int> ActOnHpChanged;
        
        // 当前血量
        private int _curHp;

        public int CurHp
        {
            get => _curHp;
            set
            {
                if (!value.Equals(_curHp))
                {
                    _curHp = value;
                    ActOnHpChanged?.Invoke(_curHp);
                    _hpBar.UpdateProgress(_curHp, _maxHp);
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
        }

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            
            var characterData = userData as CharacterData;
            _maxHp = characterData.MaxHp;
            _side = characterData.Side;
            
            isDead = false;
            canMove = true;
            CurHp   = _maxHp;
            
            _hpBar.UpdateImmeadiatly(CurHp, _maxHp);
        }

        private HpBar _hpBar;

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
            Buff.UpdateBuff(elapseSeconds);
        }
        
        
        public void AddBuff(AddBuffInfo addBuffInfo)
        {
            Buff.AddBuff(addBuffInfo);
        }

        public void TriggerBuff<TBuffEvent, TEventActArgs>(TEventActArgs args) where TBuffEvent : BuffEvent<TEventActArgs> where TEventActArgs : class, IEventActArgs
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
        
        public void TakeDamage(int damage)
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