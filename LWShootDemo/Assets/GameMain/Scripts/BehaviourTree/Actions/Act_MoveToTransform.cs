using LWShootDemo.Entities;
using NodeCanvas.Framework;
using UnityEngine;

namespace GameMain
{
    public class Act_MoveToTransform : ActionTask
    {
        public BBParameter<Transform> Target;
        public BBParameter<AstarAI> Ai;
        protected override void OnExecute()
        {
            base.OnExecute();
            
            var aiValue = Ai.value;
            aiValue.CanMove = true;
            aiValue.UseFollowTrans = true;
            aiValue.FollowTransform = Target.value;
        }
    }
}