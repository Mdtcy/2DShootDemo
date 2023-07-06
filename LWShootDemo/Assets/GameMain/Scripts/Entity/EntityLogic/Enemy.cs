using LWShootDemo.Entities;
using NodeCanvas.StateMachines;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace GameMain
{
    public class Enemy : Character
    {

        [SerializeField]
        private FSMOwner _fsmOwner;
        
        private MeleeAttack _meleeAttack;
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            _fsmOwner = GetComponent<FSMOwner>();
            _meleeAttack = GetComponentInChildren<MeleeAttack>();
            _meleeAttack.Init(this);
            GetComponent<EnemyContext>().Character = this;
        }

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            Log.Debug("Show EnemyGhoul");
            
            ActOnDeath += OnDeath;
            _fsmOwner.StartBehaviour();
        }

        protected override void OnHide(bool isShutdown, object userData)
        {
            base.OnHide(isShutdown, userData);
            ActOnDeath -= OnDeath;
        }

        private void OnDeath()
        {
            _fsmOwner.StopBehaviour();
            GameEntry.Entity.HideEntity(this);
        }
    }
}