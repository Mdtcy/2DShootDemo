using LWShootDemo.Entities;
using NodeCanvas.StateMachines;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace GameMain
{
    public class EnemyGhoul : Enemy
    {
        [SerializeField]
        private OldEntity _oldEntity;
        
        [SerializeField]
        private FSMOwner _fsmOwner;
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            _oldEntity = GetComponent<OldEntity>();
            _fsmOwner = GetComponent<FSMOwner>();
        }

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            Log.Debug("Show EnemyGhoul");
            
            _oldEntity.Init();
            _fsmOwner.StartBehaviour();
            _oldEntity.ActOnDeath += OnDeath;
        }
        
        private void OnDeath()
        {
            _fsmOwner.StopBehaviour();
            _oldEntity.ActOnDeath -= OnDeath;
            GameEntry.Entity.HideEntity(this);
        }
    }
}