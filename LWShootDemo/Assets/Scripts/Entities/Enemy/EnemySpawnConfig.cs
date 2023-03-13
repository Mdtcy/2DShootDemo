/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年3月12日
 * @modify date 2023年3月12日
 * @desc [任务组UI]
 */

#pragma warning disable 0649
using System;
using System.Collections.Generic;
using LWShootDemo.Pool;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LWShootDemo.Entities.Enemy
{
    [CreateAssetMenu(fileName = "EnemySpawnConfig", menuName = "EnemySpawnConfig", order = 0)]
    public class EnemySpawnConfig : ScriptableObject
    {
        [Serializable]
        public class EnemyConfig
        {
            public SimpleUnitySpawnPool EnemyPool;

            public int SpawnPoint;
        }

        public List<EnemyConfig> EnemyConfigs;

        public EnemyConfig GetRandomEnemyConfig()
        {
            return EnemyConfigs[Random.Range(0, EnemyConfigs.Count)];
        }
    }
}
#pragma warning restore 0649