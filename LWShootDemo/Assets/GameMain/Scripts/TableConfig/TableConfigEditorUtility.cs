using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR

namespace GameMain
{
    public static class TableConfigEditorUtility
    {
        public static string TablePath = "Assets/GameMain/TableConfig";

        public static string NameFormat => "{0}({1})";
        
        public static T GetTableInEditor<T>() where T : SOTableList
        {
            string[] assetPaths = AssetDatabase.GetAllAssetPaths();

            // 获取Table
            foreach (string assetPath in assetPaths)
            {
                if (assetPath.StartsWith(TablePath) && !assetPath.EndsWith(".meta"))
                {
                    T table = AssetDatabase.LoadAssetAtPath<T>(assetPath);
                    if (table != null)
                    {
                        return table;
                    }
                }
            }
            
            return null;
        }

        public static void InitProp<T, Y>(T prop, Y propList) where T : IDProp where Y : SOTableList<T>
        {
            if (propList.TableList.Contains(prop))
            {
                Debug.LogError($"{propList} 已经包含该配置{prop}");
                return;
            }

            int newID = propList.NewId();
            prop.ID = newID;
            prop.DefaultName = prop.name;
            propList.TableList.Add(prop);
            EditorUtility.SetDirty(prop);
            EditorUtility.SetDirty(propList);
            AssetDatabase.SaveAssets();
            
            RenamePropWithDefaultName(prop);
        }

        public static void RenamePropWithDefaultName<T>(T prop) where T : IDProp
        {
            // 使用 AssetDatabase.RenameAsset 方法修改名字
            string path = AssetDatabase.GetAssetPath(prop);
            AssetDatabase.RenameAsset(path, string.Format(NameFormat, prop.DefaultName, prop.ID));
                        
            EditorUtility.SetDirty(prop);
            AssetDatabase.SaveAssets();
        }
        
        public static void RenameDefaultNameWithPropName<T>(T prop) where T : IDProp
        {
            prop.DefaultName = prop.name;
            
            // 将括号以及括号内的内容去掉
            string input = prop.name;
            string pattern =  @"【.*?】|\(.*?\)";
            string result = Regex.Replace(input, pattern, "");
            
            prop.DefaultName = result;
            
            EditorUtility.SetDirty(prop);
            AssetDatabase.SaveAssets();
        }

        public static void DeleteProp<T, Y>(T prop, Y propList) where T : IDProp where Y : SOTableList<T>
        {
            // 列表中移除
            propList.TableList.Remove(prop);
            EditorUtility.SetDirty(propList);

            // 删除文件
            string path = AssetDatabase.GetAssetPath(prop);
            AssetDatabase.DeleteAsset(path);
            AssetDatabase.SaveAssets();
        }

        public static void CopyProp<T>(T prop, SOTableList<T> tableList) where T : IDProp
        {
            // 获取原始对象的路径
            string originalPath = AssetDatabase.GetAssetPath(prop);

            // 获取文件的目录
            string directory = Path.GetDirectoryName(originalPath);

            // 创建新的文件名和路径
            string copyPath = Path.Combine(directory, "复制" + prop.name + ".asset");

            // 创建一个唯一的路径，避免覆盖已有的文件
            copyPath = AssetDatabase.GenerateUniqueAssetPath(copyPath);

            // 复制原始对象到新的路径
            AssetDatabase.CopyAsset(originalPath, copyPath);

            // 将新资源加载到内存
            T copyProp = AssetDatabase.LoadAssetAtPath<T>(copyPath);

            // 标记这个新的资源为 dirty，以便在下次保存项目时能够保存它的更改
            EditorUtility.SetDirty(copyProp);

            // 对新创建的副本进行一些必要的初始化操作
            int newID = tableList.NewId();
            copyProp.ID = newID;
            copyProp.DefaultName = prop.DefaultName + "(复制)";
            tableList.TableList.Add(copyProp);
            EditorUtility.SetDirty(copyProp);
            EditorUtility.SetDirty(tableList);
            AssetDatabase.SaveAssets();

            RenamePropWithDefaultName(copyProp);
        }
    }

}
#endif
