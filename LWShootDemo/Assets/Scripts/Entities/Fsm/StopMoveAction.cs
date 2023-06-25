using LWShootDemo.Entities;
using NodeCanvas.Framework;
using UnityEngine;

namespace NodeCanvas.StateMachines
{
    public class StopMoveAction : ActionTask
    {
        public BBParameter<Entity> Entity;

        protected override void OnExecute()
        {
            base.OnExecute();
            Entity.value.InputMove(Vector3.zero);
        }
    }
}