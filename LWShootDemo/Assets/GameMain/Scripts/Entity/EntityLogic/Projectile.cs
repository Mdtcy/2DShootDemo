using System;
using System.Collections.Generic;
using System.Linq;
using GameFramework;
using LWShootDemo.BuffSystem.Event;
using LWShootDemo.Entities;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Pool;

namespace GameMain
{
    public class Projectile : EntityLogicBase
    {
        public ProjectileProp Prop;
        
        /// <summary>
        /// 要发射子弹的这个人
        /// 当然可以是null发射的，但是写效果逻辑的时候得小心caster是null的情况
        /// </summary>
        public Character Caster => _caster;
        private Character _caster;
        
        /// <summary>
        /// 子弹的速度，单位：米/秒，跟Tween结合获得Tween得到当前移动速度
        /// </summary>
        private float _speed;
        public float Speed => _speed;
        
        /// <summary>
        /// 子弹的生命周期，单位：秒
        /// </summary>
        private float _duration;

        /// <summary>
        /// 子弹已经存在了多久了，单位：秒
        /// 毕竟duration是可以被重设的，比如经过一个aoe，生命周期减半了
        /// </summary>
        public float _timeElapsed = 0;
        
        ///<summary>
        ///还能命中几次
        ///</summary>
        public int _hp = 1;

        /// <summary>
        /// 运动轨迹
        /// </summary>
        private ProjectileTween _projectileTween;
        
        ///<summary>
        ///本帧的移动
        ///</summary>
        private Vector3 _moveStep;

        /// <summary>
        /// 本帧的移动距离 todo 这样是否合理
        /// </summary>
        private float MoveDistance => _moveStep.magnitude;

        /// <summary>
        /// 子弹命中纪录
        /// </summary>
        private List<BulletHitRecord> _hitRecords = new();

        /// <summary>
        /// 子弹创建后多久是没有碰撞的，这样比如子母弹之类的，不会在创建后立即命中目标，但绝大多子弹还应该是0的
        /// 单位：秒
        /// </summary>
        public float _canHitAfterCreated;

        /// <summary>
        /// 子弹正在追踪的目标，不太建议使用这个，最好保持null todo 未初始化
        /// </summary>
        public GameObject _followingTarget;

        /// <summary>
        /// 子弹传入的参数，逻辑用的到的临时记录 todo 
        /// </summary>
        public Dictionary<string, object> param = new Dictionary<string, object>();

        public Vector3 Forward 
        { 
            set => transform.up = value;
            get => transform.up;
        }

        private Dictionary<Type, ProjectileEvent> _events;

        private ProjectilePhysicCastComponent _physicCastComponent;
        private MovementComponent _movementComponent;

        private void Awake()
        {
            _physicCastComponent = GetComponent<ProjectilePhysicCastComponent>();
            _movementComponent = GetComponent<MovementComponent>();
        }

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            var projectileData = userData as ProjectileData;
            Prop = projectileData.Prop;
            _caster = projectileData.Caster;
            _duration = Prop.Duration;
            _hp = Prop.HitTimes;
            _canHitAfterCreated = Prop.CanHitAfterCreated;
            _speed = Prop.Speed;
            _timeElapsed = 0;
            _projectileTween = Prop.TweenData.CreateTween();

            // todo 是否需要优化的操作
            _events = Prop.Events.ToDictionary(e => e.GetType());
        }

        protected override void OnHide(bool isShutdown, object userData)
        {
            Prop = null;
            _events.Clear();
            _projectileTween.ReleaseToPool();
            _projectileTween = null;
            base.OnHide(isShutdown, userData);
        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);

            //如果是刚创建的，那么就要处理刚创建的事情
            if (_timeElapsed <= 0)
            {
                var projectileCreateArgs = OnProjectileCreateActArgs.Create();
                TriggerEvent<OnProjectileCreateEvent, OnProjectileCreateActArgs>(projectileCreateArgs);
                ReferencePool.Release(projectileCreateArgs);
            }

            //处理子弹命中纪录信息
            for (int i = _hitRecords.Count - 1; i >= 0; i--)
            {
                var hitRecord = _hitRecords[i];
                hitRecord.timeToCanHit -= elapseSeconds;
                if (hitRecord.timeToCanHit <= 0 || hitRecord.target == null)
                {
                    //理论上应该支持可以鞭尸，所以即使target dead了也得留着……
                    _hitRecords.Remove(hitRecord);
                }
            }

            _moveStep = _projectileTween.Tween(elapseSeconds, this, _followingTarget?.transform);
            
            //处理子弹的移动信息
            Move(_moveStep); 
            Forward = _moveStep.normalized;

            //处理子弹的碰撞信息，如果子弹可以碰撞，才会执行碰撞逻辑
            if (_canHitAfterCreated > 0)
            {
                _canHitAfterCreated -= elapseSeconds;
            }
            else
            {
                Side side = Side.None;
                if (_caster)
                {
                    Character character = _caster.GetComponent<Character>();
                    Assert.IsNotNull(character);
                    side = character.Side;
                }
                
                var hitInfos = ListPool<RaycastHit2D>.Get();
                // todo 目前并没有找到办法验证这个方法的正确性 distance
                PhysicCast(-Forward, MoveDistance, ref hitInfos);

                foreach (var hitInfo in hitInfos)
                {
                    // 如果无法击中，直接跳过
                    if (CanHit(hitInfo.collider.gameObject) == false)
                    {
                        continue;
                    }

                    // 如果因为阵营问题无法击中，直接跳过
                    var hitCharacter = hitInfo.collider.gameObject.GetComponent<Character>();
                    if (hitCharacter != null)
                    {
                        if ((Prop.hitAlly == false && side == hitCharacter.Side) ||
                            (Prop.hitFoe == false && side != hitCharacter.Side))
                        {
                            continue;
                        }
                    }
                    else
                    {
                        continue;
                    }

                    // 执行击中逻辑
                    _hp -= 1;

                    // 子弹击中事件 hitInfo.normal是从被击中的表面指向投射物 所以传入相反的作为方向
                    var projectileHitArgs = OnProjectileHitArgs.Create(hitInfo.collider.gameObject, 
                        hitInfo.point,
                        -hitInfo.normal);
                    TriggerEvent<OnProjectileHitEvent, OnProjectileHitArgs>(projectileHitArgs);
                    ReferencePool.Release(projectileHitArgs);

                    // 记录子弹命中
                    if (_hp > 0)
                    {
                        AddHitRecord(hitInfo.collider.gameObject);
                    }
                }
                
                ListPool<RaycastHit2D>.Release(hitInfos);
            }


            // 更新子弹时间
            _duration -= elapseSeconds;
            _timeElapsed += elapseSeconds;

            // 生命周期的结算
            if (_duration <= 0 || // 持续时间结束
                HitObstacle() || // 子弹碰到障碍物
                _hp <= 0) // 子弹命中次数不足
            {
                // 子弹移除事件
                var projectileRemoveArgs = OnProjectileRemoveArgs.Create();
                TriggerEvent<OnProjectileRemoveEvent, OnProjectileRemoveArgs>(projectileRemoveArgs);
                ReferencePool.Release(projectileRemoveArgs);

                GameEntry.Entity.HideEntity(Id);
            }
        }

        ///<summary>
        ///判断子弹是否还能击中某个GameObject
        ///<param name="target">目标gameObject</param>
        ///</summary>
        public bool CanHit(GameObject target)
        {
            if (_canHitAfterCreated > 0)
            {
                return false;
            }

            for (int i = 0; i < _hitRecords.Count; i++)
            {
                if (_hitRecords[i].target == target)
                {
                    return false;
                }
            }

            // todo 无敌的处理暂时略去
            // ChaState cs = target.GetComponent<ChaState>();
            // if (cs && cs.immuneTime > 0) return false;

            return true;
        }
        
        /// <summary>
        /// 触发子弹事件
        /// </summary>
        /// <param name="args"></param>
        /// <typeparam name="TProjectileEvent"></typeparam>
        /// <typeparam name="TEventActArgs"></typeparam>
        public void TriggerEvent<TProjectileEvent, TEventActArgs>(TEventActArgs args)
            where TProjectileEvent : ProjectileEvent<TEventActArgs> where TEventActArgs : BaseProjectileEventActArgs
        {
            var key = typeof(TProjectileEvent);
            
            if (_events.TryGetValue(key, out var projectileEvent))
            {
                if (projectileEvent is TProjectileEvent tProjectileEvent)
                {
                    args.Projectile = this;
                    tProjectileEvent.Trigger(args);
                }
            }
        }

        // private Vector3     
        private void Move(Vector3 moveForce)
        {
            _movementComponent.InputMove(moveForce);
        }
        
        // todo 击中障碍物的判断
        public bool HitObstacle()
        {
            return false;
            // throw new NotImplementedException();
        }

        public void PhysicCast(Vector3 direction, float distance, ref List<RaycastHit2D> hitInfos)
        {
            _physicCastComponent.PhysicCast(direction, distance, ref hitInfos);
        }
        
        /// <summary>
        /// 添加命中纪录
        /// </summary>
        /// <param name="target"></param>
        private void AddHitRecord(GameObject target)
        {
            _hitRecords.Add(new BulletHitRecord(
                target,
                Prop.sameTargetDelay
            ));
        }
    }
}