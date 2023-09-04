/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年3月13日
 * @modify date 2023年3月13日
 * @desc [Player死亡事件]
 */

#pragma warning disable 0649
using UnityEngine;

namespace Events
{
    /// <summary>
    /// Player死亡事件
    /// </summary>
    public struct PlayDeathEvent
    {
        private static event Delegate OnEvent;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void RuntimeInitialization()
        {
            OnEvent = null;
        }

        public static void Register(Delegate callback)
        {
            OnEvent += callback;
        }

        public static void Unregister(Delegate callback)
        {
            OnEvent -= callback;
        }

        public delegate void Delegate();

        public static void Trigger()
        {
            OnEvent?.Invoke();
        }
    }
}
#pragma warning restore 0649