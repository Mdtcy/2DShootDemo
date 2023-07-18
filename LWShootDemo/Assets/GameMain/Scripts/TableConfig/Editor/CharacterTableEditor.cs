using UnityEditor;

namespace GameMain
{
    public class CharacterTableEditor : TableConfigEditor<CharacterProp, CharacterTable>
    {
        [MenuItem("配置编辑器/Character")]
        private static void OpenWindow() 
        {
            GetWindow<CharacterTableEditor>().Show();
        }
        
        public override string LoadDataPath => "Assets/GameMain/TableConfig/CharacterTable";
        public override string SaveDataPath => "Assets/GameMain/TableConfig/CharacterTable";
        public override string NameFormat => "{0}({1})";
    }
}