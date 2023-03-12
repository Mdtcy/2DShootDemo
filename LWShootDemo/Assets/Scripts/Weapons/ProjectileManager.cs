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
using UnityEngine;

namespace LWShootDemo.Weapons
{
    /// <summary>
    /// 子弹管理器
    /// </summary>
    public class ProjectileManager : MonoBehaviour
    {
        #region FIELDS

        // 子弹预制体
        [SerializeField]
        private Projectile pfbProjectile;

        [SerializeField]
        private float projectileLifeTime = 5f;

        [SerializeField]
        private ProjectilePool projectilePool;

        // local
        private List<Projectile> projectiles = new List<Projectile>();

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        public void CreateProjectile(Vector3 position, Quaternion rotation)
        {
            var projectile = projectilePool.Get();
            var projectileTransform = projectile.transform;
            projectileTransform.position = position;
            projectileTransform.rotation = rotation;

            projectile.Init(Time.time);

            Debug.Assert(!projectiles.Contains(projectile));
            projectiles.Add(projectile);
        }

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

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649