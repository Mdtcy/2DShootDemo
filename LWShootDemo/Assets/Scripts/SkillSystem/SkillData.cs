using Sirenix.OdinInspector;

namespace GameMain
{
    public class SkillData : SerializedScriptableObject
    {
        // 前摇 可以被打断技能
        public float PreSwing;

        // 后摇 可以被打断技能
        public float BackSwing;
        
        public float Duration;

        public string TimeLine;
        
        public float Cooldown;
    }
}