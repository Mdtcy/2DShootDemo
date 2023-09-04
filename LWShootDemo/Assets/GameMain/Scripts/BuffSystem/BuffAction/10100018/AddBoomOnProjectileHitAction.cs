using Sirenix.OdinInspector;
using UnityEngine;

namespace GameMain
{
    [LabelText("粘性炸弹")]
    public class AddBoomOnProjectileHitAction : ActionBase<BuffOnProjectileHitArgs, AddBoomOnProjectileHitActData>
    {
        protected override void ExecuteInternal(BuffOnProjectileHitArgs args)
        {
            var target = args.HitObject.GetComponent<Character>();
            if (target == null)
            {
                return;
            }

            if (args.Projectile.ContainTag(ProjectileTag.NormalAttack))
            {
                if (Random.Range(0f, 1f) < Data.BasePossibility + args.Buff.Stack * Data.PossibilityPerStack)
                {
                    target.AddBuff(new AddBuffInfo(Data.BoomBuff, 
                        args.Projectile.Caster.gameObject, 
                        args.HitObject,
                        stack: 1, 
                        duration: Data.DelayBoomDuration, 
                        durationSetTo:true,
                        permanent: false));
                }
            }
        }
    }
}