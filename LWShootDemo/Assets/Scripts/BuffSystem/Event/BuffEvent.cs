using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine.Assertions;

namespace LWShootDemo.BuffSystem.Event
{
    [Serializable]
    public abstract class BuffEvent
    {
        [ReadOnly]
        public BuffEventType EventType;
        [ValueDropdown("GetActionData")]
        public List<ActionData> ActionsData = new List<ActionData>();

        public void Trigger(EventActArgsBase args)
        {
            foreach (var data in ActionsData)
            {
                // var action = data.CreateAction();
                // action.Execute(args);
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
                // result.Add(Activator.CreateInstance(type) as ActionData);   
                // var data = Activator.CreateInstance(type) as ActionData;
                // if (data.ActionArgType == ArgType || data.ActionArgType.IsSubclassOf(ArgType))
                // {
                //     // result.Add(CreateInstance(type) as BuffEvent);
                //     result.Add(Activator.CreateInstance(type) as ActionData);   
                // }
            }

            return result;
        }
        
    }

}