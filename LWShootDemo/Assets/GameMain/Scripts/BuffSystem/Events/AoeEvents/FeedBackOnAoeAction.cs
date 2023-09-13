using Sirenix.OdinInspector;
using UnityEngine;

namespace GameMain
{
    [LabelText("在Aoe位置播放特效(PlayFeedBackAction)")]
    public class FeedBackOnAoeAction : ActionBase<BaseAoeEventActArgs, FeedBackOnCreateAoeActionData>
    {
        protected override void ExecuteInternal(BaseAoeEventActArgs args)
        {
            var pos = args.Aoe.transform.position + (Vector3)Data.Offset;
            var scale = Data.UseAoeRadiusAsScale ? args.Aoe.Radius * 2 : 1;
            
            var playAtPosData = PlayAtPosFeedBackData.Create(pos, scale);
            GameEntry.FeedBack.PlayAtPos(Data.PfbFeedBack, playAtPosData);
            playAtPosData.ReleaseToPool();
        }
    }
}