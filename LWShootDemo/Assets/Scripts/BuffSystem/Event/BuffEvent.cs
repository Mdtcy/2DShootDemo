using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Assertions;

namespace LWShootDemo.BuffSystem.Event
{
    [Serializable]
    public abstract class BuffEvent
    {
        public abstract int ID { get; }
        
        public abstract Type ArgType { get; }

        [ValueDropdown("GetActionData")]
        public List<ActionData> ActionsData = new List<ActionData>();

        public void Trigger(IEventActArgs args)
        {
            Assert.AreEqual(args.GetType(), ArgType, $"传入参数和事件参数不匹配 {args.GetType()} {ArgType}");
            // 获取一个ActionHandler

            foreach (var data in ActionsData)
            {
                Assert.IsTrue(ArgType == data.ArgType || ArgType.IsSubclassOf(data.ArgType), $"ActionData的参数类型不对 {data.ArgType}");
                var action = data.CreateAction(args);
                action.Execute();
            }
        }
        
        private IEnumerable<ActionData> GetActionData()
        {
            // 获取所有继承自ActionData的类型
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => typeof(ActionData).IsAssignableFrom(p) && !p.IsAbstract);
    
            // 为每一个类型创建一个实例，并添加到结果列表中
            var result = new List<ActionData>();
            foreach (var type in types)
            {
                var data = Activator.CreateInstance(type) as ActionData;
                
                if (data.ArgType == ArgType || ArgType.IsSubclassOf(data.ArgType))
                {
                    result.Add(Activator.CreateInstance(type) as ActionData);   
                }
            }

            return result;
        }
        
    }

}