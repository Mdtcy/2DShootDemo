using Sirenix.OdinInspector;
using UnityEngine;

namespace GameMain
{
    [LabelText("概率造成伤害")]
    public class RandomToDamageActionData : ActionData<BuffOnProjectileHitArgs, RandomToDamageAction>
    {
        [LabelText("基础概率")]
        [Range(0,1)]
        public float BasePossibility;
        
        [LabelText("每层概率")]
        [Range(0,1)]
        public float PerStackPossibility;
        
        [LabelText("最大概率")]
        [Range(0,1)]
        public float MaxPossibility;

        public int Damage;
    }
}