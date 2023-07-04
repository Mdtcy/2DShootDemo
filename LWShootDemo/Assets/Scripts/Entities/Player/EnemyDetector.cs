using System.Linq;
using LWShootDemo.Entities.Enemy;
using Unity.VisualScripting;
using UnityEngine;

namespace LWShootDemo.Entities.Player
{
    public class EnemyDetector : MonoBehaviour
    {
        public GameObject GetNearestEnemy()
        {
            // todo 获取所有没死的敌人里最近的
            var enemies = FindObjectsOfType<OldEntity>().
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