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
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

namespace LWShootDemo.Entities
{
    [RequireComponent(typeof(Entity))]
    public class Enemy : MonoBehaviour
    {
        #region FIELDS

        [SerializeField]
        private int maxHp;

        [SerializeField]
        private Entity entity;

        [SerializeField]
        private Material matNormal;

        [FormerlySerializedAs("matHurt")]
        [SerializeField]
        private Material matFlash;

        [SerializeField]
        private float knockBackForce = 1;

        [SerializeField]
        private SpriteRenderer spModel;

        private bool flashing;

        // * local
        private Transform       player;
        private SoundManager    soundManager;
        private int             curHp;
        private Coroutine       flashCoroutine;
        private TimeStopManager timeStopManager;
        private ExplosionGenerator explosionGenerator;

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
            explosionGenerator = GameManager.Instance.explosionGenerator;

            curHp           = maxHp;

            entity.ActOnHurt  += OnHurt;
            entity.ActOnDeath += OnDeath;
        }


        private void OnDeath()
        {
           GameManager.Instance.explosionGenerator.CreateExplosion(transform.position);
        }

        private void OnHurt(DamageInfo damageInfo)
        {
            entity.ApplyKnowBack(0.2f, damageInfo.Direction * knockBackForce);
            soundManager.PlaySfx(SoundType.Hit);
            timeStopManager.StopTime(0f, 0.02f);

            if (flashCoroutine != null)
            {
                StopCoroutine(flashCoroutine);
            }

            flashCoroutine = StartCoroutine(IFlash(0.05f));
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

            entity.TryRotate(lookAngle);
        }

        private void ChaseTarget()
        {
            var direction = player.position - transform.position;
            entity.TryMove(direction);
        }


        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.CompareTag("Player"))
            {
                // other.collider.GetComponent<PlayerController>().Kill();
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

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649