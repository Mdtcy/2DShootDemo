using GameMain.Item;
using UnityEditor;
using UnityEngine;

namespace GameMain
{
    public class ItemTableEditor : TableConfigEditor<ItemProp, ItemTable>
    {
        [MenuItem("配置编辑器/Item")]
        private static void OpenWindow() 
        {
            GetWindow<ItemTableEditor>().Show();
        }
        
        public override string LoadDataPath => "Assets/GameMain/TableConfig/ItemTable";
        public override string SaveDataPath => "Assets/GameMain/TableConfig/ItemTable";
        public override string NameFormat => "{0}({1})";
    }
}