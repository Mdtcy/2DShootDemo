/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年3月11日
 * @modify date 2023年3月11日
 * @desc [声音字典]
 */

#pragma warning disable 0649
using System;
using UnityEngine;

namespace LWShootDemo.Sound
{
    /// <summary>
    /// 声音字典
    /// </summary>
    [Serializable]
    public class SoundDictionary : UnitySerializedDictionary<SoundType, AudioClip>
    {
    }
}
#pragma warning restore 0649