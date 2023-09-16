using Sirenix.OdinInspector;

namespace GameMain
{
    [LabelText("播放UnitAnimation")]
    public class UnitAnimationAction : ActionBase<BaseBuffEventActArgs, UnitAnimationActionData>
    {
        protected override void ExecuteInternal(BaseBuffEventActArgs args)
        {
            var character = args.Buff.Carrier.GetComponent<Character>();
            character.UnitAnimation.Play(Data.AnimationType);
        }
    }
}