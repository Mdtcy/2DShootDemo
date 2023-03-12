/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年3月12日
 * @modify date 2023年3月12日
 * @desc [任务组UI]
 */

#pragma warning disable 0649
using System.Collections.Generic;
using UnityEngine;

namespace LWShootDemo.Explosions
{
    public class ExplosionManager : MonoBehaviour
    {
        #region FIELDS

        [SerializeField]
        private ExplosionPool explosionPool;

        [SerializeField]
        private float lifeTime;

        // local
        private List<Explosion> explosions = new List<Explosion>();

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        public void CreateExplosion(Vector3 position)
        {
            var explosion = explosionPool.Get();
            explosion.transform.position = position;
            explosion.Play();

            Debug.Assert(!explosions.Contains(explosion));
            explosions.Add(explosion);
        }

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

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