/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年3月9日
 * @modify date 2023年3月9日
 * @desc [玩家控制器]
 */

#pragma warning disable 0649
using Events;
using GameMain;
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
        private Character _character;

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
            curWeapon.Init(_character);
        }

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        private void Start()
        {
            soundManager     = GameManager.Instance.SoundManager;
            timeStopManager  = GameManager.Instance.TimeStopManager;
            

            // 初始化武器
            curWeapon = initWeapon;
            initWeapon.gameObject.SetActive(true);
            curWeapon.Init(_character);
        }

        public void Init(Character character)
        {
            _character = character;
        }

        private void Update()
        {
            // if (_oldEntity.IsDead)
            // {
            //     return;
            // }
            //
            // GetInput();
        }
        private void FixedUpdate()
        {
            // if (_oldEntity.IsDead)
            // {
            //     return;
            // }
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