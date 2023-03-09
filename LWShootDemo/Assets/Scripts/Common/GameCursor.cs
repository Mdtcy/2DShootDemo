/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年3月9日
 * @modify date 2023年3月9日
 * @desc [游戏指针]
 */

#pragma warning disable 0649
using LWShootDemo.Managers;
using UnityEngine;

namespace LWShootDemo.Common
{
    /// <summary>
    /// 游戏指针
    /// </summary>
    public class GameCursor : MonoBehaviour
    {
        #region FIELDS

        private Camera mainCamera;

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        void Start()
        {
            mainCamera     = GameManager.Instance.MainCamera;
            Cursor.visible = false;
        }

        private void Update()
        {
            Cursor.visible = false;

            Vector2 cursorPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            transform.position = cursorPos;
        }

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649