using System.IO;
using UnityEditor;

namespace GameMain.Extensions
{
    public static class FileExtension
    {
#if UNITY_EDITOR_WIN
        
        /// <summary>
        /// 移动资源到指定文件夹
        /// </summary>
        /// <param name="assetPath"></param>
        /// <param name="targetFolderPath"></param>
        public static void MoveAssetToFolder(string assetPath, string targetFolderPath)
        {
            // 确保目标文件夹存在
            if (!Directory.Exists(targetFolderPath))
            {
                Directory.CreateDirectory(targetFolderPath);
            }

            // 获取文件名
            string fileName = Path.GetFileName(assetPath);

            // 创建目标路径
            string targetPath = Path.Combine(targetFolderPath, fileName);

            // 移动资源
            string errorMsg = AssetDatabase.MoveAsset(assetPath, targetPath);

            if (!string.IsNullOrEmpty(errorMsg))
            {
                UnityEngine.Debug.LogError($"移动资源失败：{errorMsg}");
            }
        }
#endif

    }
}