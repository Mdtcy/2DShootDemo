using Animancer;
using UnityEngine;

namespace LWShootDemo.Entities.Player
{
    public class RunState : PlayerFsmStateBase
    {
        private AnimancerComponent animancerComponent;
        public RunState(bool needsExitTime, PlayerFsmContext fsmContext) : base(needsExitTime, fsmContext)
        {
            this.animancerComponent = fsmContext.AnimancerComponent;
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
            
            // 获取输入
            var movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            if (movement == Vector2.zero)
            {
                fsm.RequestStateChange(PlayerFsm.PlayerState.Idle);

                // // 朝向移动方向
                // if (movement.x > 0)
                // {
                //     Context.FaceController.Face(Direction.Right);
                // }
                // else if (movement.x < 0)
                // {
                //     Context.FaceController.Face(Direction.Left);
                // }
            }
            
            // 移动
            Context.Entity.InputMove(movement.normalized);
        }
    }
}