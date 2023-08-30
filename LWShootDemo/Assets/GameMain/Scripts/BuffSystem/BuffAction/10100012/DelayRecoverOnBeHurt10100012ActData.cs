using Sirenix.OdinInspector;

namespace GameMain
{
    [LabelText("受到伤害时延迟恢复")]
    public class DelayRecoverOnBeHurt10100012ActData : ActionData<OnBeHurtArgs, DelayRecoverOnBeHurt10100012Action>
    {
        public float Delay;

        public float RecoverPerStack;
    }
}