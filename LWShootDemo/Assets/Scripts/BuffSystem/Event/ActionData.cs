using System;

namespace LWShootDemo.BuffSystem.Event
{
    public abstract class ActionData
    {
        public abstract Type ArgType { get; }
        public abstract IAction CreateAction();
    }
}