using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameMain
{
    public class ProjectileProp : IDProp
    {
        [InlineEditor]
        public EntityProp EntityProp;
        
        [SerializeReference]
        public ProjectileTweenData TweenData;
        
        public ProjectileTag Tag;
        
        public float Speed;
        
        ///<summary>
        ///子弹是否会命中敌人
        ///</summary>
        [BoxGroup("子弹是否会命中敌人")]
        public bool hitFoe;

        ///<summary>
        ///子弹是否会命中盟军
        ///</summary>
        [LabelText("子弹是否会命中盟军")]
        public bool hitAlly;

        /// <summary>
        /// 子弹创建后多久是没有碰撞的，这样比如子母弹之类的，不会在创建后立即命中目标，但绝大多子弹还应该是0的
        /// 单位：秒
        /// </summary>
        [BoxGroup("子弹创建后多久是没有碰撞的")]
        public float CanHitAfterCreated;
        
        ///<summary>
        ///子弹碰触同一个目标的延迟，单位：秒，最小值是Time.fixedDeltaTime（每帧发生一次）
        ///</summary>
        public float sameTargetDelay;
        
        [BoxGroup("生命周期")]
        [LabelText("持续时间(秒)")]
        public float Duration;
        
        [BoxGroup("生命周期")]
        [LabelText("可以命中的次数")]
        public int HitTimes;
        
        [ValueDropdown(nameof(GetProjectileEventTypes), IsUniqueList = true, DrawDropdownForListElements = false, ExcludeExistingValuesInList = true)]
        [ListDrawerSettings(HideAddButton = false, HideRemoveButton = true, Expanded = true)]
        [HideReferenceObjectPicker]
        [Title("ProjectEvent")]
        [LabelText(" ")]
        public List<ProjectileEvent> Events = new();
        
        #region Odin

        private IEnumerable<ValueDropdownItem> GetProjectileEventTypes()
        {
            // 获取所有继承自ProjectEvent的类型
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => typeof(ProjectileEvent).IsAssignableFrom(p) && !p.IsAbstract);
            
            // 获取已经添加到Events列表中的ProjectEvent类型
            var existingTypes = Events.Select(e => e.GetType()).ToList();

            // 为每一个类型创建一个实例，并添加到结果列表中
            var result = new List<ValueDropdownItem>();
            foreach (var type in types)
            {
                // 如果该类型已经在Events列表中，那么就跳过
                if (existingTypes.Contains(type))
                    continue;

                var valueDropDown = new ValueDropdownItem(OdinTool.GetLabelText(type), Activator.CreateInstance(type) as ProjectileEvent);
                result.Add(valueDropDown);
            }

            return result;
        }

        #endregion
    }
}