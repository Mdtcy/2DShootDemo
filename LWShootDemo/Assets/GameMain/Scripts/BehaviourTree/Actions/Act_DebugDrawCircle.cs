using NodeCanvas.Framework;
using UnityEngine;

namespace GameMain
{
    public class Act_DebugDrawCircle : ActionTask
    {
        public BBParameter<float> Radios;
        public BBParameter<Transform> Target;
        public Color Color;
        
        public override void OnDrawGizmosSelected()
        {
            base.OnDrawGizmosSelected();
            Gizmos.color = Color;
            Gizmos.DrawWireSphere(Target.value.position, Radios.value); 
        }

        public override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            Gizmos.color = Color;
            Gizmos.DrawWireSphere(Target.value.position, Radios.value); 
        }
    }
}