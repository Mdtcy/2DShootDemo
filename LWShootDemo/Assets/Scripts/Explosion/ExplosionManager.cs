/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年3月12日
 * @modify date 2023年3月12日
 * @desc [爆炸管理器 负责爆炸的生成和回收]
 */

#pragma warning disable 0649
using System;
using System.Collections.Generic;
using LWShootDemo.Entities.Enemy;
using LWShootDemo.Pool;
using UnityEngine;

namespace LWShootDemo.Explosions
{
    /// <summary>
    /// 爆炸管理器 负责爆炸的生成和回收
    /// </summary>
    public class ExplosionManager : MonoBehaviour
    {
        #region FIELDS

        // 对象池
        [SerializeField]
        private SimpleUnitySpawnPool explosionPool;

        // 爆炸持续时间
        [SerializeField]
        private float lifeTime;

        // local
        private List<Explosion> explosions = new List<Explosion>();

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// 生成一个爆炸
        /// </summary>
        /// <param name="position"></param>
        public void CreateExplosion(Vector3 position)
        {
            var explosion = explosionPool.Get().GetComponent<Explosion>();
            explosion.transform.position = position;
            explosion.Play();

            Debug.Assert(!explosions.Contains(explosion));
            explosions.Add(explosion);
        }

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        // 将超时的爆炸回收
        private void Update()
        {
            for (int index = explosions.Count - 1; index >= 0; index--)
            {
                var explosion = explosions[index];

                if (explosion.SpawnTime + lifeTime < Time.time)
                {
                    explosions.Remove(explosion);
                    explosionPool.Release(explosion);
                }
            }
        }

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649