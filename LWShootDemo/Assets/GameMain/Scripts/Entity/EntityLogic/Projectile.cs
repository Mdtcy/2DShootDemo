using System;
using System.Collections.Generic;
using System.Linq;
using GameFramework;
using LWShootDemo.Entities;
using UnityEngine;
using UnityEngine.Assertions;
using UnityGameFramework.Runtime;

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
        ///本帧的速度
        ///</summary>
        private Vector3 _velocity;

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
        
        private bool _hitObstacle = false;

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

        private MovementComponent _movementComponent;
        private Rigidbody2D _rigidbody2D;
        private Collider2D _collider2D;

        private void Awake()
        {
            _movementComponent = GetComponent<MovementComponent>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _collider2D = GetComponent<Collider2D>();
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
            _hitObstacle = false;
            base.OnHide(isShutdown, userData);
        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);

            // OnTrigger2D 在OnUpdate之前（待更进一步验证） 所以销毁放在这
            // 生命周期的结算
            if (NeedDestroy())
            {
                // 子弹移除事件
                var projectileRemoveArgs = OnProjectileRemoveArgs.Create();
                TriggerEvent<OnProjectileRemoveEvent, OnProjectileRemoveArgs>(projectileRemoveArgs);
                ReferencePool.Release(projectileRemoveArgs);

                GameEntry.Entity.HideEntity(Id);
                return;
            }
            
            //如果是刚创建的，那么就要处理刚创建的事情
            if (_timeElapsed <= 0)
            {
                var buffOnProjectileCreateArgs = BuffOnProjectileCreateArgs.Create(this);
                _caster.TriggerBuff<BuffOnProjectileCreateEvent, BuffOnProjectileCreateArgs>(buffOnProjectileCreateArgs);

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

            var target = _followingTarget == null ? null : _followingTarget.transform;
            _velocity = _projectileTween.Tween(elapseSeconds, this, target);
            
            //处理子弹的移动信息
            Move(_velocity); 
            Forward = _velocity.normalized;


            // 更新子弹时间
            _duration -= elapseSeconds;
            _timeElapsed += elapseSeconds;
        }

        private bool NeedDestroy()
        {
            return (_duration <= 0 || // 持续时间结束
                    HitObstacle() || // 子弹碰到障碍物
                    _hp <= 0); // 子弹命中次数不足
        }

        ///<summary>
        ///判断子弹是否还能击中某个GameObject todo 
        ///<param name="target">目标gameObject</param>
        ///</summary>
        public bool CanHit(GameObject target)
        {
            if (_timeElapsed < _canHitAfterCreated)
            {
                return false;
            }

            if (target.GetComponent<AoeState>() != null)
            {
                return false;
            }

            if (target.GetComponent<Fruit>() != null)
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
        
        public bool HitObstacle()
        {
            return _hitObstacle;
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


        RaycastHit2D[] hitResults = new RaycastHit2D[50];  
        private void OnTriggerEnter2D(Collider2D other)
        {
            // 如果子弹已经要销毁了，直接跳过
            if (NeedDestroy())
            {
                return;
            }

            // 如果无法击中，直接跳过
            if (CanHit(other.gameObject) == false)
            {
                return;
            }

            Side side = Side.None;
            if (_caster)
            {
                Character character = _caster.GetComponent<Character>();
                Assert.IsNotNull(character);
                side = character.Side;
            }
            
            // 如果因为阵营问题无法击中，直接跳过
            var hitCharacter = other.gameObject.GetComponent<Character>();
            if (hitCharacter != null)
            {
                if ((Prop.hitAlly == false && side == hitCharacter.Side) ||
                    (Prop.hitFoe == false && side != hitCharacter.Side))
                {
                    return;
                }
            }
            else
            {
                int obstacleLayer = LayerMask.NameToLayer("Obstacles");
                if (other.gameObject.layer == obstacleLayer)
                {
                    _hitObstacle = true;
                }
            }

            // 执行击中逻辑
            _hp -= 1;

            // 定义射线的起点和方向
            Vector2 rayOrigin = transform.position;
            Vector2 rayDirection = _rigidbody2D.velocity.normalized;
            
            int numberOfHits = Physics2D.CircleCastNonAlloc(rayOrigin, GetRadius(), rayDirection, hitResults, 2);

            bool hasHit = false;
            for (int i = 0; i < numberOfHits; i++)
            {
                if (hitResults[i].collider == other)
                {
                    var projectileHitArgs = OnProjectileHitArgs.Create(other.gameObject, 
                        hitResults[i].point,
                        -hitResults[i].normal);
                    TriggerEvent<OnProjectileHitEvent, OnProjectileHitArgs>(projectileHitArgs);
                    ReferencePool.Release(projectileHitArgs);

                    if (_caster != null)
                    {
                        var buffProjectileHitArgs = BuffOnProjectileHitArgs.Create(this,
                            other.gameObject, 
                            hitResults[i].point,
                            -hitResults[i].normal);
                        _caster.TriggerBuff<BuffOnProjectileHitEvent,BuffOnProjectileHitArgs>(buffProjectileHitArgs);
                    }

                    hasHit = true;
                    break;
                }
            }

            if (!hasHit)
            {
                // todo 暂时不知道原因
                Log.Error("没有检测到子弹击中的碰撞体");
                return;
            }

            // 记录子弹命中
            if (_hp > 0)
            {
                AddHitRecord(other.gameObject);
            }
        }
        
        public bool ContainTag(ProjectileTag tag)
        {
            return Prop.Tag.HasFlag(tag);
        }
        
        /// <summary>
        /// 获取一个大概的半径
        /// </summary>
        /// <returns></returns>
        float GetRadius()
        {
            if (_collider2D is CircleCollider2D circle)
            {
                return circle.radius;
            }
            else if (_collider2D is BoxCollider2D box)
            {
                // 取较长的那个维度（宽或高）的一半作为“半径”
                var size = box.size;
                return Mathf.Max(size.x, size.y) * 0.5f;
            }
            else
            {
                Log.Error("子弹的碰撞体不是圆形或者矩形");
                return 0f;
            }
        }
    }
}