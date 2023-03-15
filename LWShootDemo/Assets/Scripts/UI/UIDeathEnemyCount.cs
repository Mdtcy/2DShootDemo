/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年3月14日
 * @modify date 2023年3月14日
 * @desc [显示死亡敌人数量]
 */

#pragma warning disable 0649
using DG.Tweening;
using Events;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace LWShootDemo.UI
{
    /// <summary>
    /// 显示死亡敌人数量
    /// </summary>
    public class UIDeathEnemyCount : MonoBehaviour
    {
        #region FIELDS

        [SerializeField]
        private TextMeshProUGUI txtCount;

        [SerializeField]
        private float targetScale;

        [SerializeField]
        private float duration;

        // local
        private int      count;
        private Sequence sequence;

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        private void Start()
        {
            EnemyDeathEvent.Register(OnEnemyDeath);
            txtCount.text = "0";
        }

        private void OnDestroy()
        {
            EnemyDeathEvent.Unregister(OnEnemyDeath);
        }

        [Button]
        private void OnEnemyDeath()
        {
            count++;
            txtCount.text = count.ToString();

            sequence.Kill();
            sequence      = DOTween.Sequence();
            var scaleTween = transform.DOScale(targetScale, duration).SetEase(Ease.InSine);
            var resetTween = transform.DOScale(1, duration).SetEase(Ease.OutSine);
            sequence.Append(scaleTween);
            sequence.Append(resetTween);
            sequence.Play();
        }

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649