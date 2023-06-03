using System.Collections.Generic;
using LWShootDemo.BuffSystem.Tags;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class BuffTagContainer
{
    [ListDrawerSettings(HideAddButton = false)]
    [ValueDropdown(nameof(GetAvailableTags), IsUniqueList = true, DrawDropdownForListElements = false, ExcludeExistingValuesInList = true)]
    [SerializeField]
    private List<BuffTag> _tagList = new List<BuffTag>();

    public IReadOnlyCollection<BuffTag> Tags => _tagList;

    public bool AddTag(BuffTag tag)
    {
        if (!_tagList.Contains(tag))
        {
            _tagList.Add(tag);
            return true;
        }
        return false;
    }

    public bool RemoveTag(BuffTag tag)
    {
        return _tagList.Remove(tag);
    }

    public bool ContainsTag(BuffTag tag)
    {
        return _tagList.Contains(tag);
    }
    
    private static IEnumerable<ValueDropdownItem<BuffTag>> GetAvailableTags()
    {
        BuffSystemSetting setting = BuffSystemSetting.Inst();
        string folderPath = setting.BuffTagFolderPath;
        string[] assetGUIDs = AssetDatabase.FindAssets("t:ScriptableObject", new[] { folderPath });
        foreach (string guid in assetGUIDs)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            ScriptableObject asset = AssetDatabase.LoadAssetAtPath<ScriptableObject>(assetPath);
            List<ScriptableObject> objs = GetSubObjectOfType<BuffTag>(asset);
            foreach(BuffTag tag in objs)
            {
                yield return new ValueDropdownItem<BuffTag>(tag.name, tag);
            }
        }
    }

    private static List<ScriptableObject> GetSubObjectOfType<ClassType>(ScriptableObject asset) where ClassType : ScriptableObject
    {
        Object[] objs = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(asset));

        List<ScriptableObject> ofType = new List<ScriptableObject>();

        foreach(Object o in objs)
        {
            if(o is ClassType)
            {
                ofType.Add((ScriptableObject)o);
            }
        }

        return ofType;
    }
}
