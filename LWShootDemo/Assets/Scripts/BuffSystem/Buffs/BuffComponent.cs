using System;
using System.Collections.Generic;
using LWShootDemo.BuffSystem.Event;
using LWShootDemo.BuffSystem.Events;
using UnityEngine;
using UnityEngine.Pool;

namespace LWShootDemo.BuffSystem.Buffs
{
    public class BuffComponent : MonoBehaviour
    {
        public List<Buff> Buffs = new();

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

                // Buff没秒Tick一次
                if (buff.TimeElapsed >= buff.Ticked + 1)
                {
                    buff.TriggerEvent<BuffTickEvent, BuffArgs>(new BuffArgs(buff));
                    buff.Ticked++;
                }
            }
            ListPool<Buff>.Release(buffsToRemove);
        }
    }
}