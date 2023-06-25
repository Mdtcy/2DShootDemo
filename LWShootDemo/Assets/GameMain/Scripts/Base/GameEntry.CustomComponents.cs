using LWShootDemo.DamageNumber;
using LWShootDemo.Damages;

namespace Fumiki
{
    public sealed partial class GameEntry
    {
        public static SpriteCollectionComponent SpriteCollection { get; private set; }
        public static StaticResourceComponent StaticResource { get; private set; }
        public static TextureSetComponent TextureSet { get; private set; }
        public static TimerComponent Timer { get; private set; }
        public static TimingWheelComponent TimingWheel { get; private set; }
        public static DamageComponent Damage { get; private set; }
        public static PopupComponent Popup { get; private set; }
        public static ProjectileComponent Projectile { get; private set; }
        
        private void InitCustomComponents()
        {
            SpriteCollection = UnityGameFramework.Runtime.GameEntry.GetComponent<SpriteCollectionComponent>();
            StaticResource = UnityGameFramework.Runtime.GameEntry.GetComponent<StaticResourceComponent>();
            TextureSet = UnityGameFramework.Runtime.GameEntry.GetComponent<TextureSetComponent>();
            Timer = UnityGameFramework.Runtime.GameEntry.GetComponent<TimerComponent>();
            TimingWheel = UnityGameFramework.Runtime.GameEntry.GetComponent<TimingWheelComponent>();
            
            // GameLogic
            Damage = UnityGameFramework.Runtime.GameEntry.GetComponent<DamageComponent>();
            Popup = UnityGameFramework.Runtime.GameEntry.GetComponent<PopupComponent>();
            Projectile = UnityGameFramework.Runtime.GameEntry.GetComponent<ProjectileComponent>();
        }
    }
}