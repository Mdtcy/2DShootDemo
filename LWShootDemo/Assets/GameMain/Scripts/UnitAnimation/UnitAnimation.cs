using Animancer;
using GameMain;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace LWShootDemo.Entities
{
    public class UnitAnimation : MonoBehaviour
    {
        public AnimancerComponent Animancer;
        
        [System.Serializable]
        public class AnimationDictionary : SerializableDictionary<AnimationType, AnimationClip>
        {
        }
        
        [SerializeField]
        private AnimationDictionary _animationMap;

        public AnimancerState Play(AnimationType animationType)
        {
            if (_animationMap.TryGetValue(animationType, out var animationClip))
            {
                return Animancer.Play(animationClip);
            }
            else
            {
                Log.Error($"未配置AnimationType {animationType} 对应的动画");
                return null;
            }
        }
    }
}