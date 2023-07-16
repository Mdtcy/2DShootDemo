using GameFramework.Fsm;
using LWShootDemo;
using LWShootDemo.Entities;
using Pathfinding;
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
            _owner = fsm.Owner;
            fsm.Owner.UnitAnimation.Play(AnimationType.Walk);
            
            _enemyFsmContext.AstarAI.targetPosition = _player.transform;
            _enemyFsmContext.AstarAI.CanMove = true;
            // _enemyFsmContext.Seeker.StartPath(_enemyFsmContext.transform.position, _player.transform.position, OnPathComplete)
            // _enemyFsmContext.Seeker.StartPath();
        }

        protected override void OnLeave(IFsm<Enemy> fsm, bool isShutdown)
        {
            base.OnLeave(fsm, isShutdown);
            _enemyFsmContext.AstarAI.CanMove = false;
            _enemyFsmContext.AstarAI.targetPosition = null;
        }

        private Enemy _owner;

        // private void OnPathComplete(Path p)
        // {
        //     if (!p.error)
        //     {
        //         _path = p;
        //         // 得到路径后可以如何使用由你决定
        //         // 比如，你可以把第一个路径点给到你自己的运动逻辑
        //         if (_path.vectorPath.Count > 0)
        //         {
        //             Vector3 nextPoint = _path.vectorPath[0];
        //             Vector2 direction = nextPoint + _enemyFsmContext.Offset  - _owner.transform.position;
        //             _owner.InputMove(direction.normalized);
        //         }
        //     }
        // }

        protected override void OnUpdate(IFsm<Enemy> fsm, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
            // Vector2 direction = _player.position + _enemyFsmContext.Offset  - fsm.Owner.transform.position;
            // fsm.Owner.InputMove(direction.normalized);

            // _enemyFsmContext.Seeker.StartPath(_enemyFsmContext.transform.position, _player.transform.position,
            //     OnPathComplete);
            if (Vector3.Distance(_player.transform.position, fsm.Owner.transform.position) <
                _enemyFsmContext.AttackDistance)
            {
                ChangeState<EnemyAttackState>(fsm);
            }
        }
    }
}