using Sirenix.OdinInspector;
using UnityEngine;

namespace GameMain
{
    [LabelText("普攻发射额外的子弹")]
    public class FireExtraProjectile10100017ActData : ActionData<BuffOnProjectileCreateArgs, FireExtraProjectile10100017Action>
    {
        [LabelText("需要射几次才能发射一次,每层buff-1")]
        public int BaseFireNeedCount;

        public ProjectileProp ProjectileProp;
    }
}