using System.Collections.Generic;
using System.Linq;
using Damages;
using GameFramework;
using LWShootDemo.BuffSystem.Events;
using LWShootDemo.Entities;
using UnityEngine;

namespace LWShootDemo.Damages
{
    public class DamageManager : GameFrameworkModule, IDamageManager
    {
        private Queue<DamageInfo> _damageInfoQueue = new();

        internal override void Update(float elapseSeconds, float realElapseSeconds)
        {
            while (_damageInfoQueue.Count > 0)
            {
                var damageInfo = _damageInfoQueue.Dequeue();
                DealWithDamage(damageInfo);
            }
        }

        private void DealWithDamage(DamageInfo damageInfo)
        {
            var attacker = damageInfo.attacker;
            var defender = damageInfo.defender;
            
            // 如果目标已经挂了，就直接return了
            if (!defender || defender.IsDead)
            {
                return;
            }
            // 先走一遍所有攻击者的onHit
            if (attacker)
            {
                var onHitArgs = new OnHitArgs(ref damageInfo);
                attacker.TriggerBuff<OnHitEvent, OnHitArgs>(onHitArgs);
            }

            // 然后走一遍挨打者的beHurt
            var onBeHurtArgs = new OnBeHurtArgs(ref damageInfo);
            defender.TriggerBuff<OnBeHurtEvent, OnBeHurtArgs>(onBeHurtArgs);

            if (defender.CanBeKilledByDamageInfo(damageInfo))
            {
                // 如果要增加免死金牌类的效果 需要在这里增加一个新事件
                // 杀死敌人的时候，会触发onKill
                if (attacker)
                {
                    var onKillArgs = new OnKillArgs(ref damageInfo);
                    attacker.TriggerBuff<OnKillEvent, OnKillArgs>(onKillArgs);
                }
                
                // 被杀死的时候，会触发onBeKilled
                var onBeKilledArgs = new OnBeKilledArgs(ref damageInfo);
                defender.TriggerBuff<OnBeKilledEvent, OnBeKilledArgs>(onBeKilledArgs);
            }

            //最后根据结果处理：如果是治疗或者角色非无敌，才会对血量进行调整。
            bool isHeal = damageInfo.IsHeal();
            int dVal = damageInfo.DamageValue(isHeal);
            if (isHeal == true || defender.ImmuneTime <= 0)
            {
                defender.TakeDamage(dVal);
                // todo 按游戏设计的规则跳数字，如果要有暴击，也可以丢在策划脚本函数（lua可以返回多参数）也可以随便怎么滴
                // SceneVariants.PopUpNumberOnCharacter(damageInfo.defender, Mathf.Abs(dVal), isHeal);
            }

            //伤害流程走完，添加buff
            foreach (var addBuffInfo in damageInfo.AddBuffs.Where(addBuffInfo => defender != null && defender.IsDead == false))
            {
                defender.AddBuff(addBuffInfo);
            }
        }

        ///<summary>
        ///添加一个damageInfo
        ///<param name="attacker">攻击者，可以为null</param>
        ///<param name="target">挨打对象</param>
        ///<param name="damage">基础伤害值</param>
        ///<param name="damageDegree">伤害的角度</param>
        ///<param name="criticalRate">暴击率，0-1</param>
        ///<param name="tags">伤害信息类型</param>
        ///</summary>
        public void DoDamage(Entity attacker, Entity target, int damage, Vector2 damageDirection, float criticalRate, DamageInfoTag[] tags)
        {
            this._damageInfoQueue.Enqueue(
                new DamageInfo(attacker, target, damage, damageDirection, criticalRate, tags));
        }

        internal override void Shutdown()
        {
            _damageInfoQueue.Clear();
        }
    }
}