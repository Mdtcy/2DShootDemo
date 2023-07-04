using System.Collections.Generic;
using TMPro;
using UnityGameFramework.Runtime;

namespace GameMain
{
    public class StaticResourceComponent : GameFrameworkComponent
    {
        public Dictionary<string, TMP_FontAsset> FontAssets = new();
    }
}