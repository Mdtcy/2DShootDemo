using UnityEngine;

namespace GameMain
{
    public class FeedBackOnBuffCarrierActData : ActionData<BaseBuffEventActArgs, FeedBackOnBuffCarrierAct>
    {
        public GameObject PfbFeedBack;
        
        public Vector2 Offset;
    }
}