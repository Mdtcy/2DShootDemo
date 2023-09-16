using NodeCanvas.Framework;

namespace GameMain
{
    public class Act_MeleeAttack : ActionTask
    {
        public BBParameter<MeleeAttack> MeleeAttack;

        protected override void OnExecute()
        {
            base.OnExecute();
            MeleeAttack.value.Attack();
            EndAction();
        }
    }
}