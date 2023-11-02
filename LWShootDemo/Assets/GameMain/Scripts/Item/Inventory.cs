using System.Collections.Generic;

namespace GameMain
{
    public class Inventory
    {
        // key: itemId, value: count
        public Dictionary<int, int> Items = new();
        
        /// <summary>
        /// 获取物品数量
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public int GetItemCount(int itemId)
        {
            return Items.ContainsKey(itemId) ? Items[itemId] : 0;
        }
        
        public void AddItem(int itemId)
        {
            AddItem(itemId, 1);
        }
        
        public void AddItem(int itemId, int count)
        {
            if (Items.ContainsKey(itemId))
            {
                Items[itemId] += count;
            }
            else
            {
                Items.Add(itemId, count);
            }
            
            GameEntry.Event.Fire(this, OnInventoryAddItemEventArgs.Create(itemId, count));
        }
    }
}