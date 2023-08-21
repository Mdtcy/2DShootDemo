using System;
using System.Collections.Generic;
using System.Linq;
using GameFramework;
using UnityEngine;

namespace GameMain
{
    public class AoeState : EntityLogicBase
    {
        private AoeProp _prop;
        private Character _caster;
        private float _timeElapsed;
        private Dictionary<Type, AoeEvent> _events;
        private AoeTween _tween;
        private MovementComponent _movementComponent;
        
        ///<summary>
        ///本帧的速度
        ///</summary>
        private Vector3 _velocity;
        
        // tick了几次
        private int _tickCount;
        
        public Vector3 Forward 
        { 
            set => transform.up = value;
            get => transform.up;
        }

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            _movementComponent = GetComponent<MovementComponent>();
        }

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            var data = userData as AoeData;
            _prop = data.Prop;
            _caster = data.Caster;
            _tickCount = 0;
            // todo 优化
            _events = _prop.Events.ToDictionary(e => e.GetType());
            _tween = _prop.TweenData.CreateTween();
            
            // Aoe创建事件
            var aoeCreateArgs = OnAoeCreateArgs.Create();
            TriggerEvent<OnAoeCreateEvent, OnAoeCreateArgs>(aoeCreateArgs);
            ReferencePool.Release(aoeCreateArgs);
        }

        protected override void OnRecycle()
        {
            _prop = null;
            _caster = null;
            _events = null;
            _tickCount = 0;
            _tween.ReleaseToPool();
            _tween = null;
            
            base.OnRecycle();
        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);

            if (NeedToDestroy())
            {
                // Aoe移除事件
                var aoeRemoveArgs = OnAoeRemoveArgs.Create();
                TriggerEvent<OnAoeRemoveEvent, OnAoeRemoveArgs>(aoeRemoveArgs);
                ReferencePool.Release(aoeRemoveArgs);
                
                GameEntry.Entity.HideEntity(this);
                
                return;
            }

            if (_timeElapsed >= (_tickCount + 1) * _prop.TickTime)
            {
                // Aoe Tick事件
                var aoeTickArgs = OnAoeTickArgs.Create(_tickCount);
                TriggerEvent<OnAoeTickEvent, OnAoeTickArgs>(aoeTickArgs);
                ReferencePool.Release(aoeTickArgs);
                
                _tickCount++;
            }
            
            // 移动
            _velocity = _tween.Tween(elapseSeconds, this);
            Move(_velocity);
            Forward = _velocity.normalized;
            
            _timeElapsed += elapseSeconds;
        }

        /// <summary>
        /// 是否需要销毁
        /// </summary>
        /// <returns></returns>
        private bool NeedToDestroy()
        {
            if (_timeElapsed >= _prop.Duration)
            {
                return true;
            }

            return false;
        }

        // private Vector3     
        private void Move(Vector3 moveForce)
        {
            _movementComponent.InputMove(moveForce);
        }
        
        /// <summary>
        /// 触发Aoe事件
        /// </summary>
        /// <param name="args"></param>
        /// <typeparam name="TAoeEvent"></typeparam>
        /// <typeparam name="TEventActArgs"></typeparam>
        public void TriggerEvent<TAoeEvent, TEventActArgs>(TEventActArgs args)
            where TAoeEvent : AoeEvent<TEventActArgs> where TEventActArgs : BaseAoeEventActArgs
        {
            var key = typeof(TAoeEvent);
            
            if (_events.TryGetValue(key, out var aoeEvent))
            {
                if (aoeEvent is TAoeEvent tAoeEvent)
                {
                    args.Aoe = this;
                    tAoeEvent.Trigger(args);
                }
            }
        }

        public bool ContainTag(ProjectileTag tag)
        {
            return _prop.Tag.HasFlag(tag);
        }
    }
}