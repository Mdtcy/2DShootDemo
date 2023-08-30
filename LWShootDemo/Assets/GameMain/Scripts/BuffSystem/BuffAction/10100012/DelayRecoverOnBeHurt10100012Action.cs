using System;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;

namespace GameMain
{
    [LabelText("受到伤害时延迟恢复")]
    public class DelayRecoverOnBeHurt10100012Action : ActionBase<OnBeHurtArgs, DelayRecoverOnBeHurt10100012ActData>
    {
        protected override void ExecuteInternal(OnBeHurtArgs args)
        {
            int recoverHp = (int)(Data.RecoverPerStack * args.Buff.Stack);
            DelayRecover(args.Buff.Carrier.GetComponent<Character>(), recoverHp).Forget();
        }
        
        private async UniTaskVoid DelayRecover(Character character, float recoverHp)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(Data.Delay));

            character.RecoverHp((int)recoverHp);
        }
    }
}