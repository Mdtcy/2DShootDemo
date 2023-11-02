using System;
using GameFramework.Event;
using GameMain.Item;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameMain
{
    public class UIItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] 
        private Image _imgIcon;

        [SerializeField] 
        private TextMeshProUGUI _txtCount;

        public void Setup(ItemProp itemProp, int count)
        {
            _imgIcon.sprite = itemProp.Model;
            // _imgIcon.SetNativeSize();

            _txtCount.text = count == 1 ? String.Empty : $"X{count}";
        }

        public void SetCount(int count)
        {
            _txtCount.text = count == 1 ? String.Empty : $"X{count}";
        }


        public void OnPointerEnter(PointerEventData eventData)
        {
        }

        public void OnPointerExit(PointerEventData eventData)
        {
        }
    }
}