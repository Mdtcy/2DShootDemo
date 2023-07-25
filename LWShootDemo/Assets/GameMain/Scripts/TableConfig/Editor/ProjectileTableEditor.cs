using UnityEditor;

namespace GameMain
{
    public class ProjectileTableEditor : TableConfigEditor<ProjectileProp, ProjectileTable>
    {
        [MenuItem("配置编辑器/Projectile")]
        private static void OpenWindow() 
        {
            GetWindow<ProjectileTableEditor>().Show();
        }
        
        public override string LoadDataPath => "Assets/GameMain/TableConfig/ProjectileTable";
        public override string SaveDataPath => "Assets/GameMain/TableConfig/ProjectileTable";
        public override string NameFormat => "{0}({1})";
    }
}