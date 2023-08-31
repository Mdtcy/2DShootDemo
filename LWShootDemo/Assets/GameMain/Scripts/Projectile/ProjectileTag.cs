using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameMain
{
    [Flags]
    public enum ProjectileTag 
    {
        Normal = 1 << 0,
        [LabelText("普攻")]
        NormalAttack = 1 << 1,
    }
}