using Sirenix.OdinInspector;
using UnityEngine;

namespace GameMain
{
    [LabelText("在BuffCarrier位置播放FeedBack(PlayFeedBackAction)")]
    public class FeedBackOnBuffCarrierAct : ActionBase<BaseBuffEventActArgs, FeedBackOnBuffCarrierActData>
    {
        protected override void ExecuteInternal(BaseBuffEventActArgs args)
        {
            var playAtPosData = PlayAtPosFeedBackData.Create();
            playAtPosData.Pos = args.Buff.Carrier.transform.position + (Vector3)Data.Offset;
            GameEntry.FeedBack.PlayAtPos(Data.PfbFeedBack, playAtPosData);
            playAtPosData.ReleaseToPool();
        }
    }
}