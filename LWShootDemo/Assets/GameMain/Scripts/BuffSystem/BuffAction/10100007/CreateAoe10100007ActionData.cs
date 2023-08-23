using Sirenix.OdinInspector;
using UnityEngine;

namespace GameMain
{
    [LabelText("击中位置创建Aoe")]
    public class CreateAoe10100007ActionData : ActionData<BuffOnProjectileHitArgs, CreateAoe10100007Action> 
    {
        public float Radius;
        public AoeProp AoeProp;
    }
}