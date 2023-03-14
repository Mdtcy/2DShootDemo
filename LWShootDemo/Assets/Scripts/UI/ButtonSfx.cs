/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年3月14日
 * @modify date 2023年3月14日
 * @desc [按钮点击音效]
 */

#pragma warning disable 0649
using LWShootDemo.Managers;
using LWShootDemo.Sound;
using UnityEngine;
using UnityEngine.UI;

namespace LWShootDemo.UI
{
    /// <summary>
    /// 按钮点击音效
    /// </summary>
    public class ButtonSfx : MonoBehaviour
    {
        #region FIELDS

        [SerializeField]
        private Button btn;

        [SerializeField]
        private SoundType soundType;

        // local
        private SoundManager soundManager;

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
            soundManager = GameManager.Instance.SoundManager;
        }

        private void Awake()
        {
            btn.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            soundManager.PlaySfx(soundType);
        }

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649