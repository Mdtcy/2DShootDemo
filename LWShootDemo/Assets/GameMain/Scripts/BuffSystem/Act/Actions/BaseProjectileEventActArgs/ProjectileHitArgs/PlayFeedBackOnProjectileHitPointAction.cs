
using Sirenix.OdinInspector;

namespace GameMain
{
    [LabelText("在击中点播放特效(PlayFeedBackOnProjectileHitPointAction)")]
    public class PlayFeedBackOnProjectileHitPointAction : ActionBase<OnProjectileHitArgs, PlayFeedBackOnProjectileHitPointActData>
    {
        protected override void ExecuteInternal(OnProjectileHitArgs args)
        {
            var playAtPosData = PlayAtPosFeedBackData.Create();
            playAtPosData.Pos = args.HitPoint;
            GameEntry.FeedBack.PlayAtPos(Data.PfbFeedBack, playAtPosData);
            playAtPosData.ReleaseToPool();
        }
    }
}