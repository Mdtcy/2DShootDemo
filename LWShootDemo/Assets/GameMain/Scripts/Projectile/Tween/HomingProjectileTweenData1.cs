using Sirenix.OdinInspector;
using UnityEngine;

namespace GameMain
{
    public class HomingProjectileTweenData1 : ProjectileGenericTweenData<HomingProjectileTween1>
    {
        [LabelText("旋转速度")]
        public float RotateSpeed;

        [LabelText("不旋转只前进的时间")]
        public float ForwardTime;
        
        [LabelText("速度曲线")]
        public AnimationCurve SpeedCurve;

        [LabelText("第几秒达到最大速度")]
        public float MaxSpeedTime;
    }
}