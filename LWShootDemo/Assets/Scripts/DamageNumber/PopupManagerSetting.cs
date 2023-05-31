using UnityEngine;

namespace LWShootDemo.DamageNumber
{
    [CreateAssetMenu(fileName = "PopupManagerSetting", menuName = "配置/PopupManagerSetting")]
    public class PopupManagerSetting : ScriptableObject
    {
        [System.Serializable]
        public class PopupDictionAry : UnitySerializedDictionary<PopupType, GameObject>
        {
        }
        
        public PopupDictionAry PopupMap;
    }
}