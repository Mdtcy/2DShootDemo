using Animancer;
using FSM;
using UnityEngine;

namespace LWShootDemo.Entities.Player
{
    public class RunState : PlayerFsmStateBase
    {
        private AnimancerComponent animancerComponent;
        private Rigidbody2D rigidbody2D;

        private float speed = 1;
        
        public RunState(bool needsExitTime, PlayerFsmContext fsmContext) : base(needsExitTime, fsmContext)
        {
            this.animancerComponent = fsmContext.AnimancerComponent;
            this.rigidbody2D = fsmContext.Rb2D;
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
                // 获取输入
                float horizontal = Input.GetAxis("Horizontal");
                float vertical = Input.GetAxis("Vertical");
                
                // 移动
                Vector3 playerInput = new Vector3(horizontal, 0f, vertical);
                rigidbody2D.velocity = playerInput * speed;  
                
                // 朝向移动方向
                if (horizontal > 0)
                {
                    Context.FaceController.Face(Direction.Right);
                }
                else if (horizontal < 0)
                {
                    Context.FaceController.Face(Direction.Left);
                }
            }
            else
            {
                fsm.RequestStateChange(PlayerFsm.PlayerState.Idle);
            }
        }
        
        
    }
}