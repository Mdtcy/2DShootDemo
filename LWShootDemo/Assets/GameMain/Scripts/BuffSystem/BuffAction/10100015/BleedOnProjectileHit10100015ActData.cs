using Sirenix.OdinInspector;
using UnityEngine;

namespace GameMain
{
    [LabelText("子弹命中时流血")]
    public class BleedOnProjectileHit10100015ActData : ActionData<BuffOnProjectileHitArgs, BleedOnProjectileHit10100015Action>
    {
        [Range(0,1)]
        public float PossibilityPerStack;

        public float Duration;

        [LabelText("概率达到100%时，每层额外的buff+几秒")]
        public float DurationWhenMaxPossibilityPerStack;
        
        public BuffData BleedBuff;
    }
}