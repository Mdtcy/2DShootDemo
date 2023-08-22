using UnityEngine;

namespace GameMain
{
    public class CreateOrUpgradeAoe10100006ActionData : ActionData<BuffOccurArgs, CreateOrUpgradeAoe10100006Action>
    {
        public AoeProp AoeProp;

        [Tooltip("初始范围")]
        public float InitRange;
        
        [Tooltip("每层增加的范围")]
        public float UpgradeRangePerStack;
    }
}