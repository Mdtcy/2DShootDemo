/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年3月9日
 * @modify date 2023年3月9日
 * @desc [游戏内光标]
 */

#pragma warning disable 0649
using DG.Tweening;
using LWShootDemo.Managers;
using UnityEngine;

namespace LWShootDemo.Common
{
    /// <summary>
    /// 游戏内光标
    /// </summary>
    public class GameCursor : MonoBehaviour
    {
        #region FIELDS

        private Camera mainCamera;

        // 最小缩放比例
        [SerializeField]
        private float minScale;

        // 最大缩放比例
        [SerializeField]
        private float maxScale;

        // 缩放速度
        [SerializeField]
        private float speed;

        // 恢复速度
        [SerializeField]
        private float recoverSpeed;

        // 鼠标按下时的缩放比例
        [SerializeField]
        private Vector3 scaleOnMouseDown;

        // 目标缩放比例
        private Vector3 targetScale;

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

        private float resetSpeed;
        private float loopSpeed;

        private void Update()
        {
            // 鼠标按下时，恢复缩放比例为1
            if (Input.GetMouseButton(0))
            {
                targetScale = scaleOnMouseDown;
            }
            // 鼠标抬起时，让光标在两个缩放比例之间来回循环
            else
            {
                float t     = Mathf.PingPong(Time.time * speed, 1f);
                float scale = Mathf.Lerp(minScale, maxScale, t);
                targetScale = new Vector3(scale, scale, 1f);
            }
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * recoverSpeed);

            Vector2 cursorPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            transform.position = cursorPos;

            Cursor.visible = false;

        }

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649