
using UnityEditor;

namespace GameMain
{
    public class BuffTableEditor : TableConfigEditor<BuffData, BuffTable>
    {
        [MenuItem("配置编辑器/Buff")]
        private static void OpenWindow() 
        {
            GetWindow<BuffTableEditor>().Show();
        }
        
        public override string LoadDataPath => "Assets/GameMain";
        public override string SaveDataPath => "Assets/GameMain/TableConfig/BuffTable";
        public override string NameFormat => "{0}({1})";
    }
}