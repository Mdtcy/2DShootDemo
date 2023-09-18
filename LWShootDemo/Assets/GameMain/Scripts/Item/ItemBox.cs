using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Gamelogic.Extensions.Algorithms;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameMain.Item
{
    /// <summary>
    /// 打开后随机获得道具
    /// </summary>
    public class ItemBox : MonoBehaviour
    {
        [Tooltip("可以生成的道具稀有度")]
        public List<ItemRarity> Raritys;

        [SerializeField] 
        private ItemInteract _itemInteract;

        [SerializeField] private Sprite _model;
        
        private bool _hasOpen = false;
        
        [Button]
        public async UniTask Generate()
        {
            var item = Instantiate(_itemInteract);
            var allItems = GameEntry.TableConfig.Get<ItemTable>().TableList;

            var tmpList = new List<ItemProp>();
            foreach (var itemProp in allItems)
            {
                if (Raritys.Contains(itemProp.Rarity))
                {
                    tmpList.Add(itemProp);
                }
            }

            var randomItem = tmpList.RandomItem();
            item.Setup(randomItem, transform.position);
            
            await UniTask.Yield(); // 异步等待一帧

            Destroy(gameObject);
        }
        
        private void OnTriggerStay2D(Collider2D other)
        {
            if(_hasOpen == true)
            {
                return;
            }
            
            var player = other.GetComponent<Player>();
            
            if (player != null && Input.GetKeyDown(KeyCode.F))
            {
                _hasOpen = true;
                Generate().Forget();
            }
        }
    }
}