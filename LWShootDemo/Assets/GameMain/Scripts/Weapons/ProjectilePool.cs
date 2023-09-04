/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年3月12日
 * @modify date 2023年3月12日
 * @desc [子弹对象池]
 */

#pragma warning disable 0649
using UnityEngine;
using UnityEngine.Pool;

namespace LWShootDemo.Weapons
{
    /// <summary>
    /// 子弹对象池
    /// </summary>
    public class ProjectilePool : MonoBehaviour
    {
        #region FIELDS

        [SerializeField]
        private bool collectionChecks = true;

        [SerializeField]
        private int  maxPoolSize      = 10;

        [SerializeField]
        private int defaultCapacity = 100;

        // 子弹预制体
        [SerializeField]
        private Transform pfbProjectile;

        private IObjectPool<Projectile> pool;

        #endregion

        #region PROPERTIES

        /// <summary>
        /// 对象池
        /// </summary>
        private IObjectPool<Projectile> Pool => pool ??= new ObjectPool<Projectile>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool,
                                                OnDestroyPoolObject, collectionChecks, defaultCapacity, maxPoolSize);

        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// 获取子弹
        /// </summary>
        /// <returns></returns>
        public Projectile Get()
        {
            return Pool.Get();
        }

        /// <summary>
        /// 释放子弹到池中
        /// </summary>
        /// <param name="projectile"></param>
        public void Release(Projectile projectile)
        {
            Pool.Release(projectile);
        }

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        private void OnDestroyPoolObject(Projectile projectile)
        {
            Destroy(projectile.gameObject);
        }

        private void OnReturnedToPool(Projectile projectile)
        {
            projectile.gameObject.SetActive(false);
        }

        private void OnTakeFromPool(Projectile bullet)
        {
            bullet.gameObject.SetActive(true);
        }

        private Projectile CreatePooledItem()
        {
            var go     = Instantiate(pfbProjectile);
            var bullet = go.GetComponent<Projectile>();

            return bullet;
        }

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649