/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年3月9日
 * @modify date 2023年3月9日
 * @desc [玩家控制器]
 */

#pragma warning disable 0649
using Events;
using LWShootDemo.Sound;
using LWShootDemo.TimeStop;
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

        // 实体
        [SerializeField]
        private Entity entity;

        // 移动速度
        [SerializeField]
        private float moveSpeed;

        // 攻击间隔
        [SerializeField]
        private float fireRate;

        // 死亡后的尸体
        [SerializeField]
        private Transform pfbCorpse;

        // 死亡后的特效
        [SerializeField]
        private ParticleSystem deathEffect;

        // 初始武器
        [SerializeField]
        private Weapon initWeapon;

        // 升级后的武器
        [SerializeField]
        private Weapon upgradeWeapon;

        // * local
        private SoundManager     soundManager;
        private TimeStopManager  timeStopManager;

        private Camera  mainCamera;
        private bool    canShoot = true;
        private Vector2 movement;
        private bool    firing;
        private Vector3 mouse;
        private float   lastShotTime;
        private Weapon  curWeapon;

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// 升级武器 todo 临时 展现三枪用 待优化
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
            curWeapon = initWeapon;
            initWeapon.gameObject.SetActive(true);
            curWeapon.Init(entity);
        }

        private void Update()
        {
            if (entity.IsDead)
            {
                return;
            }

            GetInput();
        }
        private void FixedUpdate()
        {
            if (entity.IsDead)
            {
                return;
            }

            // RotateToMouse();
            // TryToShoot();
            Move();
        }

        // 旋转到鼠标位置
        private void RotateToMouse()
        {
            mouse   = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mouse.z = mainCamera.nearClipPlane;
            Vector2 mouseVector = (mouse - transform.position).normalized;
            float   gunAngle    = Mathf.Atan2(mouseVector.y, mouseVector.x) * Mathf.Rad2Deg - 90f;

            entity.TryRotate(gunAngle);
        }

        // 移动
        private void Move()
        {
            // // 按住开火键的时候移动速度减半  有后坐力之后不需要这个了
            // float curSpeed = firing ? moveSpeed / 3 : moveSpeed;

            entity.TryMove(movement.normalized, moveSpeed);
        }

        // 获取输入
        private void GetInput()
        {
            movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            firing     = Input.GetMouseButton(0);
        }

        // 尝试开火
        private void TryToShoot()
        {
            canShoot = lastShotTime + fireRate < Time.time;
            if (firing && canShoot)
            {
                curWeapon.Use();
                lastShotTime = Time.time;
            }
        }

        #endregion

        #region STATIC METHODS

        #endregion

        private void OnDeath()
        {
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