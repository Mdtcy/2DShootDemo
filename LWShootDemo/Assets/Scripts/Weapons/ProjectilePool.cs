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
    public class ProjectilePool : MonoBehaviour
    {
        #region FIELDS

        public bool collectionChecks = true;
        public int  maxPoolSize      = 10;

        // 子弹预制体
        [SerializeField]
        private Transform pfbProjectile;

        private IObjectPool<Projectile> pool;

        #endregion

        #region PROPERTIES

        private IObjectPool<Projectile> Pool =>
            pool ??= new ObjectPool<Projectile>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool,
                                                OnDestroyPoolObject, collectionChecks, 100, maxPoolSize);

        #endregion

        #region PUBLIC METHODS

        public Projectile Get()
        {
            return Pool.Get();
        }

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