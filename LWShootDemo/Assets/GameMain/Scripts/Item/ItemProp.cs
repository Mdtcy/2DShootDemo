using Sirenix.OdinInspector;
using UnityEngine;

namespace GameMain.Item
{
    public class ItemProp : IDProp
    {
        [PreviewField(ObjectFieldAlignment.Left)]
        public Sprite Model;

        // todo 暂时 后面换成本地化的
        [LabelText("名称")]
        public string Name => DefaultName;

        [LabelText("描述")]
        public string Description;
        
        [LabelText("稀有度")]
        public ItemRarity Rarity;

        [LabelText("最大可获得数量（-1为无限制）")]
        public int MaxCount = -1;

        public BuffData Buff;
    }
}