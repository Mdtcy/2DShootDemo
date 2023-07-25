using LWShootDemo.BuffSystem.Event;
using LWShootDemo.BuffSystem.Events;
using UnityEngine;

namespace BuffSystem.Act.Actions
{
    public class AddDamageForceActionData : ActionData<DamageArgs, AddDamageForceAction>
    {
        public enum Target
        {
            Attacker = 0,
            Defender = 1,
        }
        
        public enum Direction
        {
            DamageDirection = 0,
            InverseDamageDirection = 1,
        }
        
        public Target TargetType;
        public Direction DirectionType;
        public float Intensity;
    }
}