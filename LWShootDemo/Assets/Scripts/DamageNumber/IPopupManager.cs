using UnityEngine;

namespace LWShootDemo.DamageNumber
{
    public interface IPopupManager
    {
        public void Spawn(Vector3 position, int damage, PopupType popupType);
        
        public void Spawn(Vector3 position, int num, PopupType popupType, Transform followTransform);
    }
}