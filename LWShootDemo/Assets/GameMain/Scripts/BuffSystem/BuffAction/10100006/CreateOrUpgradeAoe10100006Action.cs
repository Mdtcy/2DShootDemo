using UnityEngine;
using UnityEngine.Assertions;
using UnityGameFramework.Runtime;

namespace GameMain
{
    public class CreateOrUpgradeAoe10100006Action : ActionBase<BuffOccurArgs, CreateOrUpgradeAoe10100006ActionData>
    {
        protected override void ExecuteInternal(BuffOccurArgs args)
        {
            string assetName = AssetUtility.GetEntityAsset(Data.AoeProp.EntityProp.AssetName);
            var aoes = GameEntry.Entity.GetEntityGroup("Aoe").GetEntities(assetName);
            Assert.IsTrue(aoes.Length <= 1);
            if (aoes.Length == 0)
            {
                var caster= args.Buff.Caster.GetComponent<Character>();
                var casterPos = caster.transform.position;
                GameEntry.Aoe.CreateAoe(Data.AoeProp, casterPos, Quaternion.identity, caster, Data.InitRange);
            }
            // == 1
            else
            {
                // 修改半径
                float range = Data.InitRange + Data.UpgradeRangePerStack * args.Buff.Stack;
                ((aoes[0] as Entity).Logic as AoeState).SetRadius(range);
            }
        }
    }
}