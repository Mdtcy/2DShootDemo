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
            _uiItemInfo.Hide();
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
                uiItem.ActOnPointerEnterItem += OnPointerEnterItem;
                uiItem.ActOnPointerExitItem += OnPointerExitItem;
            }
        }

        public void DestroyAll()
        {
            foreach (var uiItem in _itemMaps.Values)
            {
                uiItem.ActOnPointerEnterItem -= OnPointerEnterItem;
                uiItem.ActOnPointerExitItem -= OnPointerExitItem;
                Destroy(uiItem.gameObject);
            }
            _itemMaps.Clear();
        }

        private UIItem _selectedUIItem;

        [SerializeField]
        private UIItemInfo _uiItemInfo;
        
        private void OnPointerEnterItem(UIItem uiItem)
        {
            _selectedUIItem = uiItem;
            _uiItemInfo.Show(_selectedUIItem);
        }
        
        private void OnPointerExitItem(UIItem uiItem)
        {
            _selectedUIItem = null;
            _uiItemInfo.Hide();
        }
    }
}