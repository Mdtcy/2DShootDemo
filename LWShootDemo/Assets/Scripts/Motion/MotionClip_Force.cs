using UnityEngine;

namespace LWShootDemo.Motion
{
    public class MotionClip_Force : MotionClip
    {
        Vector3 _force;
        AnimationCurve _curve;
        
        public MotionClip_Force(bool bOverrideMotion, float duration, Vector3 force, AnimationCurve curve) : base(bOverrideMotion, duration)
        {
            _force = force;
            _curve = curve;
        }
        
        public override void StartMotion()
        {
            base.StartMotion();
           
        }

        public override void UpdateMotion(float deltaTime)
        {
            base.UpdateMotion(deltaTime);
            velocity = (_curve == null ? _force : _curve.Evaluate(ElapsedTime / Duration) * _force);
        }

        public override void EndMotion()
        {
            base.EndMotion();
            // 这里你可以添加击退结束后的处理，比如清除击退效果等
        }
    }
}