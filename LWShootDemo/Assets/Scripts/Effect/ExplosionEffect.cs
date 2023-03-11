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
    public class ExplosionEffect : MonoBehaviour
    {
        #region FIELDS

        [SerializeField]
        private float lifeTime;

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        public void Play()
        {
            var dir = new Vector2(Random.Range(-10, 10), Random.Range(-10, 10)).normalized;
            GameManager.Instance.CameraController.Shake(dir, 4f,
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