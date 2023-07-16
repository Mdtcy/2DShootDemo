/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年3月10日
 * @modify date 2023年3月10日
 * @desc [发射子弹的武器]
 */

#pragma warning disable 0649
using DG.Tweening;
using GameMain;
using LWShootDemo.Common;
using LWShootDemo.Entities;
using LWShootDemo.Sound;
using UnityEngine;

namespace LWShootDemo.Weapons
{
    /// <summary>
    /// 发射子弹的武器
    /// </summary>
    public class ProjectileWeapon : Weapon
    {
        #region FIELDS

        // 发射点
        [SerializeField]
        private Transform firePoint;

        // 粒子特效 闪光 弹壳
        [SerializeField]
        private ParticleSystem fireParticle;

        // 图片模型
        [SerializeField]
        private SpriteRenderer model;

        // 开火的作用力
        [SerializeField]
        private float fireKnockBackForce = 1;

        // * local
        private Character            owener;
        private SoundManager      soundManager;
        // private CameraController  cameraController;
        private Tween             scaleTween;

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        public override void Init(Character character)
        {
            owener = character;
        }

        public override void Use()
        {
            // 随机旋转一下枪口，可以让子弹看起来更随机
            // firePoint.localEulerAngles = new Vector3(0f, 0f, Random.Range(-10f, 10f));
            var firePointPos = firePoint.position;

            // 生成子弹
            GameEntry.Projectile.CreateProjectile(owener, firePointPos, firePoint.rotation);

            // todo 对持有者产生后坐力 
            // owener.ApplyKnowBack(0.2f, -transform.up * fireKnockBackForce);

            // 开枪时缩放突出枪口震动
            scaleTween.Kill();
            scaleTween = model.transform.DOScale(2, 0.1f).OnComplete(() => model.transform.DOScale(1, 0.1f));

            // 开火粒子特效，包括枪口火焰和弹壳
            fireParticle.Play();

            // 音效
            soundManager.PlaySfx(SoundType.Fire);

            // 屏幕震动
            // cameraController.Shake((transform.position - firePointPos).normalized, 0.2f, 0.05f);
        }

        public override void RotateTo(Vector3 dir)
        {
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
            // 武器朝向敌人
            transform.rotation = Quaternion.Euler(0, 0, angle);

            if (dir.x > 0)
            {
                model.flipY = false;
            }
            else if(dir.x < 0)
            {
                model.flipY = true;
            }
        }

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        void Start()
        {
            soundManager = GameManager.Instance.SoundManager;
            // cameraController = GameManager.Instance.CameraController;
        }

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649