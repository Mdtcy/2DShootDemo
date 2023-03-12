/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年3月9日
 * @modify date 2023年3月9日
 * @desc [游戏管理器]
 */

#pragma warning disable 0649
using LWShootDemo.Common;
using LWShootDemo.Difficulty;
using LWShootDemo.Explosions;
using LWShootDemo.Popups;
using LWShootDemo.Sound;
using LWShootDemo.Weapons;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LWShootDemo.Managers
{
    /// <summary>
    /// 游戏管理器
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        #region FIELDS

        // 主相机
        [SerializeField]
        private Camera mainCamera;

        [SerializeField]
        private Transform player;

        // * local
        public SoundManager       SoundManager;
        public CameraController   CameraController;
        public TimeStopManager    TimeStopManager;
        public PopupManager       PopupManager;
        public ExplosionManager   explosionManager;
        public DifficultyManager  DifficultyManager;
        public ProjectileManager  ProjectileManager;

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

        /// <summary>
        /// 角色
        /// </summary>
        public Transform Player => player;


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

        private void Start()
        {
            SoundManager.PlayMusic(SoundType.BattleMusic);
        }

        [Button]
        public void RestartGame()
        {
            SceneManager.LoadScene("Game");
        }


        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649