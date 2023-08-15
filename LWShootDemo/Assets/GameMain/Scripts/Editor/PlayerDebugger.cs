using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;

namespace GameMain.Editor
{
    public class PlayerDebugger : OdinEditorWindow
    {
        [MenuItem("测试/PlayerDebugger")]
        private static void OpenWindow() 
        {
            GetWindow<PlayerDebugger>().Show();
        }
        
        [BoxGroup("Buff")]
        [InlineButton("添加Buff")]
        public BuffData BuffData;

        public int Stack;

        public int Duration;
        
        public void 添加Buff()
        {
            var player = (GameEntry.Procedure.CurrentProcedure as ProcedureMain).Player.Logic as Character;
            var addBuffInfo = new AddBuffInfo(BuffData, null, player.gameObject, Stack, Duration);
            player.Buff.AddBuff(addBuffInfo);
        }
    }
}