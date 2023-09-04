using LWShootDemo.Entities;
using UnityEngine;
using System.Linq;
namespace GameMain
{
    public class HomingProjectileTween1 : ProjectileTween
    {
        private HomingProjectileTweenData1 HomingProjectileTweenData1 => (HomingProjectileTweenData1) Data;

        private Transform _target;

        private Vector3 _veolcityLastFrame;
        public override void Clear()
        {
            _target = null;
            _veolcityLastFrame = Vector3.zero;
        }

        public override Vector3 Tween(float timeElapsed, Projectile projectile, Transform followTarget = null)
        {
            if (_target == null || _target.GetComponent<Character>().IsDead)
            {
                followTarget = GetNearestEnemy(projectile.transform)?.transform;
            }
            
            float timeMin = Mathf.Max(projectile._timeElapsed, 0.01f);
            var speedMultiplier = HomingProjectileTweenData1.SpeedCurve.Evaluate(timeMin/ HomingProjectileTweenData1.MaxSpeedTime);
            if(timeMin > HomingProjectileTweenData1.MaxSpeedTime)
            {
                speedMultiplier = HomingProjectileTweenData1.SpeedCurve.Evaluate(1);
            }
            
            if (followTarget == null || projectile._timeElapsed <= HomingProjectileTweenData1.ForwardTime)
            {
                _veolcityLastFrame = projectile.Forward * projectile.Speed;
                return projectile.Forward * projectile.Speed * speedMultiplier;
            }
            Vector3 velocity = 
                _veolcityLastFrame == Vector3.zero ?
                    projectile.Forward * projectile.Speed :
                    _veolcityLastFrame;
            
            // Calculate direction to target
            Vector3 directionToTarget = followTarget.position - projectile.transform.position;
            directionToTarget.Normalize();

            // Calculate the signed angle between the current velocity and the direction to the target
            float angleDifference = Vector3.SignedAngle(velocity, directionToTarget, Vector3.forward);

            // Limit the turn rate
            angleDifference = Mathf.Clamp(angleDifference,
                -HomingProjectileTweenData1.RotateSpeed * timeElapsed,
                HomingProjectileTweenData1.RotateSpeed * timeElapsed);

            // Rotate the velocity vector
            velocity = Quaternion.Euler(0, 0, angleDifference) * velocity;
            
            _veolcityLastFrame = velocity;
            return velocity * speedMultiplier;
        }
        
        public GameObject GetNearestEnemy(Transform transform)
        {
            // todo 获取所有没死的敌人里最近的
            var enemies = Object.FindObjectsOfType<Character>().
                Where(e => !e.IsDead && e.Side == Side.Enemy).ToArray();
            
            if (enemies.Length == 0)
            {
                return null;
            }

            var nearestEnemy = enemies[0];
            var nearestDistance = Vector2.Distance(transform.position, nearestEnemy.transform.position);
            foreach (var enemy in enemies)
            {
                var distance = Vector2.Distance(transform.position, enemy.transform.position);
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestEnemy = enemy;
                }
            }

            return nearestEnemy.gameObject;
        }
    }

}