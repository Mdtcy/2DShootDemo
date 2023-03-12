/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年3月9日
 * @modify date 2023年3月9日
 * @desc [玩家控制器]
 */

#pragma warning disable 0649
using LWShootDemo.Managers;
using LWShootDemo.Sound;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LWShootDemo.Entities
{
    /// <summary>
    /// 玩家控制器
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        #region FIELDS

        [SerializeField]
        private Entity entity;

        [SerializeField]
        private Rigidbody2D rb2D;

        [SerializeField]
        private float moveSpeed;

        // 攻击间隔
        [SerializeField]
        private float fireRate;

        private Vector2 movement;
        private bool    firing;

        [SerializeField]
        private Transform firePoint;

        [SerializeField]
        private GameObject pfbProjectile;


        // local
        private GlobalEventManager globalEventManager;
        private SoundManager       soundManager;
        private TimeStopManager    timeStopManager;
        private ExplosionGenerator explosionGenerator;
        private Camera             mainCamera;
        private bool               canShoot = true;

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
            globalEventManager = GameManager.Instance.GlobalEventManager;
            soundManager       = GameManager.Instance.SoundManager;
            timeStopManager    = GameManager.Instance.TimeStopManager;
            mainCamera         = GameManager.Instance.MainCamera;
            explosionGenerator = GameManager.Instance.explosionGenerator;

            entity.ActOnDeath += OnDeath;
        }

        private void Update()
        {
            if (isDead)
            {
                return;
            }

            GetInput();
        }
        private void FixedUpdate()
        {
            if (isDead)
            {
                return;
            }

            RotateToMouse();
            TryToShoot();
            Move();
        }


        private Vector3 mouse;
        private float  lastShotTime;

        private void RotateToMouse()
        {
            mouse   = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mouse.z = mainCamera.nearClipPlane;
            Vector2 mouseVector = (mouse - transform.position).normalized;
            float   gunAngle    = Mathf.Atan2(mouseVector.y, mouseVector.x) * Mathf.Rad2Deg - 90f;

            entity.TryRotate(gunAngle);

            // transform.rotation = Quaternion.AngleAxis(GetMouseAngle, Vector3.forward);
        }

        private void Move()
        {
            // 按住开火键的时候移动速度减半
            float curSpeed = firing ? moveSpeed / 3 : moveSpeed;

            entity.TryMove(movement.normalized, curSpeed);

            // var move = this.movement.normalized * curSpeed * Time.fixedDeltaTime;
            // rb2D.MovePosition((Vector2)transform.position + move);
        }

        private void GetInput()
        {
            movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            firing     = Input.GetMouseButton(0);
        }

        [SerializeField]
        private float fireKnockBackForce = 1;


        private void TryToShoot()
        {
            canShoot = lastShotTime + fireRate < Time.time;
            if (firing && canShoot)
            {
                entity.ApplyKnowBack(0.2f, -transform.up * fireKnockBackForce);
                firePoint.localEulerAngles = new Vector3(0f, 0f, Random.Range(-10f, 10f));
                var firePointPos = firePoint.position;
                Projectile projectile = Instantiate(this.pfbProjectile, firePointPos, firePoint.localRotation)
                   .GetComponent<Projectile>();
                projectile.Setup(firePoint.rotation);
                lastShotTime = Time.time;
                GameManager.Instance.CameraController.Shake((transform.position - firePointPos).normalized, 0.5f,
                                                            0.05f);
                fireParticle.Play();
                flashParticle.Play();
                soundManager.PlaySfx(SoundType.Fire);
                globalEventManager.OnShoot();
                lastShotTime = Time.time;
            }
        }

        [SerializeField]
        private ParticleSystem fireParticle;

        [SerializeField]
        private ParticleSystem flashParticle;



        #endregion

        #region STATIC METHODS

        #endregion

        private bool isDead = false;

        public bool IsDead => isDead;

        [SerializeField]
        private Transform pfbDeathPlayer;

        private void OnDeath()
        {
            isDead = true;
            explosionGenerator.CreateExplosion(transform.position);

            var playerDeathEffect = Instantiate(pfbDeathPlayer, transform.position, Quaternion.identity)
               .GetComponent<Effect.Effect>();
            playerDeathEffect.Play();

            Destroy(gameObject);
        }

    }
}
#pragma warning restore 0649