using UnityEngine;

namespace GameMain
{
    public class AoeFollowCasterTween : AoeTween
    {
        public override Vector3 Tween(float timeElapsed, AoeState aoeState)
        {
            // todo 特殊的跟随逻辑
            aoeState.SetPosition(aoeState.Caster.transform.position);
            return Vector3.zero;
        }

        public override void Clear()
        {
        }
    }
}