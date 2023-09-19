using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Gamelogic.Extensions.Algorithms;
using UnityEngine;

namespace GameMain.Item
{
    /// <summary>
    /// 打开后随机获得道具
    /// </summary>
    public class ItemBox : EntityLogicBase
    {
        // 可以生成的道具稀有度
        private List<ItemRarity> _raritys;

        private bool _hasOpen = false;
        int? tipFormId = null;

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            var data = userData as ItemBoxData;
            _raritys = data.Raritys;
            _hasOpen = false;
        }

        private async UniTask Generate()
        {
            // 根据给定稀有度随机一个Item
            var allItems = GameEntry.TableConfig.Get<ItemTable>().TableList;
            var tmpList = new List<ItemProp>();
            foreach (var itemProp in allItems)
            {
                if (_raritys.Contains(itemProp.Rarity))
                {
                    tmpList.Add(itemProp);
                }
            }

            var randomItem = tmpList.RandomItem();

            int id = GameEntry.Entity.GenerateSerialId();
            GameEntry.Entity.ShowItemInteract(new ItemInteractData(1,
                0.5f,
                randomItem,
                id,
                10300009)
            {
                Position = transform.position,
                Rotation = Quaternion.identity,
                Scale = Vector3.one,
            });

            await UniTask.Yield(); // 异步等待一帧

            GameEntry.Entity.HideEntity(this);
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (_hasOpen == true)
            {
                return;
            }

            OpenTip();
            var player = other.GetComponent<Player>();

            if (player != null && Input.GetKeyDown(KeyCode.F))
            {
                _hasOpen = true;
                Generate().Forget();
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (_hasOpen == true)
            {
                return;
            }

            TryCloseTip();
        }

        protected override void OnHide(bool isShutdown, object userData)
        {
            base.OnHide(isShutdown, userData);
            TryCloseTip();
        }

        private void OpenTip()
        {
            if (tipFormId == null)
            {
                tipFormId = GameEntry.UI.OpenUIForm(UIFormId.CommonTip, (transform, "按F开启"));
            }
        }

        private void TryCloseTip()
        {
            if (tipFormId != null)
            {
                GameEntry.UI.CloseUIForm(tipFormId.Value);
                tipFormId = null;
            }
        }
    }
}