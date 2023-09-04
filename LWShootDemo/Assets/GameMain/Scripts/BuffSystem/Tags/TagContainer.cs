using System;
using System.Collections.Generic;
using BOC.BTagged;
using LWShootDemo.Common;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace LWShootDemo.BuffSystem.Tags
{
    [Serializable]
    public class TagContainer<T> where T : BTaggedSO
    {
        [ValueDropdown(nameof(GetAvailableTags), IsUniqueList = true, DrawDropdownForListElements = false, ExcludeExistingValuesInList = true)]
        [ListDrawerSettings(HideAddButton = false, ShowFoldout = true)]
        [SerializeField]
        private List<T> _tagList = new List<T>();

        public IReadOnlyCollection<T> Tags => _tagList;

        public bool AddTag(T tag)
        {
            if (!_tagList.Contains(tag))
            {
                _tagList.Add(tag);
                return true;
            }
            return false;
        }

        public bool AddTags(TagContainer<T> other)
        {
            bool changed = false;
            foreach (var tag in other.Tags)
            {
                if (!Contains(tag))
                {
                    _tagList.Add(tag);
                    changed = true;
                }
            }

            return changed;
        }
        
        public bool RemoveTag(T tag)
        {
            return _tagList.Remove(tag);
        }

        public bool Contains(T tag)
        {
            return _tagList.Contains(tag);
        }
        
        public bool Contains(TagContainer<T> other)
        {
            foreach (var tag in other.Tags)
            {
                if (!Contains(tag))
                {
                    return false;
                }
            }

            return true;
        }

        protected virtual string TagsPath => "";
        private IEnumerable<ValueDropdownItem<T>> GetAvailableTags()
        {
            string[] assetGUIDs = AssetDatabase.FindAssets("t:ScriptableObject", new[] { TagsPath });
            foreach (string guid in assetGUIDs)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guid);
                ScriptableObject asset = AssetDatabase.LoadAssetAtPath<ScriptableObject>(assetPath);
                List<ScriptableObject> objs = AssetDataBaseUtility.GetSubObjectOfType<T>(assetPath);
                foreach(T tag in objs)
                {
                    yield return new ValueDropdownItem<T>(tag.name, tag);
                }
            }
        }
    }
}