using FSM;
using UnityEngine;

namespace LWShootDemo.Entities.Player
{
    public class IdleSate : StateBase
    {
        public IdleSate(bool needsExitTime) : base(needsExitTime)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
        }

        public override void OnLogic()
        {
            base.OnLogic();
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }
}