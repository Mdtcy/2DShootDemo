using LWShootDemo.Entities;
using NodeCanvas.Framework;

namespace NodeCanvas.StateMachines
{
    public class UnitAnimationAction : ActionTask
    {
        public BBParameter<UnitAnimation> UnitAnimation;
        public AnimationType AnimationType;

        protected override void OnExecute()
        {
            base.OnExecute();
            UnitAnimation.value.Play(AnimationType);
        }
    }
}