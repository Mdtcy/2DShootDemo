using Pathfinding;
using UnityEngine;

namespace GameMain
{
    public class EnemyFsmContext : MonoBehaviour
    {
        public Vector3 Offset;
        public float AttackDistance;
        public MeleeAttack MeleeAttack;
        public float AttackDuration;
        public float AttackBackSwing;

        public AstarAI AstarAI;
        // public AIDestinationSetter AIDestinationSetter;
    }
}