using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace GameMain
{
    public class UIItemInfo : MonoBehaviour
    {
        [SerializeField] 
        private TextMeshProUGUI _txtDesc;

        [SerializeField] 
        private Vector2 _offset;

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Show(UIItem uiItem)
        {
            gameObject.SetActive(true);
            _txtDesc.text = uiItem.ItemProp.Description;
        }

        private void LateUpdate()
        {
            var uicamera = GameEntry.UI.UICamera;
            var screenPos = Input.mousePosition;
            var rectTransform = (RectTransform) transform;
            // 获取Canvas的RectTransform
            RectTransform canvasRectTransform = (RectTransform)transform.parent;
            // 将屏幕坐标转换到Canvas的坐标
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, screenPos, uicamera, out var localPos);
            rectTransform.anchoredPosition = localPos + _offset;
        }
    }
}