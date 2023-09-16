using GameMain.Scripts.Utility;
using NodeCanvas.Framework;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace GameMain
{
    public class Act_MoveToRandomPos : ActionTask
    {
        public BBParameter<float> HangOutRange;
        public BBParameter<Transform> SelfTrans;
        public BBParameter<AstarAI> Ai;
        
        // local
        private Vector3 TargetPos;
        public Tilemap GroundTileMap => GameObject.Find("Ground").GetComponent<Tilemap>();

        protected override string info => $"移动到随机位置";

        protected override void OnExecute()
        {
            base.OnExecute();
            var pos = TilemapUtility.FindPositionWithoutColliderNearPosition(GroundTileMap,
                SelfTrans.value.position,
                HangOutRange.value,3, 
                ~0,
                1000);

            if (pos != null)
            {
                Ai.value.CanMove = true;
                Ai.value.UseFollowTrans = false;
                Ai.value.FollowPosition = pos.Value;
            }
            else
            {
                EndAction();
            }
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();
            if (Vector3.Distance(SelfTrans.value.position, Ai.value.FollowPosition) < 1f)
            {
                EndAction();
            }
        }
    }
}