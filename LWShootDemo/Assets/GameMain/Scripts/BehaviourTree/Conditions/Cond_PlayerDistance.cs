using System;
using LWShootDemo;
using NodeCanvas.Framework;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace GameMain
{
    public class Cond_PlayerDistance : ConditionTask
    {
        public BBParameter<Transform> Target;
        public NumCompareType CompareType;
        public BBParameter<float> Distance;

        protected override bool OnCheck()
        {
            // todo 
            var player = GameManager.Instance.Player;
            float distance = Vector3.Distance(player.transform.position, Target.value.position);
            switch (CompareType)
            {
                case NumCompareType.Greater:
                    return distance > Distance.value;
                case NumCompareType.GreaterOrEqual:
                    return distance >= Distance.value;
                case NumCompareType.Equal:
                    return Math.Abs(distance - Distance.value) < float.Epsilon;
                case NumCompareType.LessOrEqual:
                    return distance <= Distance.value;
                case NumCompareType.Less:
                    return distance < Distance.value;
                default:
                    Log.Error($"未定义的CompareType{CompareType}");
                    return false;
            }
        }
    }
}