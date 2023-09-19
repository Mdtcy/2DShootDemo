using System;
using GameMain.Item;
using TMPro;
using UnityEngine;

namespace GameMain
{
    public class CommonTipForm : UIFollowForm
    {
        [SerializeField]
        private TextMeshProUGUI _txtContent;

        // protected override Vector3 ActualFollowPos
        // {
        //     get
        //     {
        //         if (_interact != null)
        //         {
        //             var bounds = _interact.Collider2D.bounds;
        //             return bounds.center - Vector3.up * (bounds.extents.y + 1);
        //         }
        //         else
        //         {
        //             return Vector3.zero;
        //         }
        //     }
        // }

        // local

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            var data = (ValueTuple<Transform, string>)userData;
            FollowTarget = data.Item1;
            _txtContent.text = data.Item2;
        }
    }
}