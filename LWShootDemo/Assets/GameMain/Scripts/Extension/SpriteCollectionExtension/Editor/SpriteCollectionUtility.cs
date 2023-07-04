using UnityEditor;

namespace GameMain.Editor.SpriteCollection
{
    public static class SpriteCollectionUtility
    {
        public static void RefreshSpriteCollection()
        {
            string[] guids = AssetDatabase.FindAssets("t:SpriteCollection");
            foreach (var guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                GameMain.SpriteCollection collection = AssetDatabase.LoadAssetAtPath<GameMain.SpriteCollection>(path);
                collection.Pack();
            }

            AssetDatabase.SaveAssets();
        }
    }
}