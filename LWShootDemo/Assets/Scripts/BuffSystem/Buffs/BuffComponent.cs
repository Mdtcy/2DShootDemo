using System.Collections.Generic;
using GameFramework;
using LWShootDemo.BuffSystem.Event;
using LWShootDemo.BuffSystem.Events;
using UnityEngine;
using UnityEngine.Pool;

namespace LWShootDemo.BuffSystem.Buffs
{
    public class BuffComponent : MonoBehaviour
    {
        public List<Buff> Buffs = new();

        public void TriggerEvent<TBuffEvent, TEventActArgs>(TEventActArgs args)
            where TBuffEvent : BuffEvent<TEventActArgs> where TEventActArgs : class, IEventActArgs
        {
            foreach (var buff in Buffs)
            {
                buff.TriggerEvent<TBuffEvent, TEventActArgs>(args);
            }
            
            ReferencePool.Release(args);
        }

        private void Update()
        {
            UpdateBuff(Time.deltaTime);
        }
        
        public void UpdateBuff(float elapseSeconds)
        {
            var buffsToRemove = ListPool<Buff>.Get();
            foreach (var buff in Buffs)
            {
                // 如果buff不是永久的，那么就减少buff的持续时间
                if (buff.Permanent == false)
                {
                    buff.Duration -= elapseSeconds;
                }

                // 更新buff的时间
                buff.TimeElapsed += elapseSeconds;

                // Buff每秒Tick一次
                if (buff.TimeElapsed >= buff.Ticked + 1)
                {
                    // 触发buffTick事件
                    var buffTickArgs = BuffTickArgs.Create();
                    buff.TriggerEvent<BuffTickEvent, BuffTickArgs>(buffTickArgs);
                    ReferencePool.Release(buffTickArgs);
                    buff.Ticked++;
                }

                if (buff.Duration <= 0 || buff.Stack <= 0)
                {
                    buffsToRemove.Add(buff);
                }
            }
            
            // 移除buff
            foreach (var buffToRemove in buffsToRemove)
            {
                Buffs.Remove(buffToRemove);
                    
                // 触发buffRemove事件
                var removeArgs = BuffRemoveArgs.Create();
                buffToRemove.TriggerEvent<BuffRemoveEvent, BuffRemoveArgs>(removeArgs);
                ReferencePool.Release(removeArgs);
                    
                // todo 重新计算属性
            }
            
            ListPool<Buff>.Release(buffsToRemove);
        }

        public void AddBuff(AddBuffInfo addBuffInfo)
        {
            List<GameObject> bCaster = new List<GameObject>();
            if (addBuffInfo.Caster)
            {
                bCaster.Add(addBuffInfo.Caster);
            }

            // todo caster是null啥情况
            List<Buff> hasOnes = GetBuffById(addBuffInfo.BuffData.ID, bCaster);
            int modStack = addBuffInfo.AddStack;
            bool toRemove = false;
            Buff toAddBuff = null;
            
            if (hasOnes.Count > 0)
            {
                hasOnes[0].Duration = (addBuffInfo.DurationSetTo == true) ? addBuffInfo.Duration : (addBuffInfo.Duration + hasOnes[0].Duration);
                hasOnes[0].Stack += modStack;
                hasOnes[0].Permanent = addBuffInfo.Permanent;
                toAddBuff = hasOnes[0];
                toRemove = hasOnes[0].Stack <= 0;
            }else
            {
                // 新建一个buff
                toAddBuff = new Buff(
                    addBuffInfo.BuffData,
                    addBuffInfo.Caster,
                    gameObject,
                    addBuffInfo.Duration,
                    addBuffInfo.AddStack,
                    addBuffInfo.Permanent);
                // 添加buff
                Buffs.Add(toAddBuff);
                // 根据优先级排序
                Buffs.Sort(SortBuffByPriority);
            }
            
            // 如果不是移除的话，那么就触发buffOccur事件
            if (toRemove == false)
            {
                // 触发buffOccur事件
                var buffOccurArgs = BuffOccurArgs.Create(modStack);
                toAddBuff.TriggerEvent<BuffOccurEvent, BuffOccurArgs>(buffOccurArgs);
                ReferencePool.Release(buffOccurArgs);
            }
         
            // todo 重新计算属性
            // AttrRecheck();
        }

        /// <summary>
        /// 获取角色身上对应的buffObj
        /// <param name="id">buff的model的id</param>
        /// <param name="caster">如果caster不是空，那么就代表只有buffObj.caster在caster里面的才符合条件</param>
        /// <return>符合条件的buffObj数组</return>
        /// </summary>
        public List<Buff> GetBuffById(string id, List<GameObject> caster = null)
        {
            List<Buff> res = new List<Buff>();
            foreach (var buff in Buffs)
            {
                if (buff.ID == id &&
                    (caster == null || caster.Count == 0 || caster.Contains(buff.Caster) == true))
                {
                    res.Add(buff);
                }
            }

            return res;
        }
        
        private int SortBuffByPriority(Buff buffA, Buff buffB)
        {
            return buffA.Data.Priority.CompareTo(buffB.Data.Priority);
        }
    }
}