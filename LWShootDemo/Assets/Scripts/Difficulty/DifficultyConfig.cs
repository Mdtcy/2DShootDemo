using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
            public Vector2 Minutes;

            /// <summary>
            /// 生成敌人的点数
            /// </summary>
            public int EnemyPoint;

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
        /// <param name="minutes"></param>
        /// <returns></returns>
        public Difficulty GetDifficulty(int minutes)
        {
            foreach (var difficulty in Difficulties)
            {
                if (minutes >= difficulty.Minutes.x && minutes <= difficulty.Minutes.y)
                {
                    return difficulty;
                }
            }

            return Difficulties.Last();
        }
    }
}