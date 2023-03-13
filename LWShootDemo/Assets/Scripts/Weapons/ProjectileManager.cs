/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年3月12日
 * @modify date 2023年3月12日
 * @desc [子弹管理器]
 */

#pragma warning disable 0649
using System;
using System.Collections.Generic;
using LWShootDemo.Entities.Enemy;
using LWShootDemo.Pool;
using UnityEngine;

namespace LWShootDemo.Weapons
{
    /// <summary>
    /// 子弹管理器
    /// </summary>
    public class ProjectileManager : MonoBehaviour
    {
        #region FIELDS

        // 子弹持续时间
        [SerializeField]
        private float projectileLifeTime = 5f;

        // 子弹池
        [SerializeField]
        private SimpleUnitySpawnPool projectilePool;

        // local
        private List<Projectile> projectiles = new List<Projectile>();

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// 生成子弹
        /// </summary>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        public void CreateProjectile(Vector3 position, Quaternion rotation)
        {
            var projectile          = projectilePool.Get().GetComponent<Projectile>();
            var projectileTransform = projectile.transform;
            projectileTransform.position = position;
            projectileTransform.rotation = rotation;

            projectile.Init(Time.time);

            Debug.Assert(!projectiles.Contains(projectile));
            projectiles.Add(projectile);
        }

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        // 将超时的子弹和死亡的子弹回收，其余子弹移动
        private void FixedUpdate()
        {
            for (int index = projectiles.Count - 1; index >= 0; index--)
            {
                var projectile = projectiles[index];

                if (projectile.IsDead || projectile.SpawnTime + projectileLifeTime < Time.time)
                {
                    projectiles.Remove(projectile);
                    projectilePool.Release(projectile);
                }
                else
                {
                    projectile.Move();
                }
            }
        }

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649