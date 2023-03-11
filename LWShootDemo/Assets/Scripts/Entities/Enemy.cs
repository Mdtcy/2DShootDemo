/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年3月11日
 * @modify date 2023年3月11日
 * @desc [任务组UI]
 */

#pragma warning disable 0649
using System;
using System.Collections;
using LWShootDemo.Managers;
using LWShootDemo.Sound;
using UnityEngine;

namespace LWShootDemo.Entities
{
    public class Enemy : MonoBehaviour
    {
        #region FIELDS

        [SerializeField]
        private int maxHp;

        [SerializeField]
        private float moveSpeed;

        [SerializeField]
        private Rigidbody2D rb2D;

        [SerializeField]
        private Material matNormal;

        [SerializeField]
        private Material matHurt;

        [SerializeField]
        private SpriteRenderer spModel;

        private bool flashing;



        // * local
        private Transform    player;
        private SoundManager soundManager;
        private int          curHp;

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        private void Start()
        {
            player       = GameManager.Instance.Player;
            soundManager = GameManager.Instance.SoundManager;
            curHp        = maxHp;
        }

        private void FixedUpdate()
        {
            if (player == null)
            {
                return;
            }

            LookAtTarget();
            ChaseTarget();
        }

        private void LookAtTarget()
        {
            Transform target         = player.transform;
            Vector2   targetPosition = target.position - transform.position;
            float    lookAngle      = Vector2.Angle(Vector2.up, targetPosition);

            if (targetPosition.x > 0)
            {
                lookAngle = 360 - lookAngle;
            }

            Quaternion quat = Quaternion.identity;
            quat.eulerAngles   = new Vector3(0, 0, lookAngle);
            transform.rotation = quat;
        }

        private void ChaseTarget()
        {
            var direction = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            rb2D.MovePosition(direction);
        }


        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.CompareTag("Player"))
            {
                other.collider.GetComponent<PlayerController>().Kill();
            }
        }

        #endregion

        #region STATIC METHODS

        #endregion

        private Coroutine flashCoroutine;

        public void TakeDamage(int damage)
        {
            GameManager.Instance.TimeStopManager.StopTime(0.02f);

            if (flashCoroutine!=null)
            {
                StopCoroutine(flashCoroutine);
            }

            flashCoroutine = StartCoroutine(IFlash(0.05f));

            curHp -= damage;
            if (curHp <= 0)
            {
                Kill();
            }
        }


        private IEnumerator IFlash(float duration)
        {
            flashing         = true;
            spModel.material = matHurt;

            yield return new WaitForSecondsRealtime(duration);
            spModel.material = matNormal;
            flashing        = false;

        }

        public void Kill()
        {
            Destroy(gameObject);
        }
    }
}
#pragma warning restore 0649