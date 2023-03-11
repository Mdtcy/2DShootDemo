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

namespace LWShootDemo
{
    public class Projectile : MonoBehaviour
    {
        #region FIELDS

        private Vector3 direction;

        [SerializeField]
        private float speed = 10f;

        [SerializeField]
        private Rigidbody2D rb2D;

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        public void Setup(Quaternion direction)
        {
            transform.rotation = direction;
            // this.direction = direction;
            Destroy(gameObject, 5);
        }

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        void FixedUpdate()
        {
            Move();
        }

        void Move()
        {
            // move
            // Vector3 dir = transform.TransformDirection(direction);
            rb2D.velocity = transform.up * speed;
            // rb2D.MovePosition(tempPos);
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
            {
                // collision.GetComponentInParent<IDamageable>().TakeDamage(1);
                // GameObject asd = Instantiate(explosionHolder, transform.position, Quaternion.identity);
                // soundManager.RandomizeSfx(hitSfx);
                Destroy(gameObject);
                collision.GetComponent<Enemy>().Kill();
            }

            if (collision.CompareTag("Obstacles"))
            {
                // GameObject asd = Instantiate(explosionHolder, transform.position, Quaternion.identity);
                // soundManager.RandomizeSfx(hitSfx);
                Destroy(gameObject);
            }
        }

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649