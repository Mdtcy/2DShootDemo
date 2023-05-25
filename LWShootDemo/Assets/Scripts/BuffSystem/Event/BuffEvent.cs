using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace LWShootDemo.BuffSystem.Event
{
    [Serializable]
    public abstract class BuffEvent
    {
        [ReadOnly]
        public BuffEventType EventType;

        public List<ActionData> ActionsData = new List<ActionData>();
        public abstract Type ArgType { get; }

        public void Trigger(EventActArgsBase args)
        {
            foreach (var data in ActionsData)
            {
                var action = data.CreateAction();
                action.Execute(args);
            }
        }
        
    }

}