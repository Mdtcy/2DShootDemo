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
}

public abstract class OnProjectileStartActionData : ActionData
{
    public abstract IAction<OnProjectileStartArgs> CreateAction();
}

public abstract class OnProjectileStartAction : IAction<OnProjectileStartArgs>
{
    private OnProjectileStartActionData _data;
    public OnProjectileStartAction(OnProjectileStartActionData data)
    {
        _data = data;
    }

    public abstract void Execute(OnProjectileStartArgs args);
}

public class ProjectileStartTest1ActionData : OnProjectileStartActionData
{
    public override IAction<OnProjectileStartArgs> CreateAction()
    {
        return new ProjectileStartTest1Action(this);
    }
}

public class ProjectileStartTest1Action : OnProjectileStartAction
{
    public ProjectileStartTest1Action(ProjectileStartTest1ActionData data) : base(data)
    {
    }

    public override void Execute(OnProjectileStartArgs args)
    {
        
    }
}
