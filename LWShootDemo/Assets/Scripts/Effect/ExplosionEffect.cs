/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年3月11日
 * @modify date 2023年3月11日
 * @desc [爆炸效果]
 */

#pragma warning disable 0649
using LWShootDemo.Managers;
using LWShootDemo.Sound;
using UnityEngine;

namespace LWShootDemo.Effect
{
    /// <summary>
    /// 爆炸效果
    /// </summary>
    public class ExplosionEffect : Effect
    {
        #region FIELDS

        [SerializeField]
        private float lifeTime;

        [SerializeField]
        private Vector2 shakeIntensity;

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        public override void Play()
        {
            var player = GameManager.Instance.Player;
            // 爆炸带来的震动根据玩家和爆炸点的方向来决定
            var dir    = (transform.position - player.position).normalized;
            GameManager.Instance.CameraController.Shake(dir, Random.Range(shakeIntensity.x, shakeIntensity.y),
                                                        0.05f);
            GameManager.Instance.SoundManager.PlaySfx(SoundType.Explosion);
            Destroy(gameObject, lifeTime);
        }

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649