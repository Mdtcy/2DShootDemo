using Animancer;
using FSM;
using UnityEngine;

namespace LWShootDemo.Entities.Player
{
    public class RunState : StateBase<PlayerFsm.PlayerState>
    {
        private AnimancerComponent animancerComponent;
        private Rigidbody2D rigidbody2D;

        private float speed = 1;
        
        public RunState(bool needsExitTime, AnimancerComponent animancerComponent, Rigidbody2D rigidbody2D) : base(needsExitTime)
        {
            this.animancerComponent = animancerComponent;
            this.rigidbody2D = rigidbody2D;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            animancerComponent.TryPlay("run");
        }

        public override void OnLogic()
        {
            base.OnLogic();
            if (Input.GetAxis("Horizontal") != 0 ||
                Input.GetAxis("Vertical") != 0)
            {
                Vector3 playerInput = new Vector3(
                    Input.GetAxis("Horizontal"),
                    0f,
                    Input.GetAxis("Vertical")
                );

                rigidbody2D.velocity = playerInput * speed;   
            }
            else
            {
                fsm.RequestStateChange(PlayerFsm.PlayerState.Idle);
            }
        }
    }
}