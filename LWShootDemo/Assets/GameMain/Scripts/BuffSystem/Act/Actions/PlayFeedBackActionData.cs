using LWShootDemo.BuffSystem.Event;
using Sirenix.OdinInspector;

namespace BuffSystem.Act.Actions
{
    public class PlayFeedBackActionData : ActionData<BaseEventActArgs, PlayFeedBackAction>
    {
        [LabelText("FeedBack名称")]
        public string FeedBackName;
    }
}