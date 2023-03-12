/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年3月10日
 * @modify date 2023年3月10日
 * @desc []
 */

#pragma warning disable 0649
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

        [SerializeField]
        private ParticleSystem fireParticle;

        [SerializeField]
        private float fireKnockBackForce = 1;

        // local
        private Entity       owener;
        private SoundManager soundManager;
        private CameraController cameraController;

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        public override void Init(Entity entity)
        {
            this.owener = entity;
        }

        public override void Use()
        {
            firePoint.localEulerAngles = new Vector3(0f, 0f, Random.Range(-10f, 10f));
            var firePointPos = firePoint.position;
            Projectile projectile = Instantiate(this.pfbProjectile, firePointPos, firePoint.localRotation)
               .GetComponent<Projectile>();
            projectile.Setup(firePoint.rotation);
            owener.ApplyKnowBack(0.2f, -transform.up * fireKnockBackForce);
            fireParticle.Play();
            soundManager.PlaySfx(SoundType.Fire);
            cameraController.Shake((transform.position - firePointPos).normalized, 0.5f,
                                                        0.05f);
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