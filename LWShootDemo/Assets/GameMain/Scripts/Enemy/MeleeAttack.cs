using System.Collections.Generic;
using Damages;
using LWShootDemo.BuffSystem.Buffs;
using UnityEngine;
using Entity = LWShootDemo.Entities.Entity;
using GameEntry = Fumiki.GameEntry;

namespace GameMain
{
    // todo 一次只能检测十个
    public class MeleeAttack : MonoBehaviour
    {
        public Entity caster;

        public int damage;
        
        // public Collider2D AttackCollider;
        
        public Transform AttackPosition;
        
        public float AttackRidus;

        private bool _attacking;

        public float TimeToAttack = 0.25f;
        private float time;
        
        private List<Entity> _entitiesHasAttacked = new();

        public void Attack()
        {
            // 如果正在攻击，就不再攻击
            if (_attacking)
            {
                return;
            }

            _attacking = true;
            _entitiesHasAttacked.Clear();
        }

        private void Update()
        {
            if (_attacking)
            {
                time += Time.deltaTime;
                if (time >= TimeToAttack)
                {
                    _attacking = false;
                    time = 0;
                }
                else
                {
                    TryDamage();
                }
            }
        }

        Collider2D[] results = new Collider2D[10];
        private void TryDamage()
        {
            // TODO 优化Layer
            Physics2D.OverlapCircleNonAlloc(AttackPosition.position, AttackRidus, results);

            foreach (var detectColloder in results)
            {
                if (detectColloder == null)
                {
                    continue;
                }

                var entity = detectColloder.GetComponent<Entity>();
                if (entity != null &&
                    entity.Side != caster.Side &&
                    !_entitiesHasAttacked.Contains(entity))
                {
                    var dir = (detectColloder.transform.position - transform.position).normalized;

                    // 一次攻击只能对同一个目标造成一次伤害
                    _entitiesHasAttacked.Add(entity);
                
                    GameEntry.Damage.DoDamage(caster, 
                        entity, damage, dir, 0, 
                        new List<DamageInfoTag>(),
                        new List<AddBuffInfo>());
                }  
            }
        }
        // Add this method to draw the attack range
        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            if (AttackPosition != null)
            {
                Gizmos.DrawWireSphere(AttackPosition.position, AttackRidus);
            }
        }
    }
}