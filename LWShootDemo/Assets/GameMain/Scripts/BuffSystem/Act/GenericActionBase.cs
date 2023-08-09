namespace GameMain
{
    public abstract class ActionBase<TArgs, TActData> : ActionBase where TArgs : IEventActArgs where TActData : ActionData
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