using Sirenix.OdinInspector;
using UnityEngine;

namespace GameMain
{
    [LabelText("如果目标血量高于指定值，修改伤害")]
    public class ModifyDamageIfHighHp10100008ActionData : ActionData<OnHitArgs, ModifyDamageIfHighHp10100008Action>
    {
        [Tooltip("多少血量是否可以修改")]
        [Range(0,1)]
        public float HpCanModify;
        
        public int BasePct;
        public int PctPerStack;
    }
}