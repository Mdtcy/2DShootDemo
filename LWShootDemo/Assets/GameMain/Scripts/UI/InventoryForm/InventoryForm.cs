using System.Collections.Generic;
using GameFramework.Event;
using GameMain.Item;
using UnityEngine;

namespace GameMain
{
    public class InventoryForm : UGuiFormLogic
    {
        // key: itemId, value: UIItem
        private Dictionary<int, UIItem> _itemMaps = new();
        private Inventory _inventory;
        private ItemTable _itemTable;
        
        [SerializeField] 
        private Transform _root;

        [SerializeField] 
        private UIItem _pfbUIItem;

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            GameEntry.Event.Subscribe(OnInventoryAddItemEventArgs.EventId, OnAddItem);
            _inventory = GameEntry.SceneBlackBoard.Inventory;
            _itemTable = GameEntry.TableConfig.Get<ItemTable>();
        }

        private void OnAddItem(object sender, GameEventArgs e)
        {
            var args = e as OnInventoryAddItemEventArgs;
            int itemId = args.ItemId;
            if (_itemMaps.ContainsKey(itemId))
            {
                var uiItem = _itemMaps[itemId];
                uiItem.SetCount(_inventory.GetItemCount(itemId));
            }
            else
            {
                var uiItem = Instantiate(_pfbUIItem, transform); 
                _itemMaps.Add(itemId, uiItem);
                uiItem.transform.parent = _root;
                uiItem.transform.SetAsLastSibling();
                uiItem.Setup(_itemTable.Get(itemId), _inventory.GetItemCount(itemId));
            }
        }
    }
}