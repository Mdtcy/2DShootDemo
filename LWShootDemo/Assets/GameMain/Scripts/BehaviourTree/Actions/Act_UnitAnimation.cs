using LWShootDemo.Entities;
using NodeCanvas.Framework;

namespace GameMain
{
    public class Act_UnitAnimation : ActionTask
    {
        public BBParameter<UnitAnimation> Unimation;
        public BBParameter<AnimationType> AnimationType;

        protected override void OnExecute()
        {
            base.OnExecute();
            Unimation.value.Play(AnimationType.value);
            EndAction();
        }
    }
}