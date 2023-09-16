using LWShootDemo.Entities;
using UnityEngine;

namespace GameMain
{
    public class UnitAnimationActionData : ActionData<BaseBuffEventActArgs, UnitAnimationAction>
    {
        public AnimationType AnimationType;
    }
}