/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年3月13日
 * @modify date 2023年3月13日
 * @desc []
 */

#pragma warning disable 0649
using DG.Tweening;
using LWShootDemo.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LWShootDemo.UI
{
    public class UIGameOver : MonoBehaviour
    {
        #region FIELDS

        [SerializeField]
        private TextMeshProUGUI txtSurvivalTime;

        [SerializeField]
        private TextMeshProUGUI txtKillCount;

        [SerializeField]
        private Button btnRestartGame;

        [SerializeField]
        private Button btnBackToMenu;

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        public void Show()
        {
            gameObject.SetActive(true);
            transform.localScale = Vector3.zero;
            transform.DOScale(Vector3.one, 0.22f).SetEase(Ease.OutBack);

            txtSurvivalTime.text = $"共击杀敌人: {GameManager.Instance.KillCount}个";
            txtKillCount.text    = $"存活时间: {GameManager.Instance.GameTime} 秒";
        }

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        private void Awake()
        {
            btnRestartGame.onClick.AddListener(GameManager.Instance.RestartGame);
            btnBackToMenu.onClick.AddListener(GameManager.Instance.BackToMenu);
        }

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649