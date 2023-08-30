using Sirenix.OdinInspector;
using UnityEngine;

namespace GameMain
{
    [LabelText("生成水果")]
    public class GenerateFruitOnHit10100014ActData : ActionData<OnHitArgs, GenerateFruitOnHit10100014Action>
    {
        public EntityProp FruitProp;
        
        [LabelText("随机范围")]
        public float RandomPosRange;
        
        public float Possibality;
        
        public int GenerateCount;
        
        public int RecoverPerStack;
    }
}