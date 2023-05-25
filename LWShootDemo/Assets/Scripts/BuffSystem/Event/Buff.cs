using System.Collections.Generic;
using System.Linq;

namespace LWShootDemo.BuffSystem.Event
{
    public class Buff
    {
        private Dictionary<int, BuffEvent> _events;
    
        public Buff(BuffData buffData)
        {
            _events = buffData.Events.ToDictionary(e => e.ID);
        }

        public void TriggerEvent(int id, EventActArgsBase args)
        {
            if (_events.TryGetValue(id, out var buffEvent))
            {
                buffEvent.Trigger(args);
            }
        }
        // public void TriggerEvent(BuffEventType eventType, EventActArgsBase args)
        // {
        //     if (_events.TryGetValue(eventType, out var buffEvent))
        //     {
        //         buffEvent.Trigger(args);
        //     }
        // }
    
        public void OnHitEvent(HitArgs args)
        {
            TriggerEvent(HitEvent.EventId, args);
        }
        
        public void OnTestEvent(TestArgs args)
        {
            TriggerEvent(TestEvent.EventId, args);
        }
    }

}