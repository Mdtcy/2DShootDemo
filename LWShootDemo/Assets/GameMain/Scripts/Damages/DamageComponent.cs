using System.Collections.Generic;
using System.Linq;
using Damages;
using GameFramework;
using GameMain;
using LWShootDemo.DamageNumber;
using UnityEngine;
using UnityGameFramework.Runtime;
using GameEntry = GameMain.GameEntry;

namespace LWShootDemo.Damages
{
    public class DamageComponent : GameFrameworkComponent
    {
        private Queue<DamageInfo> _damageInfoQueue = new();
        private void Update()
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
            bool isHeal = damageInfo.IsHeal();
            
            // 如果目标已经挂了，就直接return了
            if (!defender || defender.IsDead)
            {
                return;
            }

            if (!isHeal)
            {
                // 先走一遍所有攻击者的onHit
                if (attacker)
                {
                    attacker.TriggerBuff<OnHitEvent, OnHitArgs>(OnHitArgs.Create(ref damageInfo));
                }
                // 然后走一遍挨打者的beHurt
                defender.TriggerBuff<OnBeHurtEvent, OnBeHurtArgs>(OnBeHurtArgs.Create(ref damageInfo));
            }

            if (defender.CanBeKilledByDamageInfo(damageInfo))
            {
                // 如果要增加免死金牌类的效果 需要在这里增加一个新事件
                // 杀死敌人的时候，会触发onKill
                if (attacker)
                {
                    attacker.TriggerBuff<OnKillEvent, OnKillArgs>(OnKillArgs.Create(ref damageInfo));
                }
                
                // 被杀死的时候，会触发onBeKilled
                defender.TriggerBuff<OnBeKilledEvent, OnBeKilledArgs>(OnBeKilledArgs.Create(ref damageInfo));
            }

            //最后根据结果处理：如果是治疗或者角色非无敌，才会对血量进行调整。
            int dVal = damageInfo.DamageValue(isHeal);
            if (isHeal == true || defender.ImmuneTime <= 0)
            {
                defender.TakeDamage(dVal);
                // todo 按游戏设计的规则跳数字，如果要有暴击，也可以丢在策划脚本函数（lua可以返回多参数）也可以随便怎么滴
                var popupPoint = damageInfo.defender.UnitBindManager.GetBindPointByKey("PopupPoint");
                var popupPos = popupPoint ? popupPoint.transform : damageInfo.defender.transform;
                if (isHeal)
                {
                    GameEntry.Popup.Spawn(popupPos.position, Mathf.Abs(dVal), PopupType.Heal_Normal, popupPos);
                }
                else
                {
                    if (damageInfo.HasTag(DamageInfoTag.bleed))
                    {
                        GameEntry.Popup.Spawn(popupPos.position, Mathf.Abs(dVal), PopupType.Hurt_Bleed, popupPos);
                    }
                    else
                    {
                        GameEntry.Popup.Spawn(popupPos.position, Mathf.Abs(dVal), PopupType.Hurt_Normal, popupPos);
                    }
                }
            }

            //伤害流程走完，添加buff
            foreach (var addBuffInfo in damageInfo.AddBuffs.Where(addBuffInfo => defender != null && defender.IsDead == false))
            {
                defender.AddBuff(addBuffInfo);
            }
            
            ReferencePool.Release(damageInfo);
        }

        /// <summary>
        /// 添加一个damageInfo
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="target"></param>
        /// <param name="damage"></param>
        /// <param name="damageDirection"></param>
        /// <param name="criticalRate"></param>
        /// <param name="tags"></param>
        /// <param name="addBuffInfos"></param>
        public void DoDamage(Character attacker,
            Character target, 
            int damage, 
            Vector2 damageDirection,
            float criticalRate,
            List<DamageInfoTag> tags,
            List<AddBuffInfo> addBuffInfos)
        {
            var damageInfo = ReferencePool.Acquire<DamageInfo>();
            damageInfo.Init(attacker, target, damage, damageDirection, criticalRate, tags, addBuffInfos);
            _damageInfoQueue.Enqueue(damageInfo);
        }
    }
}