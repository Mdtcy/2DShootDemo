using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace LWShootDemo.Common
{
    public static class AssetDataBaseExtension 
    {
        public static List<T> FindAssetsByType<T>() where T : UnityEngine.Object
        {
            List<T> assets = new List<T>();
            string[] guids = AssetDatabase.FindAssets(string.Format("t:{0}", typeof(T)));
            for( int i = 0; i < guids.Length; i++ )
            {
                string assetPath = AssetDatabase.GUIDToAssetPath( guids[i] );
                T asset = AssetDatabase.LoadAssetAtPath<T>( assetPath );
                if( asset != null )
                {
                    assets.Add(asset);
                }
            }
            return assets;
        }
        
        public static T FindAssetByType<T>() where T : UnityEngine.Object
        {
            string[] guids = AssetDatabase.FindAssets(string.Format("t:{0}", typeof(T)));
            if (guids.Length > 0)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath( guids[0] );
                T asset = AssetDatabase.LoadAssetAtPath<T>( assetPath );
                return asset;
            }
            return null;
        }
    }
}