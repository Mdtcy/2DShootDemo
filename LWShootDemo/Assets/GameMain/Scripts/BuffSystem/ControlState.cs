using System;

namespace GameMain
{
    [Flags]
    public enum ControlState
    {
        DisableMove = 1 << 0,
        DisableRotate = 1 << 1,
        DisableUseSkill = 1 << 2,
    }
}