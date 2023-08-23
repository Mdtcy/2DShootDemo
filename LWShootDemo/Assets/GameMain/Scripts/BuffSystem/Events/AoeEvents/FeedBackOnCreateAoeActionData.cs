using UnityEngine;

namespace GameMain
{
    public class FeedBackOnCreateAoeActionData : ActionData<BaseAoeEventActArgs, FeedBackOnAoeAction>
    {
        public GameObject PfbFeedBack;
        
        public Vector2 Offset;

        public bool UseAoeRadiusAsScale;
    }
}