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

        // 实体的阵营
        [SerializeField]
        private Side side;

        // 最大血量
        [SerializeField]
        private int maxHp;

        // 当前血量
        [ShowInInspector]
        [ReadOnly]
        private int curHp;

        [SerializeField]
        private Rigidbody2D rb2D;

        // 是否可以移动
        [ShowInInspector]
        [ReadOnly]
        private bool canMove = true;

        // 击退协程
        private Coroutine knockBackHandle;

        // 是否死亡
        private bool isDead;

        /// <summary>
        /// 死亡事件
        /// </summary>
        public Action ActOnDeath;

        /// <summary>
        /// 受伤事件
        /// </summary>
        public Action<DamageInfo> ActOnHurt;

        #endregion

        #region PROPERTIES

        /// <summary>
        /// 是否死亡
        /// </summary>
        public bool IsDead => isDead;

        /// <summary>
        /// 实体阵营
        /// </summary>
        public Side Side   => side;

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

        /// <summary>
        /// 造成伤害
        /// </summary>
        /// <param name="damageInfo"></param>
        public void TakeDamage(DamageInfo damageInfo)
        {
            ActOnHurt?.Invoke(damageInfo);

            int realDamage = damageInfo.Damage;
            if (damageInfo.IsCrit)
            {
                realDamage *= 2;
            }

            curHp -= realDamage;

            if (curHp <= 0)
            {
                Death();
            }
        }

        /// <summary>
        /// 应用击退
        /// </summary>
        /// <param name="duraction"></param>
        /// <param name="force"></param>
        public void ApplyKnowBack(float duraction, Vector2 force)
        {
            if (knockBackHandle != null)
            {
                StopCoroutine(knockBackHandle);
            }

            knockBackHandle = StartCoroutine(IApplyKnowBack(duraction, force));
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            canMove = true;
            curHp   = maxHp;
        }

        /// <summary>
        /// 重置
        /// </summary>
        public void Reset()
        {
            isDead = false;
        }

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        private IEnumerator IApplyKnowBack(float duraction, Vector2 force)
        {
            canMove = false;
            rb2D.AddForce(force);

            yield return new WaitForSeconds(duraction);
            rb2D.velocity = Vector2.zero;
            canMove       = true;
        }


        // 死亡
        private void Death()
        {
            isDead = true;
            ActOnDeath?.Invoke();
        }

        #endregion

        #region STATIC METHODS
        #endregion
    }
}
#pragma warning restore 0649