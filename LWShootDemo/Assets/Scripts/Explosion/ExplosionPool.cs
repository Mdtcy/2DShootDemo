/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年3月12日
 * @modify date 2023年3月12日
 * @desc [爆炸对象池]
 */

#pragma warning disable 0649
using UnityEngine;
using UnityEngine.Pool;

namespace LWShootDemo.Explosions
{
    /// <summary>
    /// 爆炸对象池
    /// </summary>
    public class ExplosionPool : MonoBehaviour
    {
        #region FIELDS

        [SerializeField]
        private bool collectionChecks = true;

        [SerializeField]
        private int maxPoolSize = 10;

        [SerializeField]
        private int defaultCapacity = 100;

        // 爆炸预制体
        [SerializeField]
        private Explosion pfbExplosion;

        private IObjectPool<Explosion> pool;

        #endregion

        #region PROPERTIES

        /// <summary>
        /// 对象池
        /// </summary>
        private IObjectPool<Explosion> Pool =>
            pool ??= new ObjectPool<Explosion>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool,
                                               OnDestroyPoolObject, collectionChecks, defaultCapacity, maxPoolSize);

        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// 获取爆炸
        /// </summary>
        /// <returns></returns>
        public Explosion Get()
        {
            return Pool.Get();
        }

        /// <summary>
        /// 释放爆炸到池中
        /// </summary>
        /// <param name="explosion"></param>
        public void Release(Explosion explosion)
        {
            Pool.Release(explosion);
        }

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        private void OnDestroyPoolObject(Explosion explosion)
        {
            Destroy(explosion.gameObject);
        }

        private void OnReturnedToPool(Explosion explosion)
        {
            explosion.gameObject.SetActive(false);
        }

        private void OnTakeFromPool(Explosion bullet)
        {
            bullet.gameObject.SetActive(true);
        }

        private Explosion CreatePooledItem()
        {
            var explosion = Instantiate(pfbExplosion);

            return explosion;
        }

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649