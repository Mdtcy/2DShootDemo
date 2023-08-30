using Sirenix.OdinInspector;

namespace GameMain
{
    [LabelText("命中回血")]
    public class RecoverHpOnHit10100013Action : ActionBase<OnHitArgs, RecoverHpOnHit10100013ActData>
    {
        protected override void ExecuteInternal(OnHitArgs args)
        {
            var character = args.Buff.Carrier.GetComponent<Character>();
            float recoverHp = Data.BaseRecoverHp + args.Buff.Stack * Data.RecoverHpPerStack;
            character.RecoverHp((int)recoverHp);
        }
    }
}