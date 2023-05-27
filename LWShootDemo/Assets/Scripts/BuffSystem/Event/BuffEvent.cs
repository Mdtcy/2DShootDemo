using System;
using System.Collections.Generic;
using System.Linq;
using LWShootDemo.BuffSystem.Act;
using Sirenix.OdinInspector;
using UnityEngine.Assertions;

namespace LWShootDemo.BuffSystem.Event
{
    [Serializable]
    public abstract class BuffEvent
    {
        public abstract int ID { get; }
        
        public abstract Type ExpectedArgumentType { get; }

        [LabelText("操作列表")]
        [ValueDropdown("GetValidActionDataTypes", IsUniqueList = true, DrawDropdownForListElements = false, ExcludeExistingValuesInList = true)]
        [HideReferenceObjectPicker]
        [ListDrawerSettings(Expanded = true)]
        public List<ActionData> ActionsData = new();

        private IEnumerable<ValueDropdownItem> GetValidActionDataTypes()
        {
            // 获取所有继承自ActionData的类型
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => typeof(ActionData).IsAssignableFrom(p) && !p.IsAbstract);
    
            // 为每一个类型创建一个实例，并添加到结果列表中
            var result = new List<ValueDropdownItem>();
            foreach (var type in types)
            {
                var data = Activator.CreateInstance(type) as ActionData;
                
                if (data.ExpectedArgumentType == ExpectedArgumentType || ExpectedArgumentType.IsSubclassOf(data.ExpectedArgumentType))
                {
                    var valueDropDown = new ValueDropdownItem(OdinTool.GetLabelText(type), Activator.CreateInstance(type) as ActionData);
                    result.Add(valueDropDown);   
                }
            }

            return result;
        }
    }
    
    public abstract class BuffEvent<TArgs> : BuffEvent where TArgs : class, IEventActArgs
    {
        public override Type ExpectedArgumentType => typeof(TArgs);

        public void Trigger(TArgs args)
        {
            Assert.IsNotNull(args);
            Assert.AreEqual(args.GetType(), ExpectedArgumentType, $"传入参数和事件参数不匹配 {args.GetType()} {ExpectedArgumentType}");
            // 获取一个ActionHandler

            foreach (var data in ActionsData)
            {
                if (data.State == ActionState.Disable)
                {
                    continue;
                }

                Assert.IsTrue(ExpectedArgumentType == data.ExpectedArgumentType || ExpectedArgumentType.IsSubclassOf(data.ExpectedArgumentType), $"ActionData的参数类型不对 {data.ExpectedArgumentType}");
                var action = data.CreateAction();
                action.Execute(args);
            }
        }
    }

}