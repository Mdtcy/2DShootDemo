/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年3月14日
 * @modify date 2023年3月14日
 * @desc [敌人死亡特效]
 */

#pragma warning disable 0649
using LWShootDemo.Pool;
using UnityEngine;

namespace LWShootDemo.Enemy
{
    /// <summary>
    /// 敌人死亡特效
    /// </summary>
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