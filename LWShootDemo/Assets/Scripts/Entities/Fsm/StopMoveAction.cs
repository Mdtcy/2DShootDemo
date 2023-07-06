using GameMain;
using NodeCanvas.Framework;
using UnityEngine;

namespace NodeCanvas.StateMachines
{
    public class StopMoveAction : ActionTask
    {
        public BBParameter<Character> Character;

        protected override void OnExecute()
        {
            base.OnExecute();
            Character.value.InputMove(Vector3.zero);
        }
    }
}