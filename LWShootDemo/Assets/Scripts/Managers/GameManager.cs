/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年3月9日
 * @modify date 2023年3月9日
 * @desc [游戏管理器]
 */

#pragma warning disable 0649
using UnityEngine;

namespace LWShootDemo.Managers
{
    /// <summary>
    /// 游戏管理器
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        #region FIELDS

        /// <summary>
        /// 主相机
        /// </summary>
        [SerializeField]
        private Camera mainCamera;

        #endregion

        #region PROPERTIES

        public static GameManager Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// 主相机
        /// </summary>
        public Camera MainCamera => mainCamera;

        #endregion

        #region PUBLIC METHODS

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("[GameManager]同时只能存在一个GameManager");

                return;
            }

            Instance = this;
        }

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649