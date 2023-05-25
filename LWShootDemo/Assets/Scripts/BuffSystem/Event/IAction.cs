namespace LWShootDemo.BuffSystem.Event
{
    public abstract class IAction
    {
        public abstract void Execute(EventActArgsBase args);
    }
}