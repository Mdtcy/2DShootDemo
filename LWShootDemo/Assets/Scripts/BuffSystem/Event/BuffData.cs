using System;
using System.Collections.Generic;
using System.Linq;
using LWShootDemo.BuffSystem.Tags;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace LWShootDemo.BuffSystem.Event
{
    [CreateAssetMenu]
    [HideMonoScript]
    public class BuffData : SerializedScriptableObject
    {
        public string ID;
        
        [LabelText("优先级")]
        public int Priority;

        public List<BuffTag> BuffTags = new();

        [ValueDropdown(nameof(GetBuffEventTypes), IsUniqueList = true, DrawDropdownForListElements = false, ExcludeExistingValuesInList = true)]
        [ListDrawerSettings(HideAddButton = false, HideRemoveButton = true, Expanded = true)]
        [HideReferenceObjectPicker]
        [Title("BuffEvent")]
        [LabelText(" ")]
        public List<BuffEvent> Events = new();

        public bool ContainTag(BuffTag tag)
        {
            return BuffTags.Contains(tag);
        }
        
        public bool ContainTags(List<BuffTag> tags)
        {
            foreach (var tag in tags)
            {
                if (BuffTags.Contains(tag) == false)
                    return false;
            }
            
            return true;
        }
        
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