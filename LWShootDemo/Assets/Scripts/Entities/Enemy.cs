/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年3月11日
 * @modify date 2023年3月11日
 * @desc [任务组UI]
 */

#pragma warning disable 0649
using System.Collections;
using LWShootDemo.Effect;
using LWShootDemo.Managers;
using LWShootDemo.Sound;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

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

        [FormerlySerializedAs("matHurt")]
        [SerializeField]
        private Material matFlash;

        [SerializeField]
        private SpriteRenderer spModel;

        private bool flashing;

        // * local
        private Transform       player;
        private SoundManager    soundManager;
        private int             curHp;
        private Coroutine       flashCoroutine;
        private TimeStopManager timeStopManager;

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
            player          = GameManager.Instance.Player;
            soundManager    = GameManager.Instance.SoundManager;
            timeStopManager = GameManager.Instance.TimeStopManager;
            curHp           = maxHp;
            canMove         = true;
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
            if (canMove)
            {
                var direction = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
                rb2D.MovePosition(direction);
            }
        }


        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.CompareTag("Player"))
            {
                other.collider.GetComponent<PlayerController>().Kill();
            }
        }

        // 闪光
        private IEnumerator IFlash(float duration)
        {
            flashing         = true;
            spModel.material = matFlash;

            yield return new WaitForSecondsRealtime(duration);
            spModel.material = matNormal;
            flashing         = false;

        }

        public void TakeDamage(DamageInfo damageInfo)
        {
            ApplyKnowBack(0.2f, damageInfo.Direction);
            soundManager.PlaySfx(SoundType.Hit);
            timeStopManager.StopTime(0.02f);

            if (flashCoroutine != null)
            {
                StopCoroutine(flashCoroutine);
            }

            flashCoroutine = StartCoroutine(IFlash(0.05f));

            curHp -= damageInfo.Damage;

            if (curHp <= 0)
            {
                Kill();
            }
        }

        [ShowInInspector]
        [ReadOnly]
        private bool canMove = true;

        [SerializeField]
        private float force = 1;

        private Coroutine knockBackHandle;

        public void ApplyKnowBack(float duraction, Vector2 direction)
        {
            if (knockBackHandle != null)
            {
                StopCoroutine(knockBackHandle);
            }

            knockBackHandle = StartCoroutine(IApplyKnowBack(duraction, direction));
        }

        private IEnumerator IApplyKnowBack(float duraction, Vector2 direction)
        {
            canMove = false;
            rb2D.AddForce(direction * force);
            yield return new WaitForSeconds(duraction);
            rb2D.velocity = Vector2.zero;
            canMove       = true;
        }

        [SerializeField]
        private Transform pfbExplosion;


        // 击杀
        private void Kill()
        {
            var explosion = Instantiate(pfbExplosion, transform.position, quaternion.identity)
               .GetComponent<ExplosionEffect>();
            explosion.Play();
            Destroy(gameObject);
        }

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649