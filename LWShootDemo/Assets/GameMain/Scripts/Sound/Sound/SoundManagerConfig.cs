using System.Collections.Generic;
using UnityEngine;

namespace LWShootDemo.Sound
{
    [CreateAssetMenu(fileName = "SoundConfig", menuName = "SoundConfig", order = 0)]
    public class SoundManagerConfig : ScriptableObject
    {
        public List<SoundConfig> SoundConfigs;
    }
}