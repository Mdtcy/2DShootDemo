using System;
using UnityEngine;

namespace GameMain
{
    [Flags]
    public enum ProjectileTag 
    {
        Normal = 1 << 0,
    }
}