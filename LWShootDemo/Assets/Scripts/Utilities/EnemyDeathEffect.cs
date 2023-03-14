/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年3月14日
 * @modify date 2023年3月14日
 * @desc []
 */

#pragma warning disable 0649
using LWShootDemo.Managers;
using LWShootDemo.Pool;
using UnityEngine;

namespace Utilities
{
    public class EnemyDeathEffect : PoolObject
    {
        #region FIELDS

        [SerializeField]
        private float duration;

        private SimpleUnitySpawnPool deathEffectPool;
        private float                spawnTime;

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        public void Init()
        {
            spawnTime = Time.time;
        }

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        private void Start()
        {
            deathEffectPool = GameManager.Instance.EnemyDeathEffectPool;
        }

        private void Update()
        {
            if (Time.time - spawnTime > duration)
            {
                deathEffectPool.Release(this);
            }
        }

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649