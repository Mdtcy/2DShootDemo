/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年3月11日
 * @modify date 2023年3月11日
 * @desc [声音配置]
 */

#pragma warning disable 0649
using System;
using System.Collections.Generic;
using UnityEngine;

namespace LWShootDemo.Sound
{
    /// <summary>
    /// 声音配置
    /// </summary>
    [Serializable]
    public class SoundConfig
    {
        public SoundType Type;

        public List<AudioClip> AudioClips;
    }
}
#pragma warning restore 0649