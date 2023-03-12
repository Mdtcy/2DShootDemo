/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年3月11日
 * @modify date 2023年3月11日
 * @desc [敌人生成器]
 */

#pragma warning disable 0649
using LWShootDemo.Difficulty;
using LWShootDemo.Entities.Enemy;
using LWShootDemo.Managers;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LWShootDemo.Entities
{
    /// <summary>
    /// 敌人生成器
    /// </summary>
    public class EnemySpawner : MonoBehaviour
    {
        #region FIELDS

        [SerializeField]
        private Vector2 spawnArea;

        [SerializeField]
        private bool debugMode;

        [SerializeField]
        [InlineEditor(InlineEditorModes.FullEditor)]
        private EnemySpawnConfig enemySpawnConfig;

        // local
        private float             spawnTimer;
        private Transform         player;
        private DifficultyManager difficultyManager;

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

        // 根据生成点数生成敌人 生成点数花完就不能再生成了，可以生成超过生成点数的敌人
        private void SpawnEnemy(int spawnPoint)
        {
            while (spawnPoint > 0)
            {
                var enemyConfig = enemySpawnConfig.GetRandomEnemyConfig();
                spawnPoint -= enemyConfig.SpawnPoint;
                Instantiate(enemyConfig.PfbEnemy, GetRandomSpawnPosition(), Quaternion.identity);
            }
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