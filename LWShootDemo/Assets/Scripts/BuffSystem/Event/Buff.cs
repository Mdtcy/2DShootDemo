using System;
using System.Collections.Generic;
using System.Linq;

namespace LWShootDemo.BuffSystem.Event
{
    public class Buff
    {
        private Dictionary<Type, BuffEvent> _events;
    
        public Buff(BuffData buffData)
        {
            _events = buffData.Events.ToDictionary(e => e.GetType());
        }

        public void TriggerEvent<TBuffEvent, TEventActArgs>(TEventActArgs args)
            where TBuffEvent : BuffEvent<TEventActArgs> where TEventActArgs : class, IEventActArgs
        {
            var key = typeof(TBuffEvent);

            if (_events.TryGetValue(key, out var buffEvent))
            {
                if (buffEvent is TBuffEvent tbuffEvent)
                {
                    tbuffEvent.Trigger(args);
                }
            }
        }

        public void OnHitEvent(HitArgs args)
        {
            TriggerEvent<HitEvent, HitArgs>(args);
        }
        
        public void OnTestEvent(TestArgs args)
        {
            TriggerEvent<TestEvent, TestArgs>(args);
        }
    }

}