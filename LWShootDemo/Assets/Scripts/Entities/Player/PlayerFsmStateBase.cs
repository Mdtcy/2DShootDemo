using FSM;
using UnityEngine;

namespace LWShootDemo.Entities.Player
{
    public class PlayerFsmStateBase : StateBase<PlayerFsm.PlayerState>
    {
        protected PlayerFsmContext Context;
        
        public PlayerFsmStateBase(bool needsExitTime, PlayerFsmContext fsmContext) : base(needsExitTime)
        {
            Context = fsmContext;
        }
    }
}