using System;
using LWShootDemo.BuffSystem.Event;

public class OnProjectileStartArgs : EventActArgsBase
{
    // 在这里添加此事件的参数
}

public class OnProjectileStartEvent : BuffEvent
{
    public OnProjectileStartEvent()
    {
        EventType = BuffEventType.OnProjectileStart;
    }

    // 在这里添加此事件的逻辑
    public override Type ArgType { get; }
}