

namespace GameMain
{
    public class AddProjectileForceActionData : ActionData<OnProjectileHitArgs, AddProjectileForceAction>
    {
        public float Duration;
        
        public float Intensity;
    }
}