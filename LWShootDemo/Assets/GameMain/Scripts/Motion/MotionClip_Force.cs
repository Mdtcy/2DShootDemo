using UnityEngine;

namespace GameMain
{
    public class MotionClip_Force : MotionClip
    {
        Vector3 _dir;
        AnimationCurve _curve;
        float _intensity;
        
        public MotionClip_Force(bool bOverrideMotion, float duration, Vector3 dir, AnimationCurve curve, float intensity) : base(bOverrideMotion, duration)
        {
            _dir = dir;
            _curve = curve;
            _intensity = intensity;
        }
        
        public override void StartMotion()
        {
            base.StartMotion();
           
        }

        public override void UpdateMotion(float deltaTime)
        {
            base.UpdateMotion(deltaTime);
            velocity = (_curve == null ? _dir : _curve.Evaluate(ElapsedTime / Duration) * _dir) * _intensity;
        }

        public override void EndMotion()
        {
            base.EndMotion();
            // 这里你可以添加击退结束后的处理，比如清除击退效果等
        }
    }
}