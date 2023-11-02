using Sirenix.OdinInspector;
using UnityEngine;

namespace GameMain
{
    public class UIFollowForm : UGuiFormLogic
    {
        protected Transform FollowTarget;
        
        [LabelText("跟随UI")]
        [SerializeField]
        protected RectTransform _view;
        
        [LabelText("偏移")]
        [SerializeField]
        protected Vector2 _offset;

        [LabelText("实际追踪目标位置")]
        [ShowInInspector]
        [ReadOnly]
        protected virtual Vector3 ActualFollowPos
        {
            get
            {
                if (FollowTarget != null)
                {
                    return FollowTarget.position;
                }
                else
                {
                    return Vector3.zero;
                }
            }
        }

        protected virtual void LateUpdate()
        {
            UpdatePos();
        }

        protected void UpdatePos()
        {
            var camera = Camera.main;
            var uicamera = GameEntry.UI.UICamera;
            var worldPos = ActualFollowPos + new Vector3(_offset.x, _offset.y, 0);
            // 将_attrPointInteract的位置转换到屏幕坐标
            var screenPos = camera.WorldToScreenPoint(worldPos);
            
            // 获取Canvas的RectTransform
            RectTransform canvasRectTransform = (RectTransform)_view.parent;
            // 将屏幕坐标转换到Canvas的坐标
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, screenPos, uicamera, out var localPos);
            _view.anchoredPosition = localPos;
        }
    }
}