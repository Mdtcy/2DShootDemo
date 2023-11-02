using Sirenix.OdinInspector;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace GameMain
{
    public class DifficultyComponent : GameFrameworkComponent
    {
        /*
         *  playerFactor = 1 + 0.3 * (playerCount - 1)
            timeFactor = 0.046 * difficultyValue * playerCount 0.2次方
            stageFactor = 1.15 stagesCompleted次方
            coeff = (playerFactor + timeInMinutes * timeFactor) * stageFactor
            在这组公式中，变量playerCount等于游戏中的玩家数量，其中包括在多人模式下不幸断开链接的玩家；
            difficultyValue在不同难度模式下各不相同，在“细雨”中等于1，在“暴雨”里等于2，在“季风”中为3；s
            tagesCompleted 为已完成关卡数，这些关卡也包括除了时空间隙市集的其他隐藏区域，该数字与结算时显示的数目一致；
            timeInMinutes则是一轮游戏开始起每秒计算增加的分钟数。据此，笔者尝试画出单人的“细雨”模式下难度系数的变化图：
         */
        
        // 对应开局选择的不同难度 1 2 3 todo 可以进行选择
        [BoxGroup("难度")]
        [ShowInInspector]
        [ReadOnly]
        [LabelText("难度值")]
        private int _difficultyValue = 1;
        
        // 开局计算
        private float _timeFactor => 0.046f * _difficultyValue;
        
        // 已经完成的关卡数量 todo 需要更新
        [ShowInInspector]
        [ReadOnly]
        [LabelText("已经完成的关卡数量")]
        [BoxGroup("难度")]
        private int _stagesCompleted = 0;
        
        // 单人游戏 这里设置成1就行
        private float PlayerFactor => 1;
        
        [ShowInInspector]
        [ReadOnly]
        [LabelText("游戏运行以来的秒数")]
        [BoxGroup("难度")]
        private float _timeInSeconds;
        private float StageFactor => Mathf.Pow(1.15f, _stagesCompleted);
        
        // 游戏运行以来的分钟数
        [ShowInInspector]
        [LabelText("游戏运行以来的分钟数")]
        [BoxGroup("难度")]
        private float TimeInMinutes => _timeInSeconds / 60f;

        [ShowInInspector]
        [LabelText("难度系数")]
        [BoxGroup("难度")]
        private float Coeff => (PlayerFactor + TimeInMinutes * _timeFactor) * StageFactor;


        [BoxGroup("局内影响")] 
        [LabelText("敌人等级")]
        [ShowInInspector] 
        public float EnemyLevel => 1 + (Coeff - PlayerFactor) / 0.33f;

        [BoxGroup("局内影响")]
        [LabelText("交互物成本乘数")]
        [ShowInInspector]
        public float MoneyCostNum => Mathf.Pow(Coeff, 1.25f);
        
        // [BoxGroup("局内影响")]
        // [LabelText("敌人经验值影响")]
        // [ShowInInspector]
        // public float MoneyCostNum => Mathf.Pow(Coeff, 1.25f);
        
        private void Update()
        {
            _timeInSeconds += Time.deltaTime;
        }

        public float GetMoneyCost(float baseCost)
        {
            return baseCost * MoneyCostNum;
        }
    }
}