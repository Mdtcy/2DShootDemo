using Animancer;
using FSM;

namespace LWShootDemo.Entities.Player
{
    public class ShootState : StateBase<PlayerFsm.PlayerState>
    {
        private AnimancerComponent animancerComponent;
        public ShootState(bool needsExitTime, AnimancerComponent animancerComponent) : base(needsExitTime)
        {
            this.animancerComponent = animancerComponent;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            animancerComponent.TryPlay("fire", 0);
        }
        
    }
}