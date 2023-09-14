using Animancer;
using LWShootDemo.Weapons;
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
        
        public override void OnLogic()
        {
            base.OnLogic();
            
            // 获取输入
            var movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            
            if (movement == Vector2.zero)
            {
                fsm.RequestStateChange(PlayerFsm.PlayerState.Idle);
            }
            
            // 移动
            Context.Character.InputMove(movement.normalized * Context.Character.Speed);
        }
    }
}