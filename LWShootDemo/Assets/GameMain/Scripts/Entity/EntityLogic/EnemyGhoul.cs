using System.Collections.Generic;
using GameFramework.Fsm;
using LWShootDemo.Entities;
using UnityGameFramework.Runtime;

namespace GameMain
{
    public class EnemyGhoul : Character
    {
        public UnitAnimation UnitAnimation;
        
        public EnemyFsmContext EnemyFsmContext;
        
        private MeleeAttack _meleeAttack;
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            UnitAnimation = GetComponent<UnitAnimation>();
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

            List<FsmState<EnemyGhoul>> stateList = new List<FsmState<EnemyGhoul>>() { new ChaseState(), new EnemyAttackState() };
            
            // todo ID需要唯一 未确认
            _fsmOwner = GameEntry.Fsm.CreateFsm(Id.ToString(), this, stateList);
            _fsmOwner.Start<ChaseState>();
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