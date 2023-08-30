using System.Collections.Generic;
using Damages;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameMain
{
    [LabelText("伤害持有者10100015")]
    public class DamageCarrier10100015ActData : ActionData<BuffTickArgs, DamageCarrier10100015Action>
    {
        public List<DamageInfoTag> DamageInfoTags;
        
        [LabelText("造成攻击力百分比伤害")]
        public float DamageAtkPercent;
    }
}