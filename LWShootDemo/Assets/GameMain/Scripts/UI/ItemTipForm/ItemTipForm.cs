using GameMain.Item;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameMain
{
    public class ItemTipForm : UIFollowForm
    {
        [SerializeField]
        private TextMeshProUGUI _txtName;
        
        [SerializeField]
        private TextMeshProUGUI _txtDesc;

        [SerializeField] 
        private Image _imgIcon;

        protected override Vector3 ActualFollowPos
        {
            get
            {
                if (_interact != null)
                {
                    var bounds = _interact.Collider2D.bounds;
                    return bounds.center - Vector3.up * (bounds.extents.y + 1);
                }
                else
                {
                    return Vector3.zero;
                }
            }
        }

        // local
        private ItemInteract _interact;

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            _interact = userData as ItemInteract;
            FollowTarget = _interact.transform;
            
            var prop = _interact.ItemProp;
            _txtName.text = prop.Name;
            _txtDesc.text = prop.Description;
            _imgIcon.sprite = prop.Model;
            _imgIcon.SetNativeSize();
        }
    }
}