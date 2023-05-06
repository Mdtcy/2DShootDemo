using Animancer;
using UnityEngine;

namespace LWShootDemo.Entities.Player
{
    public class ShootState : PlayerFsmStateBase
    {
        private AnimancerComponent animancerComponent;
        
        // local
        private bool canShoot;
        private float lastShotTime;
        
        public ShootState(bool needsExitTime, PlayerFsmContext fsmContext) : base(needsExitTime, fsmContext)
        {
            this.animancerComponent = fsmContext.AnimancerComponent;
        }

        public override void OnEnter()
        {
            base.OnEnter();
        }
        
        public override void OnLogic()
        {
            base.OnLogic();
            canShoot = lastShotTime + Context.FireRate < Time.time;
            var enemy = Context.EnemyDetector.GetNearestEnemy();
            if (canShoot)
            {
                animancerComponent.TryPlay("fire", 0);
                if (enemy != null)
                {
                    // 获取敌人方向
                    var direction = enemy.transform.position - Context.transform.position;
                    var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
                    
                    // 武器朝向敌人
                    Context.Weapon.transform.rotation = Quaternion.Euler(0, 0, angle);
                    
                    // 朝向敌人
                    var faceDirection = direction.x > 0 ? Direction.Right : Direction.Left;
                    Context.FaceController.Face(faceDirection);
                }

                // 使用武器
                Context.Weapon.Use();
                lastShotTime = Time.time;
            }
            
            if (Input.GetButton("Horizontal") ||
                Input.GetButton("Vertical") )
            {
                fsm.RequestStateChange(PlayerFsm.PlayerState.Run);
            }
            else if (enemy == null)
            {
                fsm.RequestStateChange(PlayerFsm.PlayerState.Idle);
            }
        }
    }
}