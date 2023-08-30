using Sirenix.OdinInspector;

namespace GameMain
{
    [LabelText("命中回血")]
    public class RecoverHpOnHit10100013ActData : ActionData<OnHitArgs, RecoverHpOnHit10100013Action>
    {
        public float BaseRecoverHp;

        public float RecoverHpPerStack;
    }
}