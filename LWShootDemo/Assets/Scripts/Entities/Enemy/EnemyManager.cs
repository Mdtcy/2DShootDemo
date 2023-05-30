/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年3月11日
 * @modify date 2023年3月11日
 * @desc [敌人管理器 负责敌人的生成和管理]
 */

#pragma warning disable 0649
using System;
using System.Collections.Generic;
using Entities.Enemy;
using GameFramework.ObjectPool;
using LWShootDemo.Difficulty;
using LWShootDemo.Pool;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace LWShootDemo.Entities.Enemy
{
    /// <summary>
    /// 敌人管理器 负责敌人的生成和管理
    /// </summary>
    public class EnemyManager : MonoBehaviour
    {
        [Serializable]
        public class EnemySpawnSetting
        {
            public Transform Prefab;

            public int SpawnPoint;
        }

        #region FIELDS

        [SerializeField]
        private Vector2 spawnArea;

        [SerializeField]
        private bool debugMode;

        [SerializeField]
        private List<EnemySpawnSetting> _enemySpawnSettings;
        
        

        // local
        private float             spawnTimer;
        private Transform         player;
        private DifficultyManager difficultyManager;

        private List<EnemyController> enemys = new List<EnemyController>();
        private IObjectPool<EnemyObject> _enemyObjectPool = null;

        // * inject
        [Inject]
        private IObjectPoolManager _objectPoolManager;


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
            player            = GameManager.Instance.Player;
            difficultyManager = GameManager.Instance.DifficultyManager;
            _enemyObjectPool = _objectPoolManager.CreateSingleSpawnObjectPool<EnemyObject>("Enemy", 50);
        }

        private void Update()
        {
            if (player == null)
            {
                return;
            }

            if (spawnTimer < 0)
            {
                var difficulty = difficultyManager.GetCurrentDifficulty();

                SpawnEnemy(difficulty.SpawnEnemyPoint);

                // 重置计时器
                spawnTimer = Random.Range(difficulty.EnemySpawnInterval.x, difficulty.EnemySpawnInterval.y);
            }
            else
            {
                spawnTimer -= Time.deltaTime;
            }
        }

        private void FixedUpdate()
        {
            for (int index = enemys.Count - 1; index >= 0; index--)
            {
                var enemy = enemys[index];

                if (enemy.IsDead)
                {
                    enemys.Remove(enemy);
                    _enemyObjectPool.Unspawn(enemy.gameObject);
                }
                else
                {
                    enemy.EnemyUpdate();
                }
            }
        }
        
        // 根据生成点数生成敌人 每一波有一个生成点数 每个难度对应一个生成点数，每次生成一个敌人消耗生成点数，生成点数花完就不能再生成了，可以生成超过生成点数的敌人
        private void SpawnEnemy(int spawnPoint)
        {
            while (spawnPoint > 0)
            {
                var enemySpawnSetting = _enemySpawnSettings[Random.Range(0, _enemySpawnSettings.Count)];
                spawnPoint -= enemySpawnSetting.SpawnPoint;

                // todo 
                var enemy = CreateEnemy(enemySpawnSetting.Prefab.gameObject);
                enemy.transform.position = GetRandomSpawnPosition();

                var enemyController = enemy.GetComponent<EnemyController>();
                enemyController.Setup(enemySpawnSetting.Prefab.name);
                enemys.Add(enemyController);
            }
        }
        
        // todo 
        [Inject]
        private DiContainer _container;
        
        private GameObject CreateEnemy(GameObject pfbEnemy)
        {
            GameObject enemy = null;
            EnemyObject enemyObject = _enemyObjectPool.Spawn(pfbEnemy.name);
            if (enemyObject != null)
            {
                enemy = (GameObject)enemyObject.Target;
            }
            else
            {
                enemy = _container.InstantiatePrefab(pfbEnemy);
                _enemyObjectPool.Register(EnemyObject.Create(enemy), true);
            }

            return enemy;
        }

        // 在玩家周围的生成区域随机生成一个位置，保证在围绕玩家的一个矩形上生成敌人
        private Vector3 GetRandomSpawnPosition()
        {
            Vector3 pos = Vector3.zero;

            if(Random.value < 0.5f)
            {
                pos.x =  Random.Range(-spawnArea.x, spawnArea.x);
                pos.y = Random.value < 0.5f ? -spawnArea.y : spawnArea.y;
            }
            else
            {
                pos.y = Random.Range(-spawnArea.y, spawnArea.y);
                pos.x = Random.value < 0.5f ? -spawnArea.x : spawnArea.x;
            }

            pos   += player.position;
            pos.z =  0;

            return pos;
        }

        // * debug 绘制出生成区域
        private void OnDrawGizmos()
        {
            if (!debugMode)
            {
                return;
            }

            if (player == null)
            {
                var players = GameObject.FindGameObjectsWithTag("Player");
                if(players.Length == 0)
                {
                    return;
                }

                player = players[0].transform;
            }

            Gizmos.color = Color.white;
            Vector3 pos = player.position;

            Gizmos.DrawWireCube(pos,  2 * new Vector3(spawnArea.x, spawnArea.y, 0f));
        }

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649