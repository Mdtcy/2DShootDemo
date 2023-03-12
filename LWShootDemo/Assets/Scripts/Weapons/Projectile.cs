/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年3月9日
 * @modify date 2023年3月9日
 * @desc [任务组UI]
 */

#pragma warning disable 0649
using LWShootDemo.Entities;
using UnityEngine;

namespace LWShootDemo.Weapons
{
    public class Projectile : MonoBehaviour
    {
        #region FIELDS

        private Vector3 direction;

        [SerializeField]
        private float speed = 10f;

        [SerializeField]
        private float critChance = 0.2f;

        [SerializeField]
        private Rigidbody2D rb2D;

        private float spawnTime;
        private bool  isDead;

        #endregion

        #region PROPERTIES

        public float SpawnTime => spawnTime;

        public bool IsDead => isDead;

        #endregion

        #region PUBLIC METHODS

        public void Init(float spawnTime)
        {
            this.spawnTime = spawnTime;
            isDead         = false;
        }

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        public void Move()
        {
            rb2D.velocity = transform.up * speed;
            // rb2D.MovePosition(tempPos);
        }


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

            if (entity.Side != Side.Player)
            {
                var dir = (collision.transform.position - transform.position).normalized;
                isDead = true;
                var damageInfo = new DamageInfo(1, dir, Random.value < critChance);
                collision.GetComponent<Entity>().TakeDamage(damageInfo);
            }
        }

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649