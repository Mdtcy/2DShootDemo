using UnityEngine;
using UnityGameFramework.Runtime;

namespace LWShootDemo.DamageNumber
{
    public class PopupComponent : GameFrameworkComponent
    {
        // todo
        [SerializeField]
        private PopupManagerSetting _setting;
        
        public void Spawn(Vector3 position, int num, PopupType popupType)
        {
            _setting.PopupMap[popupType].GetComponent<DamageNumbersPro.DamageNumber>().Spawn(position, num);
        }
        
        public void Spawn(Vector3 position, int num, PopupType popupType, Transform followTransform)
        {
            _setting.PopupMap[popupType].GetComponent<DamageNumbersPro.DamageNumber>().Spawn(position, num, followTransform);
        }
    }
}