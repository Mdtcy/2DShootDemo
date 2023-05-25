using System;

namespace LWShootDemo.BuffSystem.Event
{
    public abstract class ActionData
    {
        public abstract Type ArgType { get; }

        public abstract IAction CreateAction(IEventActArgs args);
    }
    
    public abstract class ActionData<T> : ActionData where T : IEventActArgs
    {
        public override Type ArgType => typeof(T);

        public override IAction CreateAction(IEventActArgs args)
        {
            return CreateAction((T)args);
        }
        
        public abstract IAction CreateAction(T args);
    }
    
    
    public abstract class IAction
    {
        public IAction(ActionData data, IEventActArgs args)
        {
        }

        public abstract void Execute();
        // {
        //     Assert.AreEqual(args.GetType(), _data.ArgType, "触发的参数不对");
        // }
    }
}