using System.Collections.Generic;
using Damages;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameMain
{
    [LabelText("概率造成伤害")]
    public class RandomToDamageAction : ActionBase<BuffOnProjectileHitArgs, RandomToDamageActionData>
    {
        protected override void ExecuteInternal(BuffOnProjectileHitArgs args)
        {
            float possibility = Mathf.Min(Data.MaxPossibility, Data.BasePossibility + (Data.PerStackPossibility * (args.Buff.Stack - 1)));
            if(Random.Range(0f,1f) < possibility)
            {
                var defender = args.HitObject.GetComponent<Character>();
                var attacker = args.Buff.Carrier.GetComponent<Character>();
                if (defender != null && attacker.Side != defender.Side)
                {
                    GameEntry.Damage.DoDamage(attacker, 
                        defender, (int)Data.Damage, args.Projectile.Forward, 0, 
                        new List<DamageInfoTag>(),
                        new List<AddBuffInfo>());   
                }
            }
        }
    }
}