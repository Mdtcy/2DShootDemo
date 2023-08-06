using System.Collections.Generic;
using GameFramework;
using LWShootDemo;
using LWShootDemo.Entities;
using UnityEngine;
using UnityEngine.Assertions;
using UnityGameFramework.Runtime;

namespace GameMain
{
    public class ProjectileComponent : GameFrameworkComponent
    {
        #region FIELDS
   
        // local
        private List<Projectile> _projectiles = new();

        #endregion
        
        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// 生成子弹
        /// </summary>
        /// <param name="launcherProp"></param>
        /// <param name="projectileProp"></param>
        /// <param name="firePoint"></param>
        public void CreateProjectile(ProjectileLauncherProp launcherProp,
            ProjectileProp projectileProp,
            Character caster,
            Transform firePoint)
        {
            var quaternion = Quaternion.identity;
            switch (launcherProp.InitDirection)
            {
                case ProjectileInitDirection.FirePointDirection:
                    quaternion = firePoint.rotation;
                    break;
                case ProjectileInitDirection.FixAngle:
                    quaternion = Quaternion.Euler(0, 0, launcherProp.Angle);
                    break;
                default:
                    Log.Error("未定义的初始方向: " + launcherProp.InitDirection);
                    break;
            }

            
            // 生成子弹实体
            int id = GameEntry.Entity.GenerateSerialId();
            GameEntry.Entity.ShowProjectile(new ProjectileData(id,
                projectileProp.EntityProp.ID,
                projectileProp,
                caster)
            {
                Position = firePoint.transform.position,
                Rotation = quaternion,
                Scale = Vector3.one,
            });
        }
        
        // private void Update()
        // {
        //
        //     if (Input.GetMouseButtonDown(1))
        //     {
        //         var projectileProp = GameEntry.TableConfig.Get<ProjectileTable>().TableList[0];
        //         var launcherProp = GameEntry.TableConfig.Get<ProjectileLauncherTable>().TableList[0];
        //         // todo
        //         CreateProjectile(launcherProp, projectileProp, GameManager.Instance.Player.GetComponent<Character>(), GameManager.Instance.Player.transform);
        //     }
        // }

        #endregion
        
        #region PRIVATE METHODS

        // // 将超时的子弹和死亡的子弹回收，其余子弹移动
        // private void FixedUpdate()
        // {
        //     // 如果没有子弹，直接返回
        //     if (_projectiles.Count <= 0)
        //     {
        //         return;
        //     }
        //
        //     // 这一帧的时间
        //     float timePassed = Time.fixedDeltaTime;
        //
        //     for (int i = _projectiles.Count - 1; i >= 0; i--)
        //     {
        //         Projectile projectile = _projectiles[i];
        //         if (!projectile || projectile.hp <= 0)
        //         {
        //             continue;
        //         }
        //
        //         //如果是刚创建的，那么就要处理刚创建的事情
        //         if (projectile.timeElapsed <= 0)
        //         {
        //             var projectileCreateArgs = OnProjectileCreateActArgs.Create();
        //             projectile.TriggerEvent<OnProjectileCreateEvent, OnProjectileCreateActArgs>(projectileCreateArgs);
        //             ReferencePool.Release(projectileCreateArgs);
        //         }
        //
        //         //处理子弹命中纪录信息
        //         int hIndex = 0;
        //         while (hIndex < projectile.hitRecords.Count)
        //         {
        //             projectile.hitRecords[hIndex].timeToCanHit -= timePassed;
        //             if (projectile.hitRecords[hIndex].timeToCanHit <= 0 || projectile.hitRecords[hIndex].target == null)
        //             {
        //                 //理论上应该支持可以鞭尸，所以即使target dead了也得留着……
        //                 projectile.hitRecords.RemoveAt(hIndex);
        //             }
        //             else
        //             {
        //                 hIndex += 1;
        //             }
        //         }
        //
        //         //处理子弹的移动信息
        //         projectile.Move();
        //
        //         //处理子弹的碰撞信息，如果子弹可以碰撞，才会执行碰撞逻辑
        //         if (projectile.canHitAfterCreated > 0)
        //         {
        //             projectile.canHitAfterCreated -= timePassed;
        //         }
        //         else
        //         {
        //             Side side = Side.None;
        //             if (projectile.caster)
        //             {
        //                 Character character = projectile.caster.GetComponent<Character>();
        //                 Assert.IsNotNull(character);
        //                 side = character.Side;
        //             }
        //             
        //             // todo ListPool
        //             List<RaycastHit2D> hitInfos = new List<RaycastHit2D>(100);
        //
        //             // todo 这里的检测不是很精确 
        //             projectile.PhysicCast(-projectile.Forward, projectile.MoveDistance, ref hitInfos);
        //
        //             foreach (var hitInfo in hitInfos)
        //             {
        //                 // 如果无法击中，直接跳过
        //                 if (projectile.CanHit(hitInfo.collider.gameObject) == false)
        //                 {
        //                     continue;
        //                 }
        //                 
        //                 // 如果因为阵营问题无法击中，直接跳过
        //                 var hitCharacter = hitInfo.collider.gameObject.GetComponent<Character>();
        //                 if (hitCharacter != null)
        //                 {
        //                     if ((projectile.Prop.hitAlly == false && side == hitCharacter.Side)||
        //                         (projectile.Prop.hitFoe == false && side != hitCharacter.Side))
        //                     {
        //                         continue;
        //                     }
        //                 }
        //                 
        //                 // 执行击中逻辑
        //                 projectile.hp -= 1;
        //                 
        //                 // 子弹击中事件
        //                 var projectileHitArgs = OnProjectileHitArgs.Create();
        //                 projectile.TriggerEvent<OnProjectileHitEvent, OnProjectileHitArgs>(projectileHitArgs);
        //                 ReferencePool.Release(projectileHitArgs);
        //
        //                 // 记录子弹命中
        //                 if (projectile.hp > 0)
        //                 {
        //                     projectile.AddHitRecord(hitInfo.collider.gameObject);
        //                 }
        //             }
        //         }
        //
        //         // 更新子弹时间
        //         projectile.duration -= timePassed;
        //         projectile.timeElapsed += timePassed;
        //         
        //         // 生命周期的结算
        //         if (projectile.duration <= 0 || // 持续时间结束
        //             projectile.HitObstacle() || // 子弹碰到障碍物
        //             projectile.hp <= 0) // 子弹命中次数不足
        //         {
        //             // 子弹移除事件
        //             var projectileRemoveArgs = OnProjectileRemoveArgs.Create();
        //             projectile.TriggerEvent<OnProjectileRemoveEvent, OnProjectileRemoveArgs>(projectileRemoveArgs);
        //             ReferencePool.Release(projectileRemoveArgs);
        //
        //             _projectiles.Remove(projectile);
        //             GameEntry.Entity.HideEntity(projectile.Id);
        //         }
        //     }
        // }

        #endregion

        #region STATIC METHODS

        #endregion
    }
}