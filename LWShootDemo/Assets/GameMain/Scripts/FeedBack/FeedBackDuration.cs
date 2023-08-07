using Sirenix.OdinInspector;
using UnityEngine;

namespace GameMain
{
    public class FeedBackDuration : MonoBehaviour
    {
        [LabelText("持续时间")]
        [ShowIf("HasTimeLimit")]
        public float Duration;

        [LabelText("是否有时间限制")]
        public bool HasTimeLimit = true;
    }
}