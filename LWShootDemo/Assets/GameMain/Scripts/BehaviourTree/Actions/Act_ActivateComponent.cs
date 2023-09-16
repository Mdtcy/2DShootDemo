using NodeCanvas.Framework;
using UnityEngine;

namespace GameMain
{
    public class Act_ActivateComponent : ActionTask
    {
        public BBParameter<MonoBehaviour> Component;
        public bool Active;

        protected override void OnExecute()
        {
            base.OnExecute();
            Component.value.enabled = Active;
            EndAction();
        }
    }   
}