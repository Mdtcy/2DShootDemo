/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年3月11日
 * @modify date 2023年3月11日
 * @desc [时停管理器]
 */

#pragma warning disable 0649
using System.Collections;
using UnityEngine;

namespace LWShootDemo.TimeStop
{
    /// <summary>
    /// 时停管理器
    /// </summary>
    public class TimeStopManager : MonoBehaviour
    {
        #region FIELDS

        // 是否正在时停
        private bool stoping;

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// 更改TimeScale一段时间
        /// </summary>
        /// <param name="timeScale"></param>
        /// <param name="delay"></param>
        public void StopTime(float timeScale, float delay)
        {
            if (stoping)
            {
                return;
            }

            Debug.Assert(delay > 0);

            StopCoroutine(StartTimeAgain(delay));
            StartCoroutine(StartTimeAgain(delay));
            Time.timeScale = timeScale;
        }

        private IEnumerator StartTimeAgain(float delay)
        {
            stoping = true;
            yield return new WaitForSecondsRealtime(delay);
            Time.timeScale = 1f;
            stoping        = false;
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