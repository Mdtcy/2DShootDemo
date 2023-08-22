using Sirenix.OdinInspector;
using UnityEngine;

namespace GameMain
{
    [LabelText("关联Buff")]
    public class BindCasterBuffOnCreateActionData : ActionData<OnAoeCreateArgs, BindCasterBuffOnCreateAction>
    {
        public BuffData BuffData;
    }
}