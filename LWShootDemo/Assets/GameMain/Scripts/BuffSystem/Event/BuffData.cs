using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LWShootDemo.BuffSystem.Event
{
    [CreateAssetMenu]
    [HideMonoScript]
    public class BuffData : IDProp
    {
        [LabelText("优先级")]
        public int Priority;
        
        [HideLabel]
        [BoxGroup("操作状态")]
        public ControlState ControlState;

        [HideLabel] 
        [BoxGroup("BuffTag")]
        [ShowInInspector]
        public BuffTagContainer _buffBuffTag;

        [ValueDropdown(nameof(GetBuffEventTypes), IsUniqueList = true, DrawDropdownForListElements = false, ExcludeExistingValuesInList = true)]
        [ListDrawerSettings(HideAddButton = false, HideRemoveButton = true, Expanded = true)]
        [HideReferenceObjectPicker]
        [Title("BuffEvent")]
        [LabelText(" ")]
        public List<BuffEvent> Events = new();

        #region Odin

        private IEnumerable<ValueDropdownItem> GetBuffEventTypes()
        {
            // 获取所有继承自BuffEvent的类型
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => typeof(BuffEvent).IsAssignableFrom(p) && !p.IsAbstract);
            
            // 获取已经添加到Events列表中的BuffEvent类型
            var existingTypes = Events.Select(e => e.GetType()).ToList();

            // 为每一个类型创建一个实例，并添加到结果列表中
            var result = new List<ValueDropdownItem>();
            foreach (var type in types)
            {
                // 如果该类型已经在Events列表中，那么就跳过
                if (existingTypes.Contains(type))
                    continue;

                var valueDropDown = new ValueDropdownItem(OdinTool.GetLabelText(type), Activator.CreateInstance(type) as BuffEvent);
                result.Add(valueDropDown);
            }

            return result;
        }

        #endregion
    }
}