using Sirenix.OdinInspector;
using UnityEngine;

namespace GameMain
{
    public class RandomFireProjectileActData : ActionData<BuffOnProjectileHitArgs, RandomFireProjectileAction>
    {
        public ProjectileProp ProjectileProp;
        
        [LabelText("带有这种子弹标签的子弹才会触发")]
        public ProjectileTag ProjectileTag;

        [LabelText("每层buff增加的概率")]
        public float ProbabilityPerStack;

        public Vector2 RandomFireRotationRange;
    }
}