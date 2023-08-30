using System.Collections.Generic;
using GameFramework;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Pool;
using UnityGameFramework.Runtime;

namespace GameMain
{
    public class BuffComponent : MonoBehaviour
    {
        public List<Buff> Buffs = new();

        // todo 
        public void OnHide()
        {
            Buffs.Clear();
        }

        public void TriggerEvent<TBuffEvent, TEventActArgs>(TEventActArgs args)
            where TBuffEvent : BuffEvent<TEventActArgs> where TEventActArgs : BaseBuffEventActArgs
        {
            foreach (var buff in Buffs)
            {
                buff.TriggerEvent<TBuffEvent, TEventActArgs>(args);
            }
            
            ReferencePool.Release(args);
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
                    // Log.Debug($"buff:{buff.Data.DefaultName} 剩余时间:{buff.Duration}");
                }

                // 更新buff的时间
                buff.TimeElapsed += elapseSeconds;

                if (buff.Data.TickTime > 0)
                {
                    // BuffTick
                    if (buff.TimeElapsed >= (buff.Ticked + 1) * buff.Data.TickTime)
                    {
                        // 触发buffTick事件
                        var buffTickArgs = BuffTickArgs.Create();
                        buff.TriggerEvent<BuffTickEvent, BuffTickArgs>(buffTickArgs);
                        ReferencePool.Release(buffTickArgs);
                        buff.Ticked++;
                    }
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
                
                int modStack = -buffToRemove.Stack;
                // 触发buffModStack事件
                var buffModStackArgs = BuffModStackArgs.Create(modStack);
                buffToRemove.TriggerEvent<BuffModStackEvent, BuffModStackArgs>(buffModStackArgs);
                ReferencePool.Release(buffModStackArgs);
                    
                // todo 重新计算属性
            }
            
            ListPool<Buff>.Release(buffsToRemove);
        }

        public void AddBuff(AddBuffInfo addBuffInfo, bool forceNew)
        {
            List<GameObject> bCaster = new List<GameObject>();
            if (addBuffInfo.Caster)
            {
                bCaster.Add(addBuffInfo.Caster);
            }

            List<Buff> hasOnes = GetBuffById(addBuffInfo.BuffData.ID, bCaster);
            int modStack = addBuffInfo.AddStack;
            if(modStack < 0)
            {
                modStack = Mathf.Max(modStack, -hasOnes[0].Stack);
            }
            
            bool toRemove = false;
            Buff toAddBuff = null;
            
            if (hasOnes.Count > 0 && forceNew == false)
            {
                Assert.IsTrue(hasOnes.Count == 1, "目前一个buff只能有一个");
                hasOnes[0].Duration = (addBuffInfo.DurationSetTo == true) ? addBuffInfo.Duration : (addBuffInfo.Duration + hasOnes[0].Duration);
                Log.Debug($"add buff:{hasOnes[0].Data.DefaultName} 剩余时间:{hasOnes[0].Duration}");
                hasOnes[0].Stack += modStack;
                hasOnes[0].Permanent = addBuffInfo.Permanent;
                toAddBuff = hasOnes[0];
                toRemove = hasOnes[0].Stack <= 0;
                Assert.IsTrue(hasOnes[0].Stack >= 0);
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
            
            // 触发buffModStack事件
            var buffModStackArgs = BuffModStackArgs.Create(modStack);
            toAddBuff.TriggerEvent<BuffModStackEvent, BuffModStackArgs>(buffModStackArgs);
            ReferencePool.Release(buffModStackArgs);
         
            // todo 重新计算属性
            // AttrRecheck();
        }

        /// <summary>
        /// 获取角色身上对应的buffObj
        /// <param name="id">buff的model的id</param>
        /// <param name="caster">如果caster不是空，那么就代表只有buffObj.caster在caster里面的才符合条件</param>
        /// <return>符合条件的buffObj数组</return>
        /// </summary>
        public List<Buff> GetBuffById(int id, List<GameObject> caster = null)
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