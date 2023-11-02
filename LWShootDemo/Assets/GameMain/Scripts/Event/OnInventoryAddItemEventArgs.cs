using GameFramework;
using GameFramework.Event;
using GameMain.Item;

namespace GameMain
{
    public class OnInventoryAddItemEventArgs : GameEventArgs
    {
        public static int EventId = typeof(OnInventoryAddItemEventArgs).GetHashCode();
        public override int Id => EventId;
        
        public int ItemId;
        public int Count;
    
        public static OnInventoryAddItemEventArgs Create(int itemId, int count)
        {
            var args = ReferencePool.Acquire<OnInventoryAddItemEventArgs>();
            args.ItemId = itemId;
            args.Count = count;
            return args;
        }
        
        public override void Clear()
        {
            
        }
    }
}