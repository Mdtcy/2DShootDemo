/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年3月12日
 * @modify date 2023年3月12日
 * @desc [任务组UI]
 */

#pragma warning disable 0649
using LWShootDemo.Effect;
using Unity.Mathematics;
using UnityEngine;

namespace LWShootDemo.Managers
{
    public class ExplosionGenerator : MonoBehaviour
    {
        #region FIELDS

        [SerializeField]
        private Transform pfbExplosion;

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        public void CreateExplosion(Vector3 position)
        {
            var explosion = Instantiate(pfbExplosion, position, quaternion.identity)
               .GetComponent<ExplosionEffect>();
            explosion.Play();
        }

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649