using Sirenix.OdinInspector;

namespace GameMain
{
    [LabelText("添加击中力(AddProjectileForceAction)")]
    public class AddProjectileForceAction : ActionBase<OnProjectileHitArgs, AddProjectileForceActionData>
    {
        protected override void ExecuteInternal(OnProjectileHitArgs args)
        {
            var hitObj = args.HitObject;
            var hitDir = args.HitDirection;

            var motionClip = new MotionClip_Force(true, Data.Duration, hitDir, null, Data.Intensity);

            var character = hitObj.GetComponent<Character>();
            if (character)
            {
                character.PlayMotionClip(motionClip);
            }
        }
    }
}