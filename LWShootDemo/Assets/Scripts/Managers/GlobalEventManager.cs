/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年3月9日
 * @modify date 2023年3月9日
 * @desc [任务组UI]
 */

#pragma warning disable 0649
using UnityEngine;

namespace LWShootDemo.Managers
{
    public class GlobalEventManager : MonoBehaviour
    {
        #region FIELDS

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        public void OnShoot()
        {
            // Camera.main.GetComponent<CameraController>()
            //       .Shake((transform.position - ammoEmitter.position).normalized, 5f, 0.05f);
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