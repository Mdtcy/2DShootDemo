using System;
using LWShootDemo.BuffSystem.Event;

public class OnBuffStartArgs : EventActArgsBase
{
    // 在这里添加此事件的参数
}

public class OnBuffStartEvent : BuffEvent
{
    public OnBuffStartEvent()
    {
        EventType = BuffEventType.OnBuffStart;
    }

    // 在这里添加此事件的逻辑
    public override Type ArgType { get; }
}