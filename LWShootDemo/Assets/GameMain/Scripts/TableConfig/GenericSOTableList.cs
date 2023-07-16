using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine.Assertions;
using UnityGameFramework.Runtime;

namespace GameMain
{
    public abstract class SOTableList<T> : SOTableList where T : IDProp
    {
        [TableList]
        public List<T> TableList;
        
        public abstract int StartId { get; }
        
        
        // todo 是否有优化必要
        public T Get(int id)
        {
            foreach (var idProp in TableList)
            {
                if (idProp.ID == id)
                {
                    return idProp;
                }
            }

            Log.Error($"【{GetType().Name}】未找到ID: {id}");
            return default;
        }

        public bool IsValidId(int id)
        {
            return IdUtility.IsValidId(id, startId: StartId);
        }
        
# if UNITY_EDITOR
        
        /// <summary>
        /// 返回一个新的ID，比当前最大的ID大1
        /// </summary>
        /// <returns></returns>
        public int NewId()
        {
            if(TableList.Count == 0)
                return StartId;
            else
            {
                int maxId = int.MinValue;
                foreach (var idProp in TableList)
                {
                    if (!IsValidId(idProp.ID))
                    {
                        Log.Error($"【{GetType().Name}】ID: {idProp.ID} 无效");
                    }
                    
                    if (idProp.ID > maxId)
                    {
                        maxId = idProp.ID;
                    }
                }
                
                Assert.IsTrue(IsValidId(maxId + 1));
                
                return maxId + 1;
            }
        }

        public void ColleteProps(string dataPath)
        {
            // 清空列表
            TableList.Clear();
            string[] assetPaths = AssetDatabase.GetAllAssetPaths();

            // 收集所有的配置
            foreach (string assetPath in assetPaths)
            {
                if (assetPath.StartsWith(dataPath) && !assetPath.EndsWith(".meta"))
                {
                    T prop = AssetDatabase.LoadAssetAtPath<T>(assetPath);
                    if (prop != null)
                    {
                        TableList.Add(prop);
                    }
                }
            }

            // 保存
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
        }
        
        public void ResetAllIds(Func<T, string> renameFunc)
        {
            var idProps = new List<T>();
            idProps.AddRange(TableList);
            TableList.Clear();
            foreach (var idProp in idProps)
            {
                idProp.ID = NewId();
                EditorUtility.SetDirty(idProp);
                AssetDatabase.SaveAssets();
                
                string path = AssetDatabase.GetAssetPath(idProp);
                AssetDatabase.RenameAsset(path, renameFunc(idProp));
                EditorUtility.SetDirty(idProp);
                AssetDatabase.SaveAssets();

                TableList.Add(idProp);
                EditorUtility.SetDirty(this);
            }

            TableList.Sort((a, b) => a.ID.CompareTo(b.ID));
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
        }

        /// <summary>
        /// 检查ID是否有效
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool CheckIdValid(ref string message)
        {
            bool isValid = true;
            // 检查ID是否有效
            foreach (var idProp in TableList)
            {
                if (!IsValidId(idProp.ID))
                {
                    message += $"【{GetType().Name}】ID: {idProp.ID} 无效";
                    isValid = false;
                }
            }
            
            return isValid;
        }
        
        /// <summary>
        /// 检查是否有重复的ID
        /// </summary>
        /// <returns></returns>
        public bool CheckDuplicateId(ref string message)
        {
            bool isDuplicate = true;
            // 检查ID是否有效
            foreach (var idProp in TableList)
            {
                foreach (var idProp2 in TableList)
                {
                    if (idProp != idProp2 && idProp.ID == idProp2.ID)
                    {
                        message += $"【{GetType().Name}】ID: {idProp.ID} 重复";
                        isDuplicate = false;
                    }
                }
            }
            
            return isDuplicate;
        }
        
        public override bool CheckValid(ref string message)
        {
            bool isValid = true;
            isValid &= CheckIdValid(ref message);
            isValid &= CheckDuplicateId(ref message);
            return isValid;
        }
        
#endif
    }
}