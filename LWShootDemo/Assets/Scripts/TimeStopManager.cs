/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年3月11日
 * @modify date 2023年3月11日
 * @desc [任务组UI]
 */

#pragma warning disable 0649
using System.Collections;
using UnityEngine;

namespace LWShootDemo
{
    public class TimeStopManager : MonoBehaviour
    {
        #region FIELDS

        private bool stoping;

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        public void StopTime(float delay)
        {
            if (stoping)
            {
                return;
            }

            Debug.Assert(delay > 0);

            StopCoroutine(StartTimeAgain(delay));
            StartCoroutine(StartTimeAgain(delay));
            Time.timeScale = 0.0f;
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