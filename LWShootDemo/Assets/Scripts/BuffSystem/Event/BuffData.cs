using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LWShootDemo.BuffSystem.Event
{
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
    
            // 为每一个类型创建一个实例，并添加到结果列表中
            var result = new List<BuffEvent>();
            foreach (var type in types)
            {
                // result.Add(CreateInstance(type) as BuffEvent);
                result.Add(Activator.CreateInstance(type) as BuffEvent);
            }

            return result;
        }
    }

}