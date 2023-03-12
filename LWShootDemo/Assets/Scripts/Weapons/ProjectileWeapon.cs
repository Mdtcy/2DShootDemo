/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年3月10日
 * @modify date 2023年3月10日
 * @desc []
 */

#pragma warning disable 0649
using DG.Tweening;
using LWShootDemo.Common;
using LWShootDemo.Entities;
using LWShootDemo.Managers;
using LWShootDemo.Sound;
using UnityEngine;

namespace LWShootDemo.Weapons
{
    public class ProjectileWeapon : Weapon
    {
        #region FIELDS

        [SerializeField]
        private Transform firePoint;

        [SerializeField]
        private GameObject pfbProjectile;

        // 粒子特效 闪光 弹壳
        [SerializeField]
        private ParticleSystem fireParticle;

        [SerializeField]
        private Transform model;

        [SerializeField]
        private float fireKnockBackForce = 1;

        // * local
        private Entity           owener;
        private SoundManager     soundManager;
        private CameraController cameraController;
        private Tween            scaleTween;

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        public override void Init(Entity entity)
        {
            owener = entity;
        }

        /// <summary>
        /// 使用
        /// </summary>
        public override void Use()
        {
            // 随机旋转一下枪口，可以让子弹看起来更随机
            firePoint.localEulerAngles = new Vector3(0f, 0f, Random.Range(-10f, 10f));
            var firePointPos = firePoint.position;

            // 生成子弹
            Projectile projectile = Instantiate(pfbProjectile, firePointPos, firePoint.localRotation)
               .GetComponent<Projectile>();
            projectile.Setup(firePoint.rotation);

            // 对持有者产生后坐力
            owener.ApplyKnowBack(0.2f, -transform.up * fireKnockBackForce);

            // 开枪时缩放突出枪口震动
            scaleTween.Kill();
            scaleTween = model.DOScale(2, 0.1f).OnComplete(() => model.DOScale(1, 0.1f));

            // 开火粒子特效，包括枪口火焰和弹壳
            fireParticle.Play();

            // 音效
            soundManager.PlaySfx(SoundType.Fire);

            // 屏幕震动
            cameraController.Shake((transform.position - firePointPos).normalized, 0.5f, 0.05f);
        }

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        void Start()
        {
            soundManager = GameManager.Instance.SoundManager;
            cameraController = GameManager.Instance.CameraController;
        }

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649