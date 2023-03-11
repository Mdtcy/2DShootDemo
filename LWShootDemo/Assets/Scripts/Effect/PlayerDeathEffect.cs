/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年3月11日
 * @modify date 2023年3月11日
 * @desc [任务组UI]
 */

#pragma warning disable 0649
using DG.Tweening;
using UnityEngine;

namespace LWShootDemo.Effect
{
    public class PlayerDeathEffect : Effect
    {
        #region FIELDS

        [SerializeField]
        private float duration = 1;

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        public override void Play()
        {
            var sequence    = DOTween.Sequence();
            var scaleTween  = transform.DOScale(50, duration).SetEase(Ease.InOutSine);
            var rotateTween = transform.DORotate(new Vector3(0, 0, 360), duration, RotateMode.FastBeyond360);
            sequence.Insert(0, scaleTween);
            sequence.Insert(0, rotateTween);
            sequence.Play();
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