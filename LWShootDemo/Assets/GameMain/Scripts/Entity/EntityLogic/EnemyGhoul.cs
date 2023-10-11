using System.Numerics;
using GameMain.Scripts.Utility;
using NodeCanvas.BehaviourTrees;
using NodeCanvas.Framework;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityGameFramework.Runtime;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

namespace GameMain
{
    public class EnemyGhoul : Character
    {
        private MeleeAttack _meleeAttack;
        public BehaviourTreeOwner _behaviourTreeOwner;
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            _meleeAttack = GetComponentInChildren<MeleeAttack>();
            _behaviourTreeOwner = GetComponent<BehaviourTreeOwner>();
            _meleeAttack.Init(this);
        }

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            Log.Debug("Show EnemyGhoul");

            _hpBar.Hide();
            
            var blackboard = _behaviourTreeOwner.blackboard;
            blackboard.SetVariableValue("_character", this);
            _behaviourTreeOwner.StartBehaviour();
        }

        protected override void OnHide(bool isShutdown, object userData)
        {
            base.OnHide(isShutdown, userData);
            _behaviourTreeOwner.StopBehaviour();
        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
            _behaviourTreeOwner.UpdateBehaviour();
        }

        protected override void Death()
        {
            base.Death();
            
            // todo 不放这里
            var  groundTileMap = GameObject.Find("Ground").GetComponent<Tilemap>();
            var pos = TilemapUtility.FindPositionWithoutColliderNearPosition(groundTileMap,
                CachedTransform.position,
                2,1, 
                ~0,
                1000);
            
            // GameEntry.Entity.ShowCoinPickUp(new CoinPickUpData(GameEntry.Entity.GenerateSerialId(), 
            //     10300011, 10f)
            // {
            //     Position = CachedTransform.position,
            //     Rotation = Quaternion.identity,
            //     Scale = Vector3.one
            // });
        }
    }
}