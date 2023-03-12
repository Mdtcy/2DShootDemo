/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年3月12日
 * @modify date 2023年3月12日
 * @desc [任务组UI]
 */

#pragma warning disable 0649
using DG.Tweening;
using UnityEngine;

namespace LWShootDemo.Popups
{
    public class Popup : MonoBehaviour
    {
        #region FIELDS

        [SerializeField]
        private Ease moveEase;

        [SerializeField]
        private Ease scaleEase;

        [SerializeField]
        private float endScale;

        [SerializeField]
        private float endY;

        [SerializeField]
        private float duration;


        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        public void Play()
        {
            transform.localScale = Vector3.one;
            var sequence   = DOTween.Sequence();
            var moveTween  = transform.DOLocalMoveY(transform.position.y +endY, duration).SetEase(moveEase);
            var scaleTween = transform.DOScale(endScale, duration).SetEase(scaleEase);
            sequence.Insert(0, scaleTween);
            sequence.Insert(0, moveTween);
            sequence.onComplete += OnCompleted;
            sequence.Play();
        }

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        private void OnCompleted()
        {
            Destroy(gameObject);
        }

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649