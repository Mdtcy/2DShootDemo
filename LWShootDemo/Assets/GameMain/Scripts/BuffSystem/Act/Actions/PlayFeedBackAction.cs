using GameMain;
using LWShootDemo.BuffSystem.Event;
using Sirenix.OdinInspector;

namespace BuffSystem.Act.Actions
{
    [LabelText("播放FeedBack(PlayFeedBackAction)")]
    public class PlayFeedBackAction : ActionBase<BaseEventActArgs, PlayFeedBackActionData>
    {
        protected override void ExecuteInternal(BaseEventActArgs args)
        {
            args.Buff.Carrier.GetComponent<Character>().PlayFeedBack(Data.FeedBackName);
        }
    }
}