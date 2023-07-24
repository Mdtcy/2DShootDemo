using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;

namespace LWShootDemo.BuffSystem.Event
{
    [Serializable]
    public abstract class BaseEvent
    {
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

}