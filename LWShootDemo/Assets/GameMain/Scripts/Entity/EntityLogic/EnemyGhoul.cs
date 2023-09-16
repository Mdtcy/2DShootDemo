using GameFramework.Fsm;
using NodeCanvas.BehaviourTrees;
using UnityGameFramework.Runtime;

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

            ActOnDeath += OnDeath;

            _hpBar.Hide();
            
            _behaviourTreeOwner.StartBehaviour();
            _behaviourTreeOwner.SetExposedParameterValue("_character", this);
        }

        protected override void OnHide(bool isShutdown, object userData)
        {
            base.OnHide(isShutdown, userData);
            ActOnDeath -= OnDeath;
            _behaviourTreeOwner.StopBehaviour();
        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
            _behaviourTreeOwner.UpdateBehaviour();
        }

        private void OnDeath()
        {
            GameEntry.Entity.HideEntity(this);
        }
        
        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            _hpBar.Show();
        }
    }
}