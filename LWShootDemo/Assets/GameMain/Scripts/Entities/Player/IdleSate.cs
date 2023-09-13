using Animancer;
using FSM;
using UnityEngine;

namespace LWShootDemo.Entities.Player
{
    public class IdleSate : PlayerFsmStateBase
    {
        private AnimancerComponent animancerComponent;
        private float elapsedTime;
        public IdleSate(bool needsExitTime, PlayerFsmContext fsmContext) : base(needsExitTime, fsmContext)
        {
            this.animancerComponent = fsmContext.AnimancerComponent;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            elapsedTime = 0;
            animancerComponent.TryPlay("idle", 0, FadeMode.FromStart);
        }

        public override void OnLogic()
        {
            base.OnLogic();
            elapsedTime += Time.fixedDeltaTime;
            if (Input.GetButton("Horizontal") ||
                Input.GetButton("Vertical") )
            {
                fsm.RequestStateChange(PlayerFsm.PlayerState.Run);
            }
            // else if (elapsedTime >= Context.FireRate && 
            //          Context.EnemyDetector.GetNearestEnemy() != null)
            // {
            //     fsm.RequestStateChange(PlayerFsm.PlayerState.Shoot);
            // }
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }
}