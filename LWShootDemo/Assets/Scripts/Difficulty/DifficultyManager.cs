/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年3月12日
 * @modify date 2023年3月12日
 * @desc [难度管理器]
 */

#pragma warning disable 0649
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace LWShootDemo.Difficulty
{
    /// <summary>
    /// 难度管理器
    /// </summary>
    public class DifficultyManager : MonoBehaviour
    {
        #region FIELDS

        // 显示当前难度
        [SerializeField]
        private TextMeshProUGUI txtDifficulty;

        [SerializeField]
        [InlineEditor(InlineEditorModes.FullEditor)]
        private DifficultyConfig config;

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// 获取当前难度
        /// </summary>
        /// <returns></returns>
        public DifficultyConfig.Difficulty GetCurrentDifficulty()
        {
            return config.GetDifficulty((int)GameManager.Instance.GameTime);
        }

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        private void Update()
        {
            txtDifficulty.text = GetCurrentDifficulty().Type.ToString();
        }

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649