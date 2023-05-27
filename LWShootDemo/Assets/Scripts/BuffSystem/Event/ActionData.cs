using System;

namespace LWShootDemo.BuffSystem.Event
{
    public abstract class ActionData
    {
        public abstract Type ExpectedArgumentType { get; }

        protected abstract IAction CreateActionInternal();
        
        public IAction CreateAction()
        {
            return CreateActionInternal();
        }
    }

    public abstract class ActionData<T, TAct> : ActionData where T : IEventActArgs where TAct : IAction, new()
    {
        public override Type ExpectedArgumentType => typeof(T);
        protected override IAction CreateActionInternal()
        {
            var act = new TAct();
            act.Initialize(this);
            return act;
        }
    }
    

    public abstract class IAction
    {
        public virtual void Initialize(ActionData actionData)
        {
        }

        /// <summary>
        /// 外部调用的接口
        /// </summary>
        /// <param name="args"></param>
        public virtual void Execute(IEventActArgs args)
        {
        }
    }
    
    public abstract class Action<TArgs, TActData> : IAction where TArgs : IEventActArgs where TActData : ActionData
    {
        protected TActData Data;
        
        protected abstract void ExecuteInternal(TArgs args);
        
        public override void Execute(IEventActArgs args)
        {
            ExecuteInternal((TArgs)args);
        }

        public override void Initialize(ActionData actionData)
        {
            base.Initialize(actionData);
            Data = (TActData) actionData;
        }
    }
}