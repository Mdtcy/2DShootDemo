using GameFramework.Fsm;
using GameMain.Scripts.Utility;
using LWShootDemo;
using LWShootDemo.Entities;
using LWShootDemo.Entities.Player;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace GameMain
{
    public class EnemyHangOutState : FsmState<EnemyGhoul>
    {
        private EnemyFsmContext _enemyFsmContext;
        private Transform _player;
        public Tilemap GroundTileMap => GameObject.Find("Ground").GetComponent<Tilemap>();

        protected override void OnEnter(IFsm<EnemyGhoul> fsm)
        {
            base.OnEnter(fsm);
            _enemyFsmContext = fsm.Owner.EnemyFsmContext;
            _player = GameManager.Instance.Player;
        }

        private float _timerToChangeBehaviour;

        private Vector3 _targetPos;
        private bool _moving;
        protected override void OnUpdate(IFsm<EnemyGhoul> fsm, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
            
            float distance = Vector3.Distance(_player.transform.position, fsm.Owner.transform.position);
            if (distance <= _enemyFsmContext.ChaseDistance)
            {
                ChangeState<EnemyChaseState>(fsm);
            }

            // 如果正在移动并且靠近了目标地
            if (_moving)
            {
                float targetDistance = Vector3.Distance(_targetPos, fsm.Owner.transform.position);
                if (targetDistance <= 1)
                {
                    _timerToChangeBehaviour = -1;
                }
            }

            _timerToChangeBehaviour -= elapseSeconds;
            if (_timerToChangeBehaviour > 0)
            {
                return;
            }

            int random = Random.Range(0, 100);
            // 原地不动
            if (random < 20)
            {
                _enemyFsmContext.AstarAI.CanMove = false;
                fsm.Owner.InputMove(Vector2.zero);
                fsm.Owner.UnitAnimation.Play(AnimationType.Idle);
                _timerToChangeBehaviour = Random.Range(1.5f, 5f);
                _moving = false;
            }
            else if (random < 30)
            { 
                _enemyFsmContext.AstarAI.CanMove = false;
                fsm.Owner.InputMove(Vector2.zero);
                fsm.Owner.UnitAnimation.Play(AnimationType.Idle);
                fsm.Owner.Face(Direction.Left);               
                _timerToChangeBehaviour = Random.Range(1.5f, 5f);
                _moving = false;
            }
            else if (random < 40)
            {
                _enemyFsmContext.AstarAI.CanMove = false;
                fsm.Owner.InputMove(Vector2.zero);
                fsm.Owner.UnitAnimation.Play(AnimationType.Idle);
                fsm.Owner.Face(Direction.Right);
                _timerToChangeBehaviour = Random.Range(1.5f, 5f);
                _moving = false;
            }
            else
            {
                // todo 找到正确的位置
                _enemyFsmContext.AstarAI.CanMove = true;
                var pos = TilemapUtility.FindPositionWithoutCollider(GroundTileMap, 3, ~0, 1000);

                if (pos != null)
                {
                    _enemyFsmContext.AstarAI.FollowPosition = pos.Value;
                    fsm.Owner.UnitAnimation.Play(AnimationType.Walk);
                    _timerToChangeBehaviour = Random.Range(3f, 8f);
                    _moving = true;
                }
            }
            
            
// #if UNITY_EDITOR
//             Gizmos.DrawWireSphere(fsm.Owner.transform.position, _enemyFsmContext.ChaseDistance);
// #endif
        }
    }
}