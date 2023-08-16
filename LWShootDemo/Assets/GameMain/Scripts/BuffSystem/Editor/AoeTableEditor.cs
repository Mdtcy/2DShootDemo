using UnityEditor;

namespace GameMain.Editor
{
    public class AoeTableEditor : TableConfigEditor<AoeProp, AoeTable>
    {
        [MenuItem("配置编辑器/Aoe")]
        private static void OpenWindow() 
        {
            GetWindow<AoeTableEditor>().Show();
        }
        
        public override string LoadDataPath => "Assets/GameMain/TableConfig/AoeTable";
        public override string SaveDataPath => "Assets/GameMain/TableConfig/AoeTable";
        public override string NameFormat => "{0}({1})";
    }
}