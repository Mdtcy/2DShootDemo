
using Sirenix.OdinInspector;

namespace GameMain
{
    public class PlayFeedBackActionData : ActionData<BaseBuffEventActArgs, PlayFeedBackAction>
    {
        [LabelText("FeedBack名称")]
        public string FeedBackName;
    }
}