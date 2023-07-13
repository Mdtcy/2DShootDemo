using GameFramework.Fsm;
using LWShootDemo;
using LWShootDemo.Entities;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace GameMain
{
    public class ChaseState : FsmState<Enemy>
    {
        private Transform _player;
        private EnemyFsmContext _enemyFsmContext;

        protected override void OnInit(IFsm<Enemy> fsm)
        {
            base.OnInit(fsm);
            _enemyFsmContext = fsm.Owner.EnemyFsmContext;
            _player = GameManager.Instance.Player;
        }

        protected override void OnEnter(IFsm<Enemy> fsm)
        {
            base.OnEnter(fsm);
            Log.Debug("Enter ChaseState");
            fsm.Owner.UnitAnimation.Play(AnimationType.Walk);
        }

        protected override void OnUpdate(IFsm<Enemy> fsm, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
            Vector2 direction = _player.position + _enemyFsmContext.Offset  - fsm.Owner.transform.position;
            fsm.Owner.InputMove(direction.normalized);

            if (Vector3.Distance(_player.transform.position, fsm.Owner.transform.position) <
                _enemyFsmContext.AttackDistance)
            {
                ChangeState<EnemyAttackState>(fsm);
            }
        }
    }
}