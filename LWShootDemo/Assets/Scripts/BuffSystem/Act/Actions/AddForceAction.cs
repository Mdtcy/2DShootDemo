using LWShootDemo.BuffSystem.Event;
using LWShootDemo.BuffSystem.Events;
using LWShootDemo.Motion;
using Sirenix.OdinInspector;

namespace BuffSystem.Act.Actions
{
    [LabelText("添加力(AddForceAction)")]
    public class AddForceAction : ActionBase<OnHitArgs, AddForceActionData>
    {
        protected override void ExecuteInternal(OnHitArgs args)
        {
            var motionClip = new MotionClip_Force(true, 0.2f, args.DamageInfo.Direction, null, Data.Intensity);
            args.DamageInfo.attacker.PlayMotionClip(motionClip);
        }
    }
}