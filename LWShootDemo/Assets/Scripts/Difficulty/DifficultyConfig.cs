using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace LWShootDemo.Difficulty
{
    [CreateAssetMenu(fileName = "DifficultyConfig", menuName = "DifficultyConfig", order = 0)]
    public class DifficultyConfig : ScriptableObject
    {
        [Serializable]
        public class Difficulty
        {
            /// <summary>
            /// 难度等级
            /// </summary>
            public DifficultyType Type;

            /// <summary>
            /// 进入该等级的时间
            /// </summary>
            public Vector2 Seconds;

            /// <summary>
            /// 敌人速度乘数
            /// </summary>
            public float EnemySpeedNum;

            /// <summary>
            /// 生成敌人的点数
            /// </summary>
            public int SpawnEnemyPoint;

            /// <summary>
            /// 生成敌人的间隔
            /// </summary>
            public Vector2 EnemySpawnInterval;
        }

        /// <summary>
        /// 难度
        /// </summary>
        public List<Difficulty> Difficulties;

        /// <summary>
        /// 根据时间获取难度
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public Difficulty GetDifficulty(int seconds)
        {
            foreach (var difficulty in Difficulties)
            {
                if (seconds >= difficulty.Seconds.x && seconds < difficulty.Seconds.y)
                {
                    return difficulty;
                }
            }

            return Difficulties.Last();
        }
    }
}