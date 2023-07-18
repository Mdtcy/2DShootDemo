using UnityEditor;

namespace GameMain
{
    public class EntityTableEditor : TableConfigEditor<EntityProp, EntityTable>
    {
        [MenuItem("配置编辑器/Entity")]
        private static void OpenWindow() 
        {
            GetWindow<EntityTableEditor>().Show();
        }
        
        public override string LoadDataPath => "Assets/GameMain/TableConfig/EntityTable";
        public override string SaveDataPath => "Assets/GameMain/TableConfig/EntityTable";
        public override string NameFormat => "{0}({1})";
    }
}