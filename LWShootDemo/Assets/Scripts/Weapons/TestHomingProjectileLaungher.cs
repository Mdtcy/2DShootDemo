using System;
using GameMain;
using UnityEngine;

namespace LWShootDemo.Weapons
{
    public class TestHomingProjectileLaungher : MonoBehaviour
    {
        [SerializeField] private Transform _firePoint;
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Fire();
            }
        }

        [SerializeField] private ProjectileProp _projectileProp;
        

        private void Fire()
        {
            var player = ((GameEntry.Procedure.CurrentProcedure as ProcedureMain).Player.Logic) as Character;
            var rotation1 = _firePoint.rotation;
            // 增加rotation30度
            var rotation2 = rotation1 * Quaternion.Euler(0, 0, 30);
            var rotation3 = rotation1 * Quaternion.Euler(0, 0, -30);
            GameEntry.Projectile.CreateProjectile(_projectileProp,
                player,
                _firePoint.position,
                rotation1);
            
            GameEntry.Projectile.CreateProjectile(_projectileProp,
                player,
                _firePoint.position,
                rotation2);
            
            GameEntry.Projectile.CreateProjectile(_projectileProp,
                player,
                _firePoint.position,
                rotation3);
        }
    }
}