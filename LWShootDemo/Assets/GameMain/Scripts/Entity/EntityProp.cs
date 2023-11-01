using System;
using System.IO;
using System.Text;
using GameMain.Extensions;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace GameMain
{
    public class EntityProp : IDProp
    {
        [PropertyOrder(3)]
        [ValueDropdown("GetAssetDropdown")]
        public string AssetPath;
    
#if UNITY_EDITOR
        private ValueDropdownList<string> GetAssetDropdown()
        {
            string assetFolderPath = "Assets/GameMain/Entities"; // 调整为你的文件夹路径
            ValueDropdownList<string> result = new ValueDropdownList<string>();

            string[] assetsInFolder = AssetDatabase.FindAssets("", new string[] { assetFolderPath });
            for (int i = 0; i < assetsInFolder.Length; i++)
            {
                string fullPath = AssetDatabase.GUIDToAssetPath(assetsInFolder[i]);
                string relativePath = fullPath.Replace(assetFolderPath + "/", "");
                result.Add(relativePath, fullPath); // 第一个参数是显示名，第二个参数是实际值
            }

            return result;
        }
        
        [PropertyOrder(3)]
        [ShowInInspector]
        [PreviewField(ObjectFieldAlignment.Left)]
        public GameObject CachedAsset
        {
            get
            {
                return (GameObject)AssetDatabase.LoadAssetAtPath(AssetPath, typeof(GameObject));
            }
        }
        
        [Button("使用Prefab名字作为名称")]
        [PropertyOrder(99)]
        public void ChangeNameWithPrefabName()
        {
            if (AssetPath != String.Empty)
            {
                string toRemove1 = "Assets/GameMain/Entities/";
                string path = AssetPath.Replace(toRemove1, "");
                
                string toRemove2 = ".prefab";
                path = path.Replace(toRemove2, "");
                
                var parts = path.Split('/');
                StringBuilder result = new StringBuilder();
                
                for (int i = 0; i < parts.Length; i++)
                {
                    if (i >= parts.Length - 1)
                    {
                        result.Append(parts[i]);
                    }
                    else
                    {
                        // result.Append("【").Append(parts[i]).Append("】");
                    }
                }
                DefaultName = result.ToString();
                
                EditorUtility.SetDirty(this);
                AssetDatabase.SaveAssets();
                
                TableConfigEditorUtility.RenamePropWithDefaultName(this);
            }
        }

        [Button("移动至与Prefab路径对应的文件夹")]
        [PropertyOrder(99)]
        public void MoveToFolder()
        {
            string sourcePath = AssetDatabase.GetAssetPath(this);
        
            var assetDirectoryName = Path.GetDirectoryName(AssetPath);
            var targetDirectory = assetDirectoryName.Replace("Assets\\GameMain\\Entities", "Assets\\GameMain\\TableConfig\\EntityTable");
            FileExtension.MoveAssetToFolder(sourcePath, targetDirectory);
        }
    
#endif
        
        [PropertyOrder(2)]
        public string GroupName;
    }
}