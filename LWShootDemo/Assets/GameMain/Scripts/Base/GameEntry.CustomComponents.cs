using LWShootDemo.DamageNumber;
using LWShootDemo.Damages;

namespace GameMain
{
    public sealed partial class GameEntry
    {
        public static StaticResourceComponent StaticResource { get; private set; }
        public static TextureSetComponent TextureSet { get; private set; }
        public static TimerComponent Timer { get; private set; }
        public static TimingWheelComponent TimingWheel { get; private set; }
        public static DamageComponent Damage { get; private set; }
        public static PopupComponent Popup { get; private set; }
        public static ProjectileComponent Projectile { get; private set; }
        public static TableConfigComponent TableConfig { get; private set; }
        
        public static FeedBackComponent FeedBack { get; private set; }
        
        public static AoeComponent Aoe { get; private set; }
        
        public static DifficultyComponent Difficulty { get; private set; }
        
        private void InitCustomComponents()
        {
            StaticResource = UnityGameFramework.Runtime.GameEntry.GetComponent<StaticResourceComponent>();
            TextureSet = UnityGameFramework.Runtime.GameEntry.GetComponent<TextureSetComponent>();
            Timer = UnityGameFramework.Runtime.GameEntry.GetComponent<TimerComponent>();
            TimingWheel = UnityGameFramework.Runtime.GameEntry.GetComponent<TimingWheelComponent>();
            
            // GameLogic
            Damage = UnityGameFramework.Runtime.GameEntry.GetComponent<DamageComponent>();
            Popup = UnityGameFramework.Runtime.GameEntry.GetComponent<PopupComponent>();
            Projectile = UnityGameFramework.Runtime.GameEntry.GetComponent<ProjectileComponent>();
            TableConfig = UnityGameFramework.Runtime.GameEntry.GetComponent<TableConfigComponent>();
            FeedBack = UnityGameFramework.Runtime.GameEntry.GetComponent<FeedBackComponent>();
            Aoe = UnityGameFramework.Runtime.GameEntry.GetComponent<AoeComponent>();
            Difficulty = UnityGameFramework.Runtime.GameEntry.GetComponent<DifficultyComponent>();
        }
    }
}