/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年3月9日
 * @modify date 2023年3月9日
 * @desc [玩家控制器]
 */

#pragma warning disable 0649
using Events;
using LWShootDemo.Managers;
using LWShootDemo.Sound;
using LWShootDemo.Weapons;
using Sirenix.OdinInspector;
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
        private Transform pfbCorpse;

        [SerializeField]
        private ParticleSystem deathEffect;

        [SerializeField]
        private Weapon initWeapon;

        [SerializeField]
        private Weapon upgradeWeapon;

        // * local
        private SoundManager     soundManager;
        private TimeStopManager  timeStopManager;

        private Camera  mainCamera;
        private bool    canShoot = true;
        private bool    isDead;
        private Vector2 movement;
        private bool    firing;
        private Vector3 mouse;
        private float   lastShotTime;
        private Weapon  curWeapon;

        #endregion

        #region PROPERTIES

        public bool IsDead => isDead;

        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// 升级武器 todo 临时 待优化
        /// </summary>
        [Button]
        public void ChangeWeapon()
        {
            initWeapon.gameObject.SetActive(false);
            upgradeWeapon.gameObject.SetActive(true);
            curWeapon = upgradeWeapon;
            curWeapon.Init(entity);
        }

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        private void Start()
        {
            mainCamera         = GameManager.Instance.MainCamera;
            soundManager     = GameManager.Instance.SoundManager;
            timeStopManager  = GameManager.Instance.TimeStopManager;

            entity.Init();
            entity.ActOnDeath += OnDeath;

            // 初始化武器
            curWeapon         =  initWeapon;
            initWeapon.gameObject.SetActive(true);
            curWeapon.Init(entity);
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

        private void RotateToMouse()
        {
            mouse   = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mouse.z = mainCamera.nearClipPlane;
            Vector2 mouseVector = (mouse - transform.position).normalized;
            float   gunAngle    = Mathf.Atan2(mouseVector.y, mouseVector.x) * Mathf.Rad2Deg - 90f;

            entity.TryRotate(gunAngle);
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

        private void TryToShoot()
        {
            canShoot = lastShotTime + fireRate < Time.time;
            if (firing && canShoot)
            {
                curWeapon.Use();
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
            // explosionManager.CreateExplosion(transform.position);
            // GameManager.Instance.CameraController.Shake(new Vector2(Random.Range(-10, 10), Random.Range(-10, 10)), 2, 0.6f);
            // deathEffect.Play();

            // 播放音效
            soundManager.PlaySfx(SoundType.Hit);

            // 减缓时间
            timeStopManager.StopTime(0.2f,1f);

            // 粒子特效
            Instantiate(deathEffect, transform.position, Quaternion.identity);

            // 尸体
            Instantiate(pfbCorpse, transform.position, transform.rotation);

            PlayDeathEvent.Trigger();

            Destroy(gameObject);
        }

    }
}
#pragma warning restore 0649