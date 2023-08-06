using System;
using System.Collections.Generic;
using System.Linq;
using GameFramework;
using LWShootDemo.BuffSystem.Event;
using LWShootDemo.Entities;
using UnityEngine;
using UnityEngine.Assertions;

namespace GameMain
{
    public class Projectile : EntityLogicBase
    {
        public ProjectileProp Prop;

        ///<summary>
        ///要发射子弹的这个人
        ///当然可以是null发射的，但是写效果逻辑的时候得小心caster是null的情况
        ///</summary>
        public Character caster;
        
        ///<summary>
        ///发射的角度，单位：角度，如果useFireDegreeForever == true，那就得用这个角度来获得当前飞行路线了
        ///</summary>
        public float fireDegree;
        
        ///<summary>
        ///子弹的初速度，单位：米/秒，跟Tween结合获得Tween得到当前移动速度
        ///</summary>
        public float speed;
        
        ///<summary>
        ///子弹的生命周期，单位：秒
        ///</summary>
        public float duration;

        ///<summary>
        ///子弹已经存在了多久了，单位：秒
        ///毕竟duration是可以被重设的，比如经过一个aoe，生命周期减半了
        ///</summary>
        public float timeElapsed = 0;

        private ProjectileTween _projectileTween;
        
        ///<summary>
        ///本帧的移动
        ///</summary>
        public Vector3 MoveStep = new();

        public float MoveDistance;
        
        ///<summary>
        ///子弹的移动轨迹是否严格遵循发射出来的角度
        ///</summary>
        public bool useFireDegreeForever = false;
        
        ///<summary>
        ///子弹命中纪录
        ///</summary>
        public List<BulletHitRecord> hitRecords = new();

        ///<summary>
        ///子弹创建后多久是没有碰撞的，这样比如子母弹之类的，不会在创建后立即命中目标，但绝大多子弹还应该是0的
        ///单位：秒
        ///</summary>
        public float canHitAfterCreated = 0;

        ///<summary>
        ///子弹正在追踪的目标，不太建议使用这个，最好保持null
        ///</summary>
        public GameObject followingTarget = null;

        ///<summary>
        ///子弹传入的参数，逻辑用的到的临时记录
        ///</summary>
        public Dictionary<string, object> param = new Dictionary<string, object>();
        
        ///<summary>
        ///还能命中几次
        ///</summary>
        public int hp = 1;
        
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
            caster = projectileData.Caster;
            duration = Prop.Duration;
            hp = Prop.HitTimes;
            
            // todo 是否需要优化的操作
            _events = Prop.Events.ToDictionary(e => e.GetType());
        }

        protected override void OnHide(bool isShutdown, object userData)
        {
            Prop = null;
            _events.Clear();
            base.OnHide(isShutdown, userData);
        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);

            //如果是刚创建的，那么就要处理刚创建的事情
            if (timeElapsed <= 0)
            {
                var projectileCreateArgs = OnProjectileCreateActArgs.Create();
                TriggerEvent<OnProjectileCreateEvent, OnProjectileCreateActArgs>(projectileCreateArgs);
                ReferencePool.Release(projectileCreateArgs);
            }

            //处理子弹命中纪录信息
            int hIndex = 0;
            while (hIndex < hitRecords.Count)
            {
                hitRecords[hIndex].timeToCanHit -= elapseSeconds;
                if (hitRecords[hIndex].timeToCanHit <= 0 || hitRecords[hIndex].target == null)
                {
                    //理论上应该支持可以鞭尸，所以即使target dead了也得留着……
                    hitRecords.RemoveAt(hIndex);
                }
                else
                {
                    hIndex += 1;
                }
            }

            //处理子弹的移动信息
            Move();

            //处理子弹的碰撞信息，如果子弹可以碰撞，才会执行碰撞逻辑
            if (canHitAfterCreated > 0)
            {
                canHitAfterCreated -= elapseSeconds;
            }
            else
            {
                Side side = Side.None;
                if (caster)
                {
                    Character character = caster.GetComponent<Character>();
                    Assert.IsNotNull(character);
                    side = character.Side;
                }

                // todo ListPool
                List<RaycastHit2D> hitInfos = new List<RaycastHit2D>(100);

                // todo 这里的检测不是很精确 
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
                    hp -= 1;

                    // 子弹击中事件 hitInfo.normal是从被击中的表面指向投射物 所以传入相反的作为方向
                    var projectileHitArgs = OnProjectileHitArgs.Create(hitInfo.collider.gameObject, 
                        hitInfo.point,
                        -hitInfo.normal);
                    TriggerEvent<OnProjectileHitEvent, OnProjectileHitArgs>(projectileHitArgs);
                    ReferencePool.Release(projectileHitArgs);

                    // 记录子弹命中
                    if (hp > 0)
                    {
                        AddHitRecord(hitInfo.collider.gameObject);
                    }
                }
            }


            // 更新子弹时间
            duration -= elapseSeconds;
            timeElapsed += elapseSeconds;

            // 生命周期的结算
            if (duration <= 0 || // 持续时间结束
                HitObstacle() || // 子弹碰到障碍物
                hp <= 0) // 子弹命中次数不足
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
            if (canHitAfterCreated > 0)
            {
                return false;
            }

            for (int i = 0; i < hitRecords.Count; i++)
            {
                if (hitRecords[i].target == target)
                {
                    return false;
                }
            }
        
            // todo 
            // ChaState cs = target.GetComponent<ChaState>();
            // if (cs && cs.immuneTime > 0) return false;

            return true;
        }
        
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
        public void Move()
        {
            // todo 
                _movementComponent.InputMove(Forward * Prop.Speed);
            // throw new NotImplementedException();
        }
        
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
        public void AddHitRecord(GameObject target)
        {
            hitRecords.Add(new BulletHitRecord(
                target,
                Prop.sameTargetDelay
            ));
        }
    }
}