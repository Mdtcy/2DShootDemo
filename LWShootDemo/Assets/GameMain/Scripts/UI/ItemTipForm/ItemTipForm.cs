using DG.Tweening;
using GameMain.Item;
using TMPro;
using UnityEngine;

namespace GameMain
{
    public class ItemTipForm : UIFollowForm
    {
        [SerializeField]
        private TextMeshProUGUI _txtName;
        
        [SerializeField]
        private TextMeshProUGUI _txtDesc;
        
        [SerializeField] 
        private float _autoCloseTime = 4;

        protected override Vector3 ActualFollowPos => _followPos + Vector3.up * _extraHeight;

        // local
        private ItemInteract _interact;

        private Tween _moveTween;
        private float _extraHeight = 0;
        private Vector3 _followPos;

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            _interact = userData as ItemInteract;
            FollowTarget = _interact.transform;
            
            // 获取Interact的位置作为跟随的位置原点
            var bounds = _interact.Collider2D.bounds;
            _followPos = bounds.center - Vector3.up * (bounds.extents.y);
            
            var prop = _interact.ItemProp;
            _txtName.text = prop.Name;
            _txtDesc.text = prop.Description;
            _extraHeight = 0;

            // 使用Dotween 更改extraHeight的值
            _moveTween = DOTween.To(() => _extraHeight, 
                    x => _extraHeight = x, 0.7f, _autoCloseTime)
               .OnComplete(Close)
               .Play();
        }

        protected override void OnClose(bool isShutdown, object userData)
        {
            base.OnClose(isShutdown, userData);
            _moveTween.Kill();
            _moveTween = null;
        }
    }
}