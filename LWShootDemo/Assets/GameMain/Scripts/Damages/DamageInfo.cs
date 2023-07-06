/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年3月11日
 * @modify date 2023年3月11日
 * @desc [记录伤害信息]
 */

#pragma warning disable 0649
using System.Collections.Generic;
using Damages;
using GameFramework;
using GameMain;
using LWShootDemo.BuffSystem.Buffs;
using LWShootDemo.Entities;
using UnityEngine;

namespace LWShootDemo
{
    /// <summary>
    /// 记录伤害信息
    /// </summary>
    public class DamageInfo : IReference
    {
        public Character attacker;
        
        public Character defender;
        
        ///<summary>
        ///这次伤害的类型Tag，这个会被用于buff相关的逻辑，是一个极其重要的信息
        ///这里是策划根据游戏设计来定义的，比如游戏中可能存在"frozen" "fire"之类的伤害类型，还会存在"directDamage" "period" "reflect"之类的类型伤害
        ///根据这些伤害类型，逻辑处理可能会有所不同，典型的比如"reflect"，来自反伤的，那本身一个buff的作用就是受到伤害的时候反弹伤害，如果双方都有这个buff
        ///并且这个buff没有判断damageInfo.tags里面有reflect，则可能造成“短路”，最终有一下有一方就秒了。
        ///</summary>
        public List<DamageInfoTag> Tags = new();

        /// <summary>
        /// 伤害值
        /// </summary>
        public int Damage;

        /// <summary>
        /// 伤害的方向
        /// </summary>
        public Vector2 Direction;
        
        ///<summary>
        ///是否暴击，这是游戏设计了有暴击的可能性存在。
        ///这里记录一个总暴击率，随着buff的不断改写，最后这个暴击率会得到一个0-1的值，代表0%-100%。
        ///最终处理的时候，会根据这个值来进行抉择，可以理解为，当这个值超过1的时候，buff就可以认为这次攻击暴击了。
        ///</summary>
        public float CriticalRate;
        
        ///<summary>
        ///伤害过后，给角色添加的buff
        ///</summary>
        public List<AddBuffInfo> AddBuffs = new();

        /// <summary>
        /// 根据tag判断，这是否是一次治疗，那些tag算是治疗，当然是策划定义了才算数的
        /// </summary>
        /// <returns></returns>
        public bool IsHeal()
        {
            return HasTag(DamageInfoTag.directHeal) || HasTag(DamageInfoTag.periodHeal);
        }

        public bool HasTag(DamageInfoTag damageInfoTag)
        {
            return Tags.Contains(damageInfoTag);
        }

        // todo 可配置
        public int DamageValue(bool isHeal)
        {
            bool isCrit = Random.Range(0.00f, 1.00f) <= CriticalRate;
            if (isHeal)
            {
                Damage = -Damage;
            }

            return Mathf.CeilToInt(Damage * (isCrit == true ? 2.00f:1.00f));  //暴击1.8倍（就这么设定的别问为啥，我是数值策划我说了算）
        }

        public void Init(Character attacker,
            Character defender, 
            int damage,
            Vector2 damageDirection,
            float baseCriticalRate, 
            List<DamageInfoTag> tags,
            List<AddBuffInfo> addBuffs)
        {
            this.attacker = attacker;
            this.defender = defender;
            this.Damage = damage;
            this.CriticalRate = baseCriticalRate;
            this.Direction = damageDirection;
            this.Tags.AddRange(tags);
            this.AddBuffs.AddRange(addBuffs);
        }

        public DamageInfo()
        {
        }

        public void Clear()
        {
            attacker = null;
            defender = null;
            Damage = 0;
            Direction = Vector2.zero;
            CriticalRate = 0;
            AddBuffs.Clear();
            Tags.Clear();
        }
    }
}
#pragma warning restore 0649