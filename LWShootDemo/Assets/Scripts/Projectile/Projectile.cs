/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年3月9日
 * @modify date 2023年3月9日
 * @desc [子弹]
 */

#pragma warning disable 0649
using Damages;
using GameFramework.ObjectPool;
using LWShootDemo.Entities;
using LWShootDemo.Pool;
using UnityEngine;
using Zenject;

namespace LWShootDemo.Weapons
{
    /// <summary>
    /// 子弹
    /// </summary>
    public class Projectile : MonoBehaviour
    {
        #region FIELDS

        // 方向
        private Vector3 direction;

        // 速度
        [SerializeField]
        private float speed = 10f;

        // 伤害
        [SerializeField]
        private int damage = 1;

        // 暴击概率
        [SerializeField]
        private float critChance = 0.2f;

        [SerializeField]
        private Rigidbody2D rb2D;

        // * local

        // 子弹生成时间
        private float spawnTime;

        // 是否死亡（目前是碰撞后即标记为死亡）
        private bool  isDead;

        #endregion

        #region PROPERTIES

        /// <summary>
        /// 子弹生成时间
        /// </summary>
        public float SpawnTime => spawnTime;

        /// <summary>
        /// 是否死亡（目前是碰撞后即标记为死亡）
        /// </summary>
        public bool IsDead => isDead;
        
        private Entity caster;

        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// caster
        /// </summary>
        /// <param name="caster"></param>
        /// <param name="spawnTime"></param>
        public void Init(Entity caster, float spawnTime)
        {
            this.caster     = caster;
            this.spawnTime = spawnTime;
            isDead         = false;
        }

        /// <summary>
        /// 移动
        /// </summary>
        public void Move()
        {
            rb2D.velocity = transform.up * speed;
        }

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS
        
        [Inject]
        private IDamageManager _damageManager;

        // 撞到非主角阵营时，造成1伤害
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (isDead)
            {
                return;
            }

            var entity = collision.GetComponent<Entity>();

            if (entity == null)
            {
                return;
            }

            if (entity.Side != caster.Side)
            {
                var dir = (collision.transform.position - transform.position).normalized;
                isDead = true;
                _damageManager.DoDamage(caster,entity, damage, dir, 0,new DamageInfoTag[]{});
                // var damageInfo = new DamageInfo(damage, dir, Random.value < critChance);
                // collision.GetComponent<Entity>().TakeDamage(damageInfo);
                
            }
        }

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649