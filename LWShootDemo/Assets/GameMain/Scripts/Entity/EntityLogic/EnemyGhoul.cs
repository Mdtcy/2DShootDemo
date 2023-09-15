using System.Collections.Generic;
using GameFramework.Fsm;
using LWShootDemo.Entities;
using UnityGameFramework.Runtime;

namespace GameMain
{
    public class EnemyGhoul : Character
    {
        
        public EnemyFsmContext EnemyFsmContext;
        
        private MeleeAttack _meleeAttack;
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            EnemyFsmContext = GetComponent<EnemyFsmContext>();
            _meleeAttack = GetComponentInChildren<MeleeAttack>();
            _meleeAttack.Init(this);
        }
        
        private IFsm<EnemyGhoul> _fsmOwner;

        public void Attack()
        {
            _meleeAttack.Attack();
        }

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            Log.Debug("Show EnemyGhoul");

            List<FsmState<EnemyGhoul>> stateList = 
                new List<FsmState<EnemyGhoul>>()
                {
                    new EnemyChaseState(), 
                    new EnemyAttackState(),
                    new EnemyHangOutState()
                };
            
            // // todo ID需要唯一 未确认
            // _fsmOwner = GameEntry.Fsm.CreateFsm(Id.ToString(), this, stateList);
            // _fsmOwner.Start<EnemyChaseState>();
            ActOnDeath += OnDeath;

            _hpBar.Hide();
        }

        protected override void OnHide(bool isShutdown, object userData)
        {
            base.OnHide(isShutdown, userData);
            ActOnDeath -= OnDeath;
            
            
        }

        private void OnDeath()
        {
            GameEntry.Fsm.DestroyFsm(_fsmOwner);
            GameEntry.Entity.HideEntity(this);
        }
        
        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            _hpBar.Show();
        }
    }
}