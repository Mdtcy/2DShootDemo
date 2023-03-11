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
        private Rigidbody2D rb2D;

        [SerializeField]
        private float moveSpeed;

        private float curSpeed;

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
            soundManager = GameManager.Instance.SoundManager;
            mainCamera         = GameManager.Instance.MainCamera;
        }

        private void Update()
        {
            GetInput();
        }
        private void FixedUpdate()
        {
            Aim();
            TryToShoot();
            Move();
        }

        public float GetMouseAngle { get; private set; }

        private Vector3 mouse;
        private float  lastShotTime;

        private void Aim()
        {
            mouse   = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mouse.z = mainCamera.nearClipPlane;
            Vector2 mouseVector = (mouse - transform.position).normalized;
            float   gunAngle    = Mathf.Atan2(mouseVector.y, mouseVector.x) * Mathf.Rad2Deg - 90f;
            GetMouseAngle      = gunAngle;

            transform.rotation = Quaternion.AngleAxis(GetMouseAngle, Vector3.forward);
        }

        private void Move()
        {
            // 按住开火键的时候移动速度减半
            curSpeed = firing ? moveSpeed / 3 : moveSpeed;

            var move = this.movement.normalized * curSpeed * Time.fixedDeltaTime;
            rb2D.MovePosition((Vector2)transform.position + move);
        }

        private void GetInput()
        {
            movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            firing     = Input.GetMouseButton(0);
        }

        private void TryToShoot()
        {
            canShoot = lastShotTime + fireRate < Time.time;
            if (firing && canShoot)
            {
                firePoint.localEulerAngles = new Vector3(0f, 0f, Random.Range(-10f, 10f));
                var firePointPos = firePoint.position;
                Projectile projectile = Instantiate(this.pfbProjectile, firePointPos, firePoint.localRotation)
                   .GetComponent<Projectile>();
                projectile.Setup(firePoint.rotation);
                lastShotTime = Time.time;
                GameManager.Instance.CameraController.Shake((transform.position - firePointPos).normalized, 0.5f,
                                                            0.05f);
                fireParticle.Play();
                soundManager.PlaySfx(SoundType.Fire);
                globalEventManager.OnShoot();
                lastShotTime = Time.time;
            }
        }

        [SerializeField]
        private ParticleSystem fireParticle;

        #endregion

        #region STATIC METHODS

        #endregion

        public void Kill()
        {
            Destroy(gameObject);
        }
    }
}
#pragma warning restore 0649