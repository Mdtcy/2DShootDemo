using Sirenix.OdinInspector;
using UnityEngine;

namespace GameMain
{
    [LabelText("在Aoe位置播放特效(PlayFeedBackAction)")]
    public class FeedBackOnAoeAction : ActionBase<BaseAoeEventActArgs, FeedBackOnCreateAoeActionData>
    {
        protected override void ExecuteInternal(BaseAoeEventActArgs args)
        {
            var playAtPosData = PlayAtPosFeedBackData.Create();
            playAtPosData.Pos = args.Aoe.transform.position + (Vector3)Data.Offset;
            playAtPosData.Scale = Data.UseAoeRadiusAsScale ? args.Aoe.Radius * 2 : 1;
            GameEntry.FeedBack.PlayAtPos(Data.PfbFeedBack, playAtPosData);
            playAtPosData.ReleaseToPool();
        }
    }
}