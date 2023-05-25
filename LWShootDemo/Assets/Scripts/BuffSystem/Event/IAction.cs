namespace LWShootDemo.BuffSystem.Event
{
    // public abstract class IAction
    // {
    // }
    
    public abstract class IAction<TArg> where TArg :EventActArgsBase
    {
        // public abstract void Execute(TArg args);
    }
}