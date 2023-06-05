using UnityEngine;

namespace LWShootDemo.Motion
{
    public abstract class MotionClip 
    {
        public bool bOverrideMotion = false;
        public Vector3 velocity = Vector3.zero;

        public float Duration;
        
        public float ElapsedTime;
        
        public MotionClip(bool bOverrideMotion, float duration)
        {
            this.bOverrideMotion = bOverrideMotion;
            Duration = duration;
        }

        public virtual Vector3 GetVelocity() 
        {
            return velocity;
        }

        public virtual void StartMotion()
        {
        }

        public virtual void UpdateMotion(float deltaTime)
        {
        }

        public virtual void EndMotion() { }
    }
}