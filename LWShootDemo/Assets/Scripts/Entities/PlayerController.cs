/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年3月9日
 * @modify date 2023年3月9日
 * @desc [玩家控制器]
 */

#pragma warning disable 0649
using LWShootDemo.Effect;
using LWShootDemo.Explosions;
using LWShootDemo.Managers;
using LWShootDemo.Weapons;
using UnityEngine;

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
        private float moveSpeed;

        // 攻击间隔
        [SerializeField]
        private float fireRate;

        [SerializeField]
        private Transform pfbDeathPlayer;


        // * local
        private ExplosionManager explosionManager;
        private Camera             mainCamera;
        private bool               canShoot = true;
        private bool               isDead;
        private Vector2            movement;
        private bool               firing;

        #endregion

        #region PROPERTIES

        public bool IsDead => isDead;

        #endregion

        #region PUBLIC METHODS

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        private void Start()
        {
            mainCamera         = GameManager.Instance.MainCamera;
            explosionManager = GameManager.Instance.explosionManager;

            entity.ActOnDeath += OnDeath;
            weapon.Init(entity);
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
            // // 按住开火键的时候移动速度减半  有后坐力之后不需要这个了
            // float curSpeed = firing ? moveSpeed / 3 : moveSpeed;

            entity.TryMove(movement.normalized, moveSpeed);
        }

        private void GetInput()
        {
            movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            firing     = Input.GetMouseButton(0);
        }

        [SerializeField]
        private Weapon weapon;

        private void TryToShoot()
        {
            canShoot = lastShotTime + fireRate < Time.time;
            if (firing && canShoot)
            {
                weapon.Use();
                lastShotTime = Time.time;
                lastShotTime = Time.time;
            }
        }

        #endregion

        #region STATIC METHODS

        #endregion

        private void OnDeath()
        {
            isDead = true;
            explosionManager.CreateExplosion(transform.position);

            var playerDeathEffect = Instantiate(pfbDeathPlayer, transform.position, Quaternion.identity)
               .GetComponent<PlayerDeathEffect>();
            playerDeathEffect.Play();

            Destroy(gameObject);
        }

    }
}
#pragma warning restore 0649