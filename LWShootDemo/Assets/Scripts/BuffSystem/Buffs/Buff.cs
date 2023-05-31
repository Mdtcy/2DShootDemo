using System;
using System.Collections.Generic;
using System.Linq;
using GameFramework;
using LWShootDemo.BuffSystem.Events;
using UnityEngine;

namespace LWShootDemo.BuffSystem.Event
{
    public class Buff
    {
        public string ID => Data.ID;
        
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

        /// <summary>
        /// 层数
        /// </summary>
        public int Stack;
        
        /// <summary>
        /// buff的施法者是谁，当然可以是空的
        /// </summary>
        public GameObject Caster;

        /// <summary>
        /// buff的携带者，实际上是作为参数传递给脚本用，具体是谁，可定是所在控件的this.gameObject了
        /// </summary>
        public GameObject Carrier;
        

        public BuffData Data;
        
        
        private Dictionary<Type, BuffEvent> _events;
    
        // public Buff(BuffData buffData)
        // {
        //     _events = buffData.Events.ToDictionary(e => e.GetType());
        // }

        public Buff(BuffData buffData,
            GameObject caster,
            GameObject carrier,
            float duration,
            int stack, 
            bool permanent)
        {
            _events = buffData.Events.ToDictionary(e => e.GetType());
            Data = buffData;
            Caster = caster;
            Carrier = carrier;
            Duration = duration;
            Stack = stack;
            Permanent = permanent;
        }

        public void TriggerEvent<TBuffEvent, TEventActArgs>(TEventActArgs args)
            where TBuffEvent : BuffEvent<TEventActArgs> where TEventActArgs : class, IEventActArgs
        {
            var key = typeof(TBuffEvent);

            if (_events.TryGetValue(key, out var buffEvent))
            {
                if (buffEvent is TBuffEvent tbuffEvent)
                {
                    args.Buff = this;
                    tbuffEvent.Trigger(args);
                }
            }
        }
    }

}