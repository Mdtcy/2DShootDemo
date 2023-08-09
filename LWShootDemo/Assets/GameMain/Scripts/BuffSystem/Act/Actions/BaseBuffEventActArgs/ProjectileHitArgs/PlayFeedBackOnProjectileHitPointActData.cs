
using UnityEngine;

namespace GameMain
{
    public class PlayFeedBackOnProjectileHitPointActData : ActionData<OnProjectileHitArgs, PlayFeedBackOnProjectileHitPointAction>
    {
        public GameObject PfbFeedBack;
    }
}