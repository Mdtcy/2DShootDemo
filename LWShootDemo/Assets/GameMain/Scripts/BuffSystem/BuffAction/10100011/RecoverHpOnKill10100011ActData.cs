using Sirenix.OdinInspector;

namespace GameMain
{
    [LabelText("击杀时回复生命")]
    public class RecoverHpOnKill10100011ActData : ActionData<OnKillArgs, RecoverHpOnKill10100011Action>
    {
        public int BaseRecoverHp;
        
        public int PerStackRecoverHp;
    }
}