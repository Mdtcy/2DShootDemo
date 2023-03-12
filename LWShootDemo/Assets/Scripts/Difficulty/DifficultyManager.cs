/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年3月12日
 * @modify date 2023年3月12日
 * @desc [难度管理器]
 */

#pragma warning disable 0649
using System;
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

        [SerializeField]
        private TextMeshProUGUI txtTime;

        [SerializeField]
        private TextMeshProUGUI txtDifficulty;

        [SerializeField]
        [InlineEditor(InlineEditorModes.FullEditor)]
        private DifficultyConfig config;

        // local
        private float time;

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
            Debug.Log($"time = {time}");
            return config.GetDifficulty((int)time);
        }

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        void Update()
        {
            time += Time.deltaTime;
            TimeSpan timeSpan = TimeSpan.FromSeconds(time);
            txtTime.text       = $"{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
            txtDifficulty.text = GetCurrentDifficulty().Type.ToString();
        }

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649