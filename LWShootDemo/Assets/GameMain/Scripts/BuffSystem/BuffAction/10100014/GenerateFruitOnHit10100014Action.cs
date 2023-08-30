using Sirenix.OdinInspector;
using UnityEngine;

namespace GameMain
{
    [LabelText("生成水果")]
    public class GenerateFruitOnHit10100014Action : ActionBase<OnHitArgs, GenerateFruitOnHit10100014ActData>
    {
        protected override void ExecuteInternal(OnHitArgs args)
        {
            if (Random.Range(0f, 1f) < Data.Possibality)
            {
                int recoverHp = Data.RecoverPerStack * args.Buff.Stack;
                for (int i = 0; i < Data.GenerateCount; i++)
                {
                    int id = GameEntry.Entity.GenerateSerialId();
                    var pos = args.DamageInfo.defender.transform.position;
                    pos += Random.insideUnitSphere * Data.RandomPosRange;
                    GameEntry.Entity.ShowFruit(new FruitData(id, Data.FruitProp.ID, recoverHp)
                    {
                        Position = pos,
                        Rotation = Quaternion.identity,
                        Scale = Vector3.one,
                    });
                }   
            }
        }
    }
}