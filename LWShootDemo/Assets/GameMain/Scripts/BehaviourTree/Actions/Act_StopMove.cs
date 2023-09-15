using NodeCanvas.Framework;
using UnityEngine;

namespace GameMain
{
    public class Act_StopMove : ActionTask
    {
        public BBParameter<AstarAI> Ai;
        public BBParameter<MovementComponent> Movement;

        protected override void OnExecute()
        {
            base.OnExecute();
            var aiValue = Ai.value;
            aiValue.CanMove = false;
            Movement.value.InputMove(Vector2.zero);
            
            EndAction();
        }
    }
}