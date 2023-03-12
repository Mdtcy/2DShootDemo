/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年3月12日
 * @modify date 2023年3月12日
 * @desc [任务组UI]
 */

using System;

#pragma warning disable 0649
namespace LWShootDemo.Popups
{
    [Serializable]
    public class PopupPrefabDictionary : UnitySerializedDictionary<PopupType, Popup>
    {
    }
}
#pragma warning restore 0649