using LWShootDemo;
using NodeCanvas.Framework;
using UnityEngine;

namespace GameMain
{
    public class Act_SetPlayerTransform : ActionTask
    {
        public BBParameter<Transform> Target;

        protected override void OnExecute()
        {
            base.OnExecute();
            Target.value = GameManager.Instance.Player;
        }
    }
}