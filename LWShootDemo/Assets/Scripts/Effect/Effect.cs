/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年3月11日
 * @modify date 2023年3月11日
 * @desc [效果]
 */

#pragma warning disable 0649
using UnityEngine;

namespace LWShootDemo.Effect
{
    /// <summary>
    /// 效果
    /// </summary>
    public abstract class Effect : MonoBehaviour
    {
        public abstract void Play();
    }
}
#pragma warning restore 0649