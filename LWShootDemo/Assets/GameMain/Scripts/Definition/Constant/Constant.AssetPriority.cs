using System;

namespace GameMain
{
    public static partial class Constant
    {
        /// <summary>
        /// 资源优先级
        /// </summary>
        public static class AssetPriority
        {
            public const int ConfigAsset = 100;
            public const int DataTableAsset = 100;
            public const int DictionaryAsset = 100;
            public const int FontAsset = 50;
            public const int MusicAsset = 20;
            public const int SceneAsset = 0;
            public const int SoundAsset = 30;
            public const int UIFormAsset = 50;
            public const int UISoundAsset = 30;

            public const int EnemyAsset = 80;
            public const int PlayerAsset = 80;
            public const int ProjectileAsset = 80;
            public static int AoeAsset = 80;
            public static int FruitAsset = 80;
            public static int ItemInteractAsset = 80;
            public static int ItemBoxAsset = 80;
            
            public const int TableConfigAsset = 100;
        }
    }
}