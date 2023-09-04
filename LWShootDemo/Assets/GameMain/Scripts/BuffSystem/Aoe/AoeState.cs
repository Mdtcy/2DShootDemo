using System;
using System.Collections.Generic;
using System.Linq;
using GameFramework;
using GameFramework.Entity;
using UnityEngine;
using UnityEngine.Assertions;

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
        private List<Character> _chaInAoe = new();
        private List<Projectile> _projectileInAoe = new();
        private float _radius;
        public float Radius => _radius;

        // 关联的buff
        private Buff _relatedBuff;
        public Buff RelatedBuff => _relatedBuff;

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
        
        public Character Caster => _caster;
        
        public List<Character> ChaInAoe => _chaInAoe;
        public List<Projectile> ProjectileInAoe => _projectileInAoe;

        public Dictionary<string, object> Params;

        public void Add(string name, object ob)
        {
            Params[name] = ob;
        }

        public object Get(string name)
        {
            if (Params == null)
            {
                return null;
            }

            if (Params.ContainsKey(name))
            {
                return Params[name];
            }
            else
            {
                return null;
            }
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
            _timeElapsed = 0;
            Params = data.Params;
            SetRadius(data.Radius);
            
            // todo 优化
            _events = _prop.Events.ToDictionary(e => e.GetType());
            _tween = _prop.TweenData.CreateTween();
        }

        protected override void OnRecycle()
        {
            _prop = null;
            _caster = null;
            _events = null;
            _tickCount = 0;
            _tween.ReleaseToPool();
            _tween = null;
            _chaInAoe.Clear();
            _projectileInAoe.Clear();
            _relatedBuff = null;
            _timeElapsed = 0;
            
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

            // 移动
            _velocity = _tween.Tween(elapseSeconds, this);
            Move(_velocity);
            Forward = _velocity.normalized;

            // 捕获所有的角色
            List<IEntity> characters = new List<IEntity>();
            characters.AddRange(GameEntry.Entity.GetEntityGroup("Player").GetAllEntities());
            characters.AddRange(GameEntry.Entity.GetEntityGroup("Enemy").GetAllEntities());

            //捕获所有的子弹
            List<IEntity> bullets = new List<IEntity>();
            bullets.AddRange(GameEntry.Entity.GetEntityGroup("Projectile").GetAllEntities());

            if (_timeElapsed <= 0)
            {
                // 捕获所有的角色
                foreach (var entity in characters)
                {
                    var character = GameEntry.Entity.GetEntity(entity.Id).Logic as Character;
                    if (character != null && InRange(transform.position.x, transform.position.y,
                            character.transform.position.x, character.transform.position.y, _radius))
                    {
                        _chaInAoe.Add(character);
                    }
                }
                
                // 捕获所有的子弹
                foreach (var bullet in bullets)
                {
                    var projectile = GameEntry.Entity.GetEntity(bullet.Id).Logic as Projectile;
                    if (projectile != null && InRange(transform.position.x, transform.position.y,
                            projectile.transform.position.x, projectile.transform.position.y, _radius))
                    {
                        _projectileInAoe.Add(projectile);
                    }
                }
                
                // Aoe创建事件
                var aoeCreateArgs = OnAoeCreateArgs.Create();
                TriggerEvent<OnAoeCreateEvent, OnAoeCreateArgs>(aoeCreateArgs);
                ReferencePool.Release(aoeCreateArgs);
            }
            else
            {
                // 处理角色离开aoe
                for (int i = _chaInAoe.Count - 1; i >= 0; i--)
                {
                    var character = _chaInAoe[i];
                    if (!InRange(transform.position.x, transform.position.y,
                            character.transform.position.x, character.transform.position.y, _radius))
                    {
                        _chaInAoe.Remove(character);
                        
                        // ChaExitAoe事件
                        var onChaExitAoeArgs = OnChaExitAoeArgs.Create(character);
                        TriggerEvent<OnChaExitAoeEvent, OnChaExitAoeArgs>(onChaExitAoeArgs);
                        ReferencePool.Release(onChaExitAoeArgs);
                    }
                }
                
                // 处理子弹离开aoe
                for (int i = _projectileInAoe.Count - 1; i >= 0; i--)
                {
                    var projectile = _projectileInAoe[i];
                    if (!InRange(transform.position.x, transform.position.y,
                            projectile.transform.position.x, projectile.transform.position.y, _radius))
                    {
                        _projectileInAoe.Remove(projectile);
                        
                        // Projectile ExitAoe事件
                        var projectileExitAoeArgs = OnProjectileExitAoeArgs.Create(projectile);
                        TriggerEvent<OnProjectileExitAoeEvent, OnProjectileExitAoeArgs>(projectileExitAoeArgs);
                        ReferencePool.Release(projectileExitAoeArgs);
                    }
                }

                // 捕获所有的角色
                foreach (var entity in characters)
                {
                    var character = GameEntry.Entity.GetEntity(entity.Id).Logic as Character;
                    if (character != null && 
                        InRange(transform.position.x, transform.position.y,
                            character.transform.position.x, character.transform.position.y, _radius)&&
                        !ChaInAoe.Contains(character))
                    {
                        _chaInAoe.Add(character);
                        
                        // ChaEnterAoe事件
                        var chaEnterAoe = OnChaEnterAoeArgs.Create(character);
                        TriggerEvent<OnChaEnterAoeEvent, OnChaEnterAoeArgs>(chaEnterAoe);
                        ReferencePool.Release(chaEnterAoe);
                    }
                }
                
                // 捕获所有的子弹
                foreach (var bullet in bullets)
                {
                    var projectile = GameEntry.Entity.GetEntity(bullet.Id).Logic as Projectile;
                    if (projectile != null && InRange(transform.position.x, transform.position.y,
                            projectile.transform.position.x, projectile.transform.position.y, _radius)&&
                        !_projectileInAoe.Contains(projectile))
                    {
                        _projectileInAoe.Add(projectile);
                        
                        // Projectile EnterAoe事件
                        var projectileEnterAoeArgs = OnProjectileEnterAoeArgs.Create(projectile);
                        TriggerEvent<OnProjectileEnterAoeEvent, OnProjectileEnterAoeArgs>(projectileEnterAoeArgs);
                        ReferencePool.Release(projectileEnterAoeArgs);
                    }
                }
                
            }

            _timeElapsed += elapseSeconds;
            
            if (_timeElapsed >= (_tickCount + 1) * _prop.TickTime)
            {
                // Aoe Tick事件
                var aoeTickArgs = OnAoeTickArgs.Create(_tickCount);
                TriggerEvent<OnAoeTickEvent, OnAoeTickArgs>(aoeTickArgs);
                ReferencePool.Release(aoeTickArgs);
                
                _tickCount++;
            }
        }

        public void BindBuff(Buff buff)
        {
            _relatedBuff = buff;
        }

        public void SetRadius(float range)
        {
            SetScale(Vector3.one * range * 2);
            _radius = range;
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

        public bool InRange(float x1, float y1, float x2, float y2, float range){
            return Mathf.Pow(x1 - x2, 2) + Mathf.Pow(y1 - y2, 2) <= Mathf.Pow(range,  2);
        }
        
        public bool ContainTag(ProjectileTag tag)
        {
            return _prop.Tag.HasFlag(tag);
        }
    }
}