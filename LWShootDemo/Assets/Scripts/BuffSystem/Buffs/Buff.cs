using System;
using System.Collections.Generic;
using System.Linq;

namespace LWShootDemo.BuffSystem.Event
{
    public class Buff
    {
        public string Name;
        
        /// <summary>
        /// 剩余多久，单位：秒
        /// </summary>
        public float Duration;

        /// <summary>
        /// buff已经存在了多少时间了，单位：秒
        /// </summary>
        public float TimeElapsed;
        
        /// <summary>
        /// buff执行了多少次onTick了，如果不会执行onTick，那将永远是0
        /// </summary>
        public int Ticked = 0;
        
        /// <summary>
        /// 是否是一个永久的buff，永久的duration不会减少，但是timeElapsed还会增加
        /// </summary>
        public bool Permanent;
        
        
        private Dictionary<Type, BuffEvent> _events;
    
        public Buff(BuffData buffData)
        {
            Name = buffData.name;
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