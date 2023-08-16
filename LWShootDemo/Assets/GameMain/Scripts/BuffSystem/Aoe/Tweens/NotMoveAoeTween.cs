using Sirenix.OdinInspector;
using UnityEngine;

namespace GameMain
{
    [LabelText("不移动")]
    public class NotMoveAoeTween : AoeTween
    {
        public override Vector3 Tween(float timeElapsed, AoeState aoeState)
        {
            return Vector3.zero;
        }

        public override void Clear()
        {
        }
    }
}