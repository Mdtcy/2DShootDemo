using LWShootDemo.Entities;
using NodeCanvas.Framework;

namespace GameMain
{
    public class Act_UnitAnimation : ActionTask
    {
        public BBParameter<Character> Character;
        public BBParameter<AnimationType> AnimationType;

        protected override void OnExecute()
        {
            base.OnExecute();
            Character.value.UnitAnimation.Play(AnimationType.value);
        }
    }
}