using Animancer;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace LWShootDemo.Entities
{
    public enum AnimationType
    {
        Idle = 0,
        Attack = 1,
        Walk = 2,
        Dead = 3,
    }

    public class UnitAnimation : MonoBehaviour
    {
        public AnimancerComponent Animancer;
        
        public AnimationClip Idle;
        public AnimationClip Walk;
        public AnimationClip Attack;
        public AnimationClip Dead;

        public void Play(AnimationType animationType)
        {
            switch (animationType)
            {
                case AnimationType.Idle:
                    Animancer.Play(Idle);
                    break;
                case AnimationType.Walk:
                    Animancer.Play(Walk);
                    break;
                case AnimationType.Attack:
                    Animancer.Play(Attack);
                    break;
                case AnimationType.Dead:
                    Animancer.Play(Dead);
                    break;
                default:
                    Log.Error($"AnimationType {animationType} 未定义的类型");
                    break;
            }
        }
    }
}