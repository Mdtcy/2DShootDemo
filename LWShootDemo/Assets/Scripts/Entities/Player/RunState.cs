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
            animancerComponent.TryPlay("run", 0);
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        public override void OnLogic()
        {
            base.OnLogic();
            // if(Input.GetButton("Horizontal"))
            if (Input.GetButton("Horizontal") ||
                Input.GetButton("Vertical") )
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