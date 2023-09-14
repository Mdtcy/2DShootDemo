using FSM;
using UnityEngine;
using UnityEngine.UIElements;
using UnityGameFramework.Runtime;

namespace LWShootDemo.Entities.Player
{
    public class PlayerFsmStateBase : StateBase<PlayerFsm.PlayerState>
    {
        protected PlayerFsmContext Context;
        
        public PlayerFsmStateBase(bool needsExitTime, PlayerFsmContext fsmContext) : base(needsExitTime)
        {
            Context = fsmContext;
        }
        
        public override void OnEnter()
        {
            base.OnEnter();
            Log.Debug($"【Fsm】进入状态：{this}");
        }
    }
}