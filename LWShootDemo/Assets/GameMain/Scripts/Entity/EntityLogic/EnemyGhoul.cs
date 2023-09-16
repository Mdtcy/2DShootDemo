using NodeCanvas.BehaviourTrees;
using NodeCanvas.Framework;
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
    }
}