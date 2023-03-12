/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年3月12日
 * @modify date 2023年3月12日
 * @desc [任务组UI]
 */

#pragma warning disable 0649
using UnityEngine;

namespace LWShootDemo.Popups
{
    public class PopupManager : MonoBehaviour
    {
        #region FIELDS

        [SerializeField]
        private PopupPrefabDictionary popupPrefabMap;

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        public void Create(Vector3 position, PopupType type)
        {
            // 没配置的就不弹了
            if (!popupPrefabMap.ContainsKey(type))
            {
                return;
            }

            Debug.Assert(popupPrefabMap[type] != null);

            var popup = Instantiate(popupPrefabMap[type], position, Quaternion.identity);
            popup.Play();
        }

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649