using Sirenix.OdinInspector;
using UnityEngine;

namespace GameMain
{
    public class RandomFireProjectileOnKillData : ActionData<OnKillArgs, RandomFireProjectileOnKillAction>
    {
        [LabelText("多子弹发射时的总角度")]
        public int SectorTotalAngle;
        
        public ProjectileProp ProjectileProp;

        [LabelText("初始一层的子弹数量")]
        public int BaseCount;
        
        [LabelText("每层buff增加的子弹数量")]
        public int CountPerStackAdd;
    }
}