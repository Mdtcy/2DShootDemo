using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameMain
{
    public class AoeProp : IDProp
    {
        [InlineEditor]
        public EntityProp EntityProp;
        
        [LabelText("备注")]
        [PropertyOrder(-1)]
        public string Remark;
        
        public AoeTag Tag;

        /// <summary>
        /// aoe每一跳的时间，单位：秒
        /// 如果这个时间小于等于0，或者没有onTick，则不会执行aoe的onTick事件
        /// </summary>
        public float TickTime;

        /// <summary>
        /// 持续时间 单位：秒
        /// </summary>
        public float Duration;
        
        [SerializeReference]
        public AoeTweenData TweenData;
        
        [ValueDropdown(nameof(GetAoeEventTypes), IsUniqueList = true, DrawDropdownForListElements = false, ExcludeExistingValuesInList = true)]
        [ListDrawerSettings(HideAddButton = false, HideRemoveButton = true, ShowFoldout = true)]
        [HideReferenceObjectPicker]
        [Title("AoeEvent")]
        [LabelText(" ")]
        public List<AoeEvent> Events = new();
        
        #region Odin

        private IEnumerable<ValueDropdownItem> GetAoeEventTypes()
        {
            // 获取所有继承自AoeEvent的类型
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => typeof(AoeEvent).IsAssignableFrom(p) && !p.IsAbstract);
            
            // 获取已经添加到Events列表中的AoeEvent类型
            var existingTypes = Events.Select(e => e.GetType()).ToList();

            // 为每一个类型创建一个实例，并添加到结果列表中
            var result = new List<ValueDropdownItem>();
            foreach (var type in types)
            {
                // 如果该类型已经在Events列表中，那么就跳过
                if (existingTypes.Contains(type))
                    continue;

                var valueDropDown = new ValueDropdownItem(OdinToolUtility.GetLabelText(type), Activator.CreateInstance(type) as AoeEvent);
                result.Add(valueDropDown);
            }

            return result;
        }

        #endregion
    }
}