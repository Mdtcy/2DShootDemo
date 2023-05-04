using Animancer;
using FSM;
using UnityEngine;

namespace LWShootDemo.Entities.Player
{
    public class IdleSate : StateBase<PlayerFsm.PlayerState>
    {
        private AnimancerComponent animancerComponent;
        public IdleSate(bool needsExitTime, AnimancerComponent animancerComponent) : base(needsExitTime)
        {
            this.animancerComponent = animancerComponent;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            animancerComponent.TryPlay("idle");
        }

        public override void OnLogic()
        {
            base.OnLogic();
            if (Input.GetAxis("Horizontal") != 0 ||
                Input.GetAxis("Vertical") != 0)
            {
                fsm.RequestStateChange(PlayerFsm.PlayerState.Run);
            }
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }
}