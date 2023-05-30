/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年3月12日
 * @modify date 2023年3月12日
 * @desc [子弹管理器]
 */

#pragma warning disable 0649
using System.Collections.Generic;
using GameFramework.ObjectPool;
using LWShootDemo.Entities;
using UnityEngine;
using Zenject;

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

        // // 子弹池
        // [SerializeField]
        // private SimpleUnitySpawnPool projectilePool;

        [SerializeField] private Projectile _pfbProjectile;

        // local
        private List<Projectile> projectiles = new List<Projectile>();

        private IObjectPool<ProjectileObject> _projectileObjectPool = null;
        
        // * inject
        private IObjectPoolManager _objectPoolManager;

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS
        
        [Inject]
        public void Construct(IObjectPoolManager objectPoolManager)
        {
            _objectPoolManager = objectPoolManager;
        }

        /// <summary>
        /// 生成子弹
        /// </summary>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        public void CreateProjectile(Entity caster, Vector3 position, Quaternion rotation)
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

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        private void Start()
        {
            _projectileObjectPool = _objectPoolManager.CreateSingleSpawnObjectPool<ProjectileObject>(_pfbProjectile.name, 16);
        }

        [Inject]
        private DiContainer _container;
        
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
                projectile = _container.InstantiatePrefab(pfbProjectile).GetComponent<Projectile>();
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
#pragma warning restore 0649