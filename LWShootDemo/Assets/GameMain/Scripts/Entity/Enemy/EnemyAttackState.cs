using GameFramework.Fsm;
using LWShootDemo.Entities;
using UnityGameFramework.Runtime;

namespace GameMain
{
    public class EnemyAttackState : FsmState<EnemyGhoul>
    {
        private EnemyFsmContext _enemyFsmContext;
        
        protected override void OnInit(IFsm<EnemyGhoul> fsm)
        {
            base.OnInit(fsm);
            _enemyFsmContext = fsm.Owner.EnemyFsmContext;
        }

        protected override void OnEnter(IFsm<EnemyGhoul> fsm)
        {
            base.OnEnter(fsm);
            Log.Debug("Enter EnemyAttackState");

            idle = false;
            totalElapseTime = 0;
            fsm.Owner.InputMove(UnityEngine.Vector3.zero);
            fsm.Owner.UnitAnimation.Play(AnimationType.Attack);
            // todo 开启攻击检测 对角色造成伤害
            fsm.Owner.Attack();
            // _enemyFsmContext.MeleeAttack.Attack();
        }

        // todo 
        private bool idle = false;
        private float totalElapseTime;

        
        protected override void OnUpdate(IFsm<EnemyGhoul> fsm, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
            totalElapseTime += elapseSeconds;
            if (totalElapseTime >= _enemyFsmContext.AttackDuration )
            {
                if (totalElapseTime < _enemyFsmContext.AttackDuration + _enemyFsmContext.AttackBackSwing)
                {
                    if (!idle)
                    {
                        fsm.Owner.UnitAnimation.Play(AnimationType.Idle);

                        idle = true;   
                    }
                }
                else
                {
                    ChangeState<EnemyChaseState>(fsm);
                }
            }
        }
    }
}