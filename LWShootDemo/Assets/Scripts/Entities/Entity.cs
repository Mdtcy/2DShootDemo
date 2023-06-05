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
using LWShootDemo.BuffSystem.Buffs;
using LWShootDemo.BuffSystem.Event;
using LWShootDemo.Motion;
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

        [LabelText("Buff")]
        [SerializeField]
        private BuffComponent _buffComponent;

        [SerializeField] 
        private MovementComponent _movementComponent;
        
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

        // /// <summary>
        // /// 受伤事件
        // /// </summary>
        // public Action<DamageInfo> ActOnHurt;

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

        private void Update()
        {
            _buffComponent.UpdateBuff(Time.deltaTime);
        }

        #region PUBLIC METHODS

        /// <summary>
        /// 尝试旋转
        /// </summary>
        /// <param name="angle"></param>
        public void TryRotate(float angle)
        {
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }


        public void PlayMotionClip(MotionClip clip)
        {
            _movementComponent.PlayMotionClip(clip);
        }

        public void InputMove(Vector2 input)
        {
            _movementComponent.InputMove(input);
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

        public void AddBuff(AddBuffInfo addBuffInfo)
        {
            _buffComponent.AddBuff(addBuffInfo);
        }

        public void TriggerBuff<TBuffEvent, TEventActArgs>(TEventActArgs args) where TBuffEvent : BuffEvent<TEventActArgs> where TEventActArgs : class, IEventActArgs
        {
            _buffComponent.TriggerEvent<TBuffEvent, TEventActArgs>(args);
        }

        #endregion

        #region STATIC METHODS
        #endregion

        ///<summary>
        ///角色的无敌状态持续时间，如果在无敌状态中，子弹不会碰撞，DamageInfo处理无效化
        ///单位：秒
        ///</summary>
        public float ImmuneTime
        {
            get => _immuneTime;
            set => _immuneTime = Mathf.Max(_immuneTime, value);
        }
        private float _immuneTime = 0.00f;
        
        public bool CanBeKilledByDamageInfo(DamageInfo damageInfo)
        {
            if (this.ImmuneTime > 0 || damageInfo.IsHeal() == true)
            {
                return false;
            }

            int dValue = damageInfo.DamageValue(false);
            // return dValue >= this.resource.hp;
            return dValue >= this.curHp;
        }

        public void TakeDamage(int damage)
        { 
            // ActOnHurt?.Invoke(damageInfo);
            curHp -= damage;
            
            if (curHp <= 0)
            {
                Death();
            }
        }
    }
}
#pragma warning restore 0649