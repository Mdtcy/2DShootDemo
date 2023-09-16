using NodeCanvas.Framework;
using ParadoxNotion;
using UnityEngine;

namespace GameMain
{
    public class Act_WaitRandom : ActionTask
    {
        public BBParameter<Vector2> waitRange;
        public CompactStatus finishStatus = CompactStatus.Success;
        
        private float waitTime;

        protected override string info
        {
            get { return $"等待{waitRange}随机值:{waitTime}"; }
        }

        protected override void OnExecute()
        {
            base.OnExecute();
            waitTime = Random.Range(waitRange.value.x, waitRange.value.y);
        }

        protected override void OnUpdate() {
            if ( elapsedTime >= waitTime ) 
            {
                EndAction(finishStatus == CompactStatus.Success ? true : false);
            }
        }
    }
}