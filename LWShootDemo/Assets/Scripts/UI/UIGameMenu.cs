/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年3月12日
 * @modify date 2023年3月12日
 * @desc [菜单UI]
 */

#pragma warning disable 0649
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace LWShootDemo.UI
{
    /// <summary>
    /// 菜单UI
    /// </summary>
    public class UIGameMenu : MonoBehaviour
    {
        #region FIELDS

        [SerializeField]
        private Button btnStartGame;

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        private void Awake()
        {
            btnStartGame.onClick.AddListener(EnterGame);
        }

        private void EnterGame()
        {
            SceneManager.LoadScene("Game");
        }

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649