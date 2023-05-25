using System;

namespace LWShootDemo.BuffSystem.Event
{
    public abstract class ActionData
    {
        public abstract Type ArgType { get; }

        public abstract IAction CreateAction(EventActArgsBase args);
    }
    
    public abstract class ActionData<T> : ActionData where T : EventActArgsBase
    {
        public override Type ArgType => typeof(T);

        public override IAction CreateAction(EventActArgsBase args)
        {
            return CreateAction((T)args);
        }
        
        public abstract IAction CreateAction(T args);
    }
    
    
    public abstract class IAction
    {
        protected ActionData ActionData;
        protected EventActArgsBase Args;

        public IAction(ActionData data, EventActArgsBase args)
        {
            ActionData = data;
            Args = args;
        }

        public abstract void Execute();
        // {
        //     Assert.AreEqual(args.GetType(), _data.ArgType, "触发的参数不对");
        // }
    }
}