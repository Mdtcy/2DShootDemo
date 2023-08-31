using Sirenix.OdinInspector;
using UnityEngine;

namespace GameMain
{
    [LabelText("普攻发射额外的子弹")]
    public class FireExtraProjectile10100017Action : ActionBase<BuffOnProjectileCreateArgs, FireExtraProjectile10100017ActData>
    {
        protected override void ExecuteInternal(BuffOnProjectileCreateArgs args)
        {
            if (!args.Projectile.ContainTag(ProjectileTag.NormalAttack))
            {
                return;
            }

            // 还需要射几次才能发射
            object ob = args.Buff.Get("10100016FireCount");
            int needCount;
            if (ob == null)
            {
                needCount = Data.BaseFireNeedCount + 1 - args.Buff.Stack;
            }
            else
            {
                needCount = (int)ob;
            }

            needCount -= 1;
            if (needCount <= 0)
            {
                // 发射额外子弹 todo
                GameEntry.Projectile.CreateProjectile(Data.ProjectileProp, 
                    args.Buff.Carrier.GetComponent<Character>(),
                    args.Projectile.transform.position,
                    args.Projectile.transform.rotation);
                needCount = Data.BaseFireNeedCount + 1 - args.Buff.Stack;
            }
            
            args.Buff.Add("10100016FireCount", needCount);
        }
    }
}