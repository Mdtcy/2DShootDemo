/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年3月11日
 * @modify date 2023年3月11日
 * @desc [任务组UI]
 */

#pragma warning disable 0649
using System.Collections;
using LWShootDemo.Explosions;
using LWShootDemo.Managers;
using LWShootDemo.Popups;
using LWShootDemo.Sound;
using UnityEngine;

namespace LWShootDemo.Entities
{
    [RequireComponent(typeof(Entity))]
    public class EnemyController : MonoBehaviour
    {
        #region FIELDS

        [SerializeField]
        private Entity entity;

        [SerializeField]
        private Material matNormal;

        [SerializeField]
        private Material matFlash;

        [SerializeField]
        private float knockBackForce = 1;

        [SerializeField]
        private SpriteRenderer spModel;

        [SerializeField]
        private float moveSpeed;

        private bool flashing;

        // * local
        private Transform          player;
        private SoundManager       soundManager;
        private Coroutine          flashCoroutine;
        private TimeStopManager    timeStopManager;
        private ExplosionManager explosionManager;
        private PopupManager       popupManager;

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
            player             = GameManager.Instance.Player;
            soundManager       = GameManager.Instance.SoundManager;
            timeStopManager    = GameManager.Instance.TimeStopManager;
            explosionManager = GameManager.Instance.explosionManager;
            popupManager       = GameManager.Instance.PopupManager;

            entity.ActOnHurt  += OnHurt;
            entity.ActOnDeath += OnDeath;
        }


        private void OnDeath()
        {
            explosionManager.CreateExplosion(transform.position);
        }

        private void OnHurt(DamageInfo damageInfo)
        {
            entity.ApplyKnowBack(0.2f, damageInfo.Direction * knockBackForce);


            popupManager.Create(transform.position, damageInfo.IsCrit? PopupType.CriticalDamage : PopupType.NormalDamage);
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
            entity.TryMove(direction, moveSpeed);
        }


        private void OnCollisionEnter2D(Collision2D other)
        {
            var entity = other.collider.GetComponent<Entity>();
            if (entity!=null && entity.Side == Side.Player)
            {
                var damageInfo = new DamageInfo(1, entity.transform.position - transform.position, false);
                entity.TakeDamage(damageInfo);
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