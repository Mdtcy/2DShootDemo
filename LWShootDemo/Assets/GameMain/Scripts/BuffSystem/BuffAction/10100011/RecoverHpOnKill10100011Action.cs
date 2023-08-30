using Sirenix.OdinInspector;

namespace GameMain
{
    [LabelText("击杀时回复生命")]
    public class RecoverHpOnKill10100011Action : ActionBase<OnKillArgs, RecoverHpOnKill10100011ActData>
    {
        protected override void ExecuteInternal(OnKillArgs args)
        {
            int recoverHp = Data.BaseRecoverHp + Data.PerStackRecoverHp * args.Buff.Stack;
            args.Buff.Carrier.GetComponent<Character>().RecoverHp(recoverHp);
        }
    }
}