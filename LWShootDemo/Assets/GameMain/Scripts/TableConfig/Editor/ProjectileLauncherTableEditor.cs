using UnityEditor;

namespace GameMain
{
    public class ProjectileLauncherTableEditor : TableConfigEditor<ProjectileLauncherProp, ProjectileLauncherTable>
    {
        [MenuItem("配置编辑器/ProjectileLauncher")]
        private static void OpenWindow() 
        {
            GetWindow<ProjectileLauncherTableEditor>().Show();
        }
        
        public override string LoadDataPath => "Assets/GameMain/TableConfig/ProjectileLauncherTable";
        public override string SaveDataPath => "Assets/GameMain/TableConfig/ProjectileLauncherTable";
        public override string NameFormat => "{0}({1})";
    }
}