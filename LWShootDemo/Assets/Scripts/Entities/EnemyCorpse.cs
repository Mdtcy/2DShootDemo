/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年3月11日
 * @modify date 2023年3月11日
 * @desc [任务组UI]
 */

#pragma warning disable 0649
using Sirenix.OdinInspector;
using UnityEngine;

namespace LWShootDemo.Entities
{
    public class EnemyCorpse : MonoBehaviour
    {
        #region FIELDS

        [SerializeField]
        private int damage;

        [SerializeField]
        private float force;

        [SerializeField]
        private Rigidbody2D rb2D;

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        [Button]
        public void Setup(Vector3 direction)
        {
            rb2D.AddForce(force * direction, ForceMode2D.Impulse);
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