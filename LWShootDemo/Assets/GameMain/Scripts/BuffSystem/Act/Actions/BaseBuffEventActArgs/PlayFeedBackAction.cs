
using Sirenix.OdinInspector;

namespace GameMain
{
    [LabelText("播放FeedBack(PlayFeedBackAction)")]
    public class PlayFeedBackAction : ActionBase<BaseBuffEventActArgs, PlayFeedBackActionData>
    {
        protected override void ExecuteInternal(BaseBuffEventActArgs args)
        {
            args.Buff.Carrier.GetComponent<Character>().PlayFeedBack(Data.FeedBackName);
        }
    }
}