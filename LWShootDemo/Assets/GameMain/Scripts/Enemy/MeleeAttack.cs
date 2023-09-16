using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Damages;
using LWShootDemo.Entities;
using UnityEngine;
using GameEntry = GameMain.GameEntry;

namespace GameMain
{
    // todo 一次只能检测十个
    public class MeleeAttack : MonoBehaviour
    {
        private Character _caster;

        // public Collider2D AttackCollider;
        
        public Transform AttackPosition;
        
        public float AttackRidus;

        private bool _attacking;

        public float TimeToAttack = 0.25f;
        private float time;
        
        private List<Character> _entitiesHasAttacked = new();

        public void Init(Character caster)
        {
            _caster = caster;
        }

        public async UniTask Attack(Action onCompleted)
        {
            // 如果正在攻击，就不再攻击
            if (_attacking)
            {
                return;
            }

            _attacking = true;
            _entitiesHasAttacked.Clear();
            
            // 等待几秒后，攻击结束
            await UniTask.Delay(TimeSpan.FromSeconds(TimeToAttack)); // 这里是等待3秒，你可以根据需要更改

            _attacking = false;
            onCompleted?.Invoke();
        }

        private void Update()
        {
            if (_attacking)
            {
                TryDamage();
                // time += Time.deltaTime;
                // if (time >= TimeToAttack)
                // {
                //     _attacking = false;
                //     time = 0;
                // }
                // else
                // {
                //     TryDamage();
                // }
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

                var character = detectColloder.GetComponent<Character>();
                if (character != null &&
                    character.Side != _caster.Side &&
                    !_entitiesHasAttacked.Contains(character))
                {
                    var dir = (detectColloder.transform.position - transform.position).normalized;

                    // 一次攻击只能对同一个目标造成一次伤害
                    _entitiesHasAttacked.Add(character);
                
                    int damage = (int)(_caster.Atk);
                    GameEntry.Damage.DoDamage(_caster, 
                        character, damage, dir, 0, 
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