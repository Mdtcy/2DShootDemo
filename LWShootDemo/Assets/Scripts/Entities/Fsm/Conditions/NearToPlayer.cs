using LWShootDemo;
using NodeCanvas.Framework;
using UnityEngine;

namespace NodeCanvas.StateMachines.Conditions
{
    public class NearToPlayer : ConditionTask
    {
        public BBParameter<float> Distance;
        public BBParameter<Transform> Self;
        
        protected override bool OnCheck()
        {
            var player = GameManager.Instance.Player;
            // Debug.Log($"CheckDistance {Vector3.Distance(player.transform.position, Self.value.position)}");
            return Vector3.Distance(player.transform.position, Self.value.position) < Distance.value;
        }
    }
}