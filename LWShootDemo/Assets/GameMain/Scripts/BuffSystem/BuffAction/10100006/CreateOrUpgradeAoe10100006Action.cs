using UnityEngine;
using UnityEngine.Assertions;
using UnityGameFramework.Runtime;

namespace GameMain
{
    public class CreateOrUpgradeAoe10100006Action : ActionBase<BuffOccurArgs, CreateOrUpgradeAoe10100006ActionData>
    {
        protected override void ExecuteInternal(BuffOccurArgs args)
        {
            string assetName = Data.AoeProp.EntityProp.AssetPath;
            var aoes = GameEntry.Entity.GetEntityGroup("Aoe").GetEntities(assetName);
            Assert.IsTrue(aoes.Length <= 1);
            if (aoes.Length == 0)
            {
                var carrier= args.Buff.Carrier.GetComponent<Character>();
                var carrierPos = carrier.transform.position;
                GameEntry.Aoe.CreateAoe(Data.AoeProp, carrierPos, Quaternion.identity, carrier, Data.InitRange);
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