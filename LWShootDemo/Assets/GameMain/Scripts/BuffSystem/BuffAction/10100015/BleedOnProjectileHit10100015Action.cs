using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Assertions;

namespace GameMain
{
    [LabelText("子弹命中时流血")]
    public class BleedOnProjectileHit10100015Action : ActionBase<BuffOnProjectileHitArgs, BleedOnProjectileHit10100015ActData>
    {
        protected override void ExecuteInternal(BuffOnProjectileHitArgs args)
        {
            var character = args.HitObject.GetComponent<Character>();
            if (character != null)
            {
                float possiblity = Data.PossibilityPerStack * args.Buff.Stack;
                if (Random.Range(0f, 1f) < possiblity)
                {
                    float duration = Data.Duration;
                    if (possiblity > 1)
                    {
                        int extraStack = (int)((possiblity - 1) / Data.PossibilityPerStack);
                        duration += extraStack * Data.DurationWhenMaxPossibilityPerStack;
                    }

                    character.AddBuff(new AddBuffInfo(Data.BleedBuff, 
                        args.Projectile.Caster.gameObject, 
                        args.HitObject,
                        stack: 1, 
                        duration: duration, 
                        durationSetTo:true,
                        permanent: false));
                }
            }
        }
    }
}