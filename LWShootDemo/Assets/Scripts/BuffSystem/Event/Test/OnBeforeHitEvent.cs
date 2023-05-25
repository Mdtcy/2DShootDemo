using System;
using System.Collections.Generic;

namespace LWShootDemo.BuffSystem.Event.New
{
    [Serializable]
    public abstract class NewBuffEvent
    {
        public List<NewActionData> ActionsData = new List<NewActionData>();
        public abstract void Trigger(EventActArgsBase argsBase);
    }
    
    [Serializable]
    public abstract class NewActionData
    {
        public abstract NewAction CreateAction();
    }

    public abstract class NewAction
    {
        public abstract void Execute(EventActArgsBase args);
    }

    public class OnBeforeHitArgs : EventActArgsBase
    {
    }

    public class OnBeforeHitEvent : NewBuffEvent
    {
        public void Trigger(OnBeforeHitArgs args)
        {
            foreach (var actionData in ActionsData)
            {
                var action = actionData.CreateAction();
                action.Execute(args);
            }
        }
        

        public override void Trigger(EventActArgsBase argsBase)
        {
            var args = argsBase as OnBeforeHitArgs;
            Trigger(args);
        }
    }

    public class OnBeforeHitActionDataBase : NewActionData
    {
        public override NewAction CreateAction()
        {
            return new OnBeforeHitActionBase();
        }
    }

    public class OnBeforeHitActionBase : NewAction
    {
        public override void Execute(EventActArgsBase args)
        {
        }
    }
}