using LWShootDemo.Entities.Player;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace GameMain
{
    public class MovementComponent : MonoBehaviour
    {
        [SerializeField] 
        private Rigidbody2D _rb2D;
        
        [ShowInInspector]
        [ReadOnly]
        private Vector3 _velocity;
        public MotionClip mCurrentMotionClip;

        public bool DebugMode;
        
        // local
        private Vector2 _inputBuffer;

        void Start()
        {
            // 初始化，你可能需要设置mCurrentMotionClip为默认的运动效果
        }

        void FixedUpdate()
        {
            // 如果没有覆盖运动，那么就通过正常的方式移动角色
            if (mCurrentMotionClip == null || !mCurrentMotionClip.bOverrideMotion)
            {
                // todo 
                _velocity = CalcVelocity();
            }
            
            ApplyMotionToVelocity(Time.deltaTime);
            
            // 应用速度
            _rb2D.velocity = _velocity;
        }

        public void Stop()
        {
            _rb2D.velocity = _velocity;
            InputMove(Vector2.zero);
        }

        private void ApplyMotionToVelocity(float elapsedTime)
        {
            if (mCurrentMotionClip == null)
            {
                return;
            }
            
            if (DebugMode)
            {
                Log.Debug($"MotionClip: {mCurrentMotionClip.GetType().Name}, 速度: {_velocity} , 持续时间:{mCurrentMotionClip.ElapsedTime}");
            }
            
            // 更新MotionClip
            mCurrentMotionClip.ElapsedTime += elapsedTime;
            if(mCurrentMotionClip.ElapsedTime >= mCurrentMotionClip.Duration)
            {
                mCurrentMotionClip.EndMotion();
                mCurrentMotionClip = null;
                return;
            }

            mCurrentMotionClip.UpdateMotion(Time.deltaTime);

            if (mCurrentMotionClip.bOverrideMotion)
            {
                _velocity = mCurrentMotionClip.GetVelocity();
            }
            else
            {
                _velocity += mCurrentMotionClip.GetVelocity();
            }
        }

        public Vector2 CalcVelocity()
        {
            return _inputBuffer;
        }

        public void PlayMotionClip(MotionClip clip)
        {
            // 结束当前的MotionClip
            StopCurrentMotionClip();
            
            // 开始新的MotionClip
            mCurrentMotionClip = clip;
            mCurrentMotionClip.StartMotion();
        }
        
        public void StopCurrentMotionClip()
        {
            // 结束当前的MotionClip
            if (mCurrentMotionClip != null)
            {
                mCurrentMotionClip.EndMotion();
                mCurrentMotionClip = null;
            }
        }

        public void InputMove(Vector2 input)
        {
            _inputBuffer = input;
        }
    }
}