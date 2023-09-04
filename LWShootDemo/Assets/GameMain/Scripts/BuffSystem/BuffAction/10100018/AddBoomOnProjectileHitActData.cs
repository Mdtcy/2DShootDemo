using Sirenix.OdinInspector;

namespace GameMain
{
    [LabelText("粘性炸弹")]
    public class AddBoomOnProjectileHitActData : ActionData<BuffOnProjectileHitArgs, AddBoomOnProjectileHitAction>
    {
        public float BasePossibility;

        public float PossibilityPerStack;

        public BuffData BoomBuff;

        public float DelayBoomDuration;
    }
}