/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年3月13日
 * @modify date 2023年3月13日
 * @desc [Unity对象池]
 */

using UnityEngine;
using UnityEngine.Pool;

#pragma warning disable 0649

namespace LWShootDemo.Pool
{
    /// <summary>
    /// Unity对象池 todo 试用一下unity的pool
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SimpleUnitySpawnPool : MonoBehaviour
    {
        #region FIELDS

        [SerializeField]
        private bool collectionChecks = true;

        [SerializeField]
        private int maxPoolSize = 10;

        [SerializeField]
        private int defaultCapacity = 100;

        [SerializeField]
        protected PoolObject prefab;

        private IObjectPool<PoolObject> pool;

        #endregion

        #region PROPERTIES

        /// <summary>
        /// 对象池
        /// </summary>
        private IObjectPool<PoolObject> Pool =>
            pool ??= new ObjectPool<PoolObject>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool,
                                                OnDestroyPoolObject, collectionChecks, defaultCapacity, maxPoolSize);

        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// 获取
        /// </summary>
        /// <returns></returns>
        public PoolObject Get()
        {
            return Pool.Get();
        }

        /// <summary>
        /// 释放
        /// </summary>
        /// <param name="explosion"></param>
        public void Release(PoolObject poolObject)
        {
            Pool.Release(poolObject);
        }

        #endregion

        #region PROTECTED METHODS

        private void OnDestroyPoolObject(PoolObject poolObject)
        {
            Destroy(poolObject);
        }

        private void OnReturnedToPool(PoolObject poolObject)
        {
            poolObject.OnDespawn();
            poolObject.gameObject.SetActive(false);
        }

        private void OnTakeFromPool(PoolObject poolObject)
        {
            poolObject.OnSpawn();
            poolObject.gameObject.SetActive(true);
        }

        private PoolObject CreatePooledItem()
        {
            var poolObject = Instantiate(prefab);
            poolObject.OnSpawn();
            return poolObject;
        }

        #endregion

        #region PRIVATE METHODS

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649