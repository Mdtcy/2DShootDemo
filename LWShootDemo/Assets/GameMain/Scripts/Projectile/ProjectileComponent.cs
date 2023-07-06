using System.Collections.Generic;
using GameFramework.ObjectPool;
using LWShootDemo.Entities;
using LWShootDemo.Weapons;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace GameMain
{
    public class ProjectileComponent : GameFrameworkComponent
    {
        #region FIELDS

        // 子弹持续时间
        [SerializeField] 
        private float projectileLifeTime = 5f;

        // todo 
        [SerializeField] 
        private Projectile _pfbProjectile;

        // local
        private List<Projectile> projectiles = new List<Projectile>();

        private IObjectPool<ProjectileObject> _projectileObjectPool = null;

        #endregion
        
        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// 生成子弹
        /// </summary>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        public void CreateProjectile(Character caster, Vector3 position, Quaternion rotation)
        {
            var projectile = CreateProjectile(_pfbProjectile);
            var projectileTransform = projectile.transform;
            projectileTransform.position = position;
            projectileTransform.rotation = rotation;

            projectile.Init(caster, Time.time);

            Debug.Assert(!projectiles.Contains(projectile));
            projectiles.Add(projectile);
        }

        #endregion
        
        #region PRIVATE METHODS

        public void Init()
        {
            _projectileObjectPool = 
                GameEntry.ObjectPool.CreateSingleSpawnObjectPool<ProjectileObject>("Projectile", 50);
        }

        private Projectile CreateProjectile(Projectile pfbProjectile)
        {
            Projectile projectile = null;
            ProjectileObject projectileObject = _projectileObjectPool.Spawn(pfbProjectile.name);
            if (projectileObject != null)
            {
                projectile = (Projectile)projectileObject.Target;
            }
            else
            {
                projectile = Instantiate(pfbProjectile).GetComponent<Projectile>();
                _projectileObjectPool.Register(ProjectileObject.Create(projectile), true);
            }

            return projectile;
        }
        
        // 将超时的子弹和死亡的子弹回收，其余子弹移动
        private void FixedUpdate()
        {
            for (int index = projectiles.Count - 1; index >= 0; index--)
            {
                var projectile = projectiles[index];

                if (projectile.IsDead || projectile.SpawnTime + projectileLifeTime < Time.time)
                {
                    projectiles.Remove(projectile);
                    _projectileObjectPool.Unspawn(projectile);
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