/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年3月12日
 * @modify date 2023年3月12日
 * @desc []
 */

#pragma warning disable 0649
using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LWShootDemo.Entities
{
    public class Entity : MonoBehaviour
    {
        #region FIELDS

        [SerializeField]
        private int maxHp;

        private int curHp;

        [SerializeField]
        private Rigidbody2D rb2D;

        [ShowInInspector]
        [ReadOnly]
        private bool canMove = true;

        private Coroutine knockBackHandle;

        public Action ActOnDeath;

        public Action<DamageInfo> ActOnHurt;

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// 尝试旋转
        /// </summary>
        /// <param name="angle"></param>
        public void TryRotate(float angle)
        {
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        /// <summary>
        /// 尝试移动
        /// </summary>
        /// <param name="direction"></param>
        public void TryMove(Vector3 direction, float moveSpeed)
        {
            if (!canMove)
            {
                return;
            }

            rb2D.MovePosition(position: direction.normalized * moveSpeed * Time.deltaTime + transform.position);
        }

        public void TakeDamage(DamageInfo damageInfo)
        {
            ActOnHurt?.Invoke(damageInfo);
            curHp -= damageInfo.Damage;

            if (curHp <= 0)
            {
                Kill();
            }
        }

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        private void Start()
        {
            curHp = maxHp;
        }

        private IEnumerator IApplyKnowBack(float duraction, Vector2 force)
        {
            canMove = false;
            Debug.Log($"force:{force}");
            rb2D.AddForce(force);

            yield return new WaitForSeconds(duraction);
            rb2D.velocity = Vector2.zero;
            canMove       = true;
        }

        public void ApplyKnowBack(float duraction, Vector2 force)
        {
            if (knockBackHandle != null)
            {
                StopCoroutine(knockBackHandle);
            }

            knockBackHandle = StartCoroutine(IApplyKnowBack(duraction, force));
        }


        // 击杀
        private void Kill()
        {
            ActOnDeath?.Invoke();
            Destroy(gameObject);
        }

        #endregion

        #region STATIC METHODS
        #endregion
    }
}
#pragma warning restore 0649