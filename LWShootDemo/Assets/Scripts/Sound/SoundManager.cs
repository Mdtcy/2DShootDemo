/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年3月11日
 * @modify date 2023年3月11日
 * @desc [声音管理器]
 */

#pragma warning disable 0649
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LWShootDemo.Sound
{
    /// <summary>
    /// 声音管理器
    /// </summary>
    public class SoundManager : MonoBehaviour
    {
        #region FIELDS

        [SerializeField]
        private AudioSource musicAudioSource;

        [SerializeField]
        private AudioSource sfxAudioSource;

        [SerializeField]
        [InlineEditor(InlineEditorModes.FullEditor)]
        private SoundManagerConfig soundManagerConfig;

        // local
        private Dictionary<SoundType, List<AudioClip>> soundMap;

        #endregion

        #region PROPERTIES
        #endregion

        #region PUBLIC METHODS


        /// <summary>
        /// 播放音效
        /// </summary>
        /// <param name="soundType"></param>
        public void PlaySfx(SoundType soundType)
        {
            var audioClip = GetAudioClip(soundType);
            sfxAudioSource.PlayOneShot(audioClip);
        }

        /// <summary>
        /// 播放音乐
        /// </summary>
        /// <param name="soundType"></param>
        public void PlayMusic(SoundType soundType)
        {
            var audioClip = GetAudioClip(soundType);
            musicAudioSource.PlayOneShot(audioClip);
        }

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        private void Start()
        {
            soundMap = new Dictionary<SoundType, List<AudioClip>>();

            foreach (var soundConfig in soundManagerConfig.SoundConfigs)
            {
                soundMap.Add(soundConfig.Type, soundConfig.AudioClips);
            }
        }

        /// <summary>
        /// 根据声音类型获取声音
        /// </summary>
        /// <param name="soundType"></param>
        /// <returns></returns>
        private AudioClip GetAudioClip(SoundType soundType)
        {
            return soundMap[soundType][Random.Range(0, soundMap[soundType].Count)];
        }

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649