using LWShootDemo.Entities.Player;
using NodeCanvas.Framework;
using UnityEngine;
using UnityEngine.Assertions;

namespace GameMain
{
    public class Act_FaceOppositeRandom : ActionTask
    {
        public BBParameter<FaceController> FaceController;
        public BBParameter<float> Chance;

        protected override string info => $"{Chance}概率朝向相反方向";

        protected override void OnExecute()
        {
            base.OnExecute();
            Assert.IsTrue(Chance.value is <= 1 and >= 0, $"概率{Chance} 需要 <= 1 and >= 0");
            if (Random.Range(0, 1f) < Chance.value)
            {
                FaceController.value.FaceOpposite();
            }
            
            EndAction();
        }
    }
}