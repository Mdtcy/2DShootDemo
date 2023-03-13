/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年3月9日
 * @modify date 2023年3月9日
 * @desc [游戏管理器]
 */

#pragma warning disable 0649
using System;
using System.Collections;
using Events;
using LWShootDemo.Common;
using LWShootDemo.Difficulty;
using LWShootDemo.Explosions;
using LWShootDemo.Popups;
using LWShootDemo.Sound;
using LWShootDemo.UI;
using LWShootDemo.Weapons;
using Sirenix.OdinInspector;
using TMPro;
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

        [SerializeField]
        private TextMeshProUGUI txtTime;

        [SerializeField]
        private UIGameOver uiGameOver;

        // * local
        public SoundManager       SoundManager;
        public CameraController   CameraController;
        public TimeStopManager    TimeStopManager;
        public PopupManager       PopupManager;
        public ExplosionManager   explosionManager;
        public DifficultyManager  DifficultyManager;
        public ProjectileManager  ProjectileManager;

        private float gameTime;
        private bool  isGameOver;
        private int   killCount;

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

        public float GameTime  => gameTime;
        public int   KillCount => killCount;

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
            PlayDeathEvent.Register(OnPlayerDeath);
            EnemyDeathEvent.Register(OnEnemyDeath);
        }

        private void OnEnemyDeath()
        {
            killCount++;
        }

        private void OnDestroy()
        {
            PlayDeathEvent.Unregister(OnPlayerDeath);
            EnemyDeathEvent.Unregister(OnEnemyDeath);
        }

        private void Update()
        {
            if (isGameOver)
            {
                return;
            }

            gameTime += Time.deltaTime;
            TimeSpan timeSpan = TimeSpan.FromSeconds(gameTime);
            txtTime.text = $"{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
        }

        private void OnPlayerDeath()
        {
            StartCoroutine(ShowGameOverDelay());
            isGameOver = true;
        }

        private IEnumerator ShowGameOverDelay()
        {
            yield return new WaitForSeconds(1f);
            uiGameOver.Show();
        }

        [Button]
        public void RestartGame()
        {
            SceneManager.LoadScene("Game");
        }

        public void BackToMenu()
        {
            SceneManager.LoadScene("Menu");
        }

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649