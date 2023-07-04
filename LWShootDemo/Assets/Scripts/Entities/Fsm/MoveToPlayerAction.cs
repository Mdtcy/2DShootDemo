using LWShootDemo;
using LWShootDemo.Entities;
using NodeCanvas.Framework;
using UnityEngine;

namespace NodeCanvas.StateMachines
{
    public class MoveToPlayerAction : ActionTask
    {
        public BBParameter<Transform> Self;

        public BBParameter<OldEntity> Entity;
        
        public BBParameter<Vector3> Offset;
        
        protected override void OnExecute()
        {
            base.OnExecute();
            var player = GameManager.Instance.Player;
            Debug.Log(player.position + " " + Self.value.position);
            Vector2 direction = player.position + Offset.value  - Self.value.position;
            Entity.value.InputMove(direction.normalized);
        }
    }
}