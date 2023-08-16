using Sirenix.OdinInspector;
using UnityEngine;

namespace GameMain
{
    public class RandomFireProjectileOnProjectileHitActData : ActionData<BuffOnProjectileHitArgs, RandomFireProjectileOnProjectileHitAction>
    {
        public int Count;

        [LabelText("多子弹发射时间隔的角度")]
        public int SectorAngle;
        
        public ProjectileProp ProjectileProp;
        
        [LabelText("带有这种子弹标签的子弹才会触发")]
        public ProjectileTag ProjectileTag;

        [LabelText("每层buff增加的概率")]
        public float ProbabilityPerStack;

        public Vector2 RandomFireRotationRange;
    }
}