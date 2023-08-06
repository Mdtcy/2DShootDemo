using Sirenix.OdinInspector;

namespace GameMain
{
    // 轨迹 数量 发射方向
    public class ProjectileLauncherProp : IDProp
    {
        public ProjectileInitDirection InitDirection;

        [LabelText("发射角度(逆时针)")]
        [ShowIf("InitDirection", ProjectileInitDirection.FixAngle)]
        public float Angle;

        public int Count;

        public float Delay;

        // todo 数量

        // todo 发射方向

        // todo 轨迹
    }
}