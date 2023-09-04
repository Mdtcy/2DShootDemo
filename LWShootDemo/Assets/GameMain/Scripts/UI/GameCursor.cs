/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年3月9日
 * @modify date 2023年3月9日
 * @desc [游戏内光标]
 */

#pragma warning disable 0649
using UnityEngine;

namespace LWShootDemo.Common
{
    /// <summary>
    /// 游戏内光标
    /// </summary>
    public class GameCursor : MonoBehaviour
    {
        #region FIELDS

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

        private RectTransform canvasRectTransform;

        [SerializeField]
        private Canvas canvas;

        // 目标缩放比例
        private Vector3 targetScale;
        private float   resetSpeed;
        private float   loopSpeed;
        private RectTransform rectTransform;

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
            Cursor.visible = false;
            rectTransform  = GetComponent<RectTransform>();
            canvasRectTransform = canvas.GetComponent<RectTransform>();
        }

        private void Update()
        {
            // 让鼠标在两个缩放比例之间来回循环，当鼠标按下时，恢复缩放比例为1
            if (Input.GetMouseButton(0))
            {
                targetScale = scaleOnMouseDown;
            }
            else
            {
                float t     = Mathf.PingPong(Time.time * speed, 1f);
                float scale = Mathf.Lerp(minScale, maxScale, t);
                targetScale = new Vector3(scale, scale, 1f);
            }

            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * recoverSpeed);

            Cursor.visible = false;

            ChaseMouse();
        }

        // 跟随鼠标
        private void ChaseMouse()
        {
            // 将UI位置跟随鼠标
            Vector2 uiLocalPosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform,
                                                                    Input.mousePosition, canvas.worldCamera,
                                                                    out uiLocalPosition);
            rectTransform.anchoredPosition = uiLocalPosition;
        }

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649