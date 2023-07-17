using GameFramework;
using GameFramework.Event;
using UnityGameFramework.Runtime;

namespace GameMain
{
    public class NumericChangeEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(NumericChangeEventArgs).GetHashCode();

        public override int Id => EventId;

        public Entity Entity
        {
            get;
            private set;
        }
        
        public NumericType NumericType
        {
            get;
            private set;
        }
        
        public int FinalValue
        {
            get;
            private set;
        }

        public override void Clear()
        {
            Entity = null;
            // NumericType = NumericType.None;
            FinalValue = 0;
        }
        
        public static NumericChangeEventArgs Create(Entity entity,
            NumericType numericType,
            int finalValue)
        {
            var eventArgs = ReferencePool.Acquire<NumericChangeEventArgs>();
            eventArgs.Entity = entity;
            eventArgs.NumericType = numericType;
            eventArgs.FinalValue = finalValue;
            return eventArgs;
        }
    }
}