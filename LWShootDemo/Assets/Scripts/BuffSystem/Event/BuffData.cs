using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LWShootDemo.BuffSystem.Event
{
    // todo 增加vaildator 不能有相同的event
    [CreateAssetMenu]
    public class BuffData : SerializedScriptableObject
    {
        [ValueDropdown("GetBuffEventTypes")]
        [ListDrawerSettings(Expanded = true)]
        [Title("BuffEvent")]
        [ShowInInspector]
        public List<BuffEvent> Events = new();

        private IEnumerable<BuffEvent> GetBuffEventTypes()
        {
            // 获取所有继承自BuffEvent的类型
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => typeof(BuffEvent).IsAssignableFrom(p) && !p.IsAbstract);

            // 获取已经添加到Events列表中的BuffEvent类型
            var existingTypes = Events.Select(e => e.GetType()).ToList();

            // 为每一个类型创建一个实例，并添加到结果列表中
            var result = new List<BuffEvent>();
            foreach (var type in types)
            {
                // 如果该类型已经在Events列表中，那么就跳过
                if (existingTypes.Contains(type))
                    continue;

                result.Add(Activator.CreateInstance(type) as BuffEvent);
            }

            return result;
        }

    }

}