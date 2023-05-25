using System.Collections.Generic;
using System.Linq;

namespace LWShootDemo.BuffSystem.Event
{
    public class Buff
    {
        private Dictionary<BuffEventType, BuffEvent> _events;
    
        public Buff(BuffData buffData)
        {
            _events = buffData.Events.ToDictionary(e => e.EventType);
        }
    
        public void TriggerEvent(BuffEventType eventType, EventActArgsBase args)
        {
            if (_events.TryGetValue(eventType, out var buffEvent))
            {
                buffEvent.Trigger(args);
            }
        }
    
        public void OnBuffStart()
        {
            TriggerEvent(BuffEventType.OnBuffStart, new OnBuffStartArgs());
        }
    
        public void OnProjectileStart()
        {
            TriggerEvent(BuffEventType.OnProjectileStart, new OnProjectileStartArgs());
        }
    }

}