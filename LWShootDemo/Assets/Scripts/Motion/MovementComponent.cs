using System;
using LWShootDemo.Entities.Player;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace LWShootDemo.Motion
{
    public class MovementComponent : MonoBehaviour
    {
        [SerializeField] 
        private Rigidbody2D _rb2D;

        [SerializeField] 
        private float _speed;

        [SerializeField] 
        private FaceController _faceController;

        public Vector3 mVelocity;
        public MotionClip mCurrentMotionClip;

        public bool DebugMode;
        
        // local
        private Vector2 _inputBuffer;
        
        public Direction FaceDirection => _faceController.FaceDirection;

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
                mVelocity = CalcVelocity();
            }
            
            ApplyMotionToVelocity(Time.deltaTime);
            
            // 应用速度
            _rb2D.velocity = mVelocity;

            if (_faceController != null)
            {
                if(mVelocity.x > 0)
                {
                    _faceController.Face(Direction.Right);
                }
                else if(mVelocity.x < 0)
                {
                    _faceController.Face(Direction.Left);
                }
            }
        }
        
        private void ApplyMotionToVelocity(float elapsedTime)
        {
            if (mCurrentMotionClip == null)
            {
                return;
            }
            
            if (DebugMode)
            {
                Log.Debug($"MotionClip: {mCurrentMotionClip.GetType().Name}, 速度: {mVelocity} , 持续时间:{mCurrentMotionClip.ElapsedTime}");
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
                mVelocity = mCurrentMotionClip.GetVelocity();
            }
            else
            {
                mVelocity += mCurrentMotionClip.GetVelocity();
            }
        }

        public Vector2 CalcVelocity()
        {
            // 这里使用输入的方向（已经被规范化为长度为1）乘以角色的移动速度
            return _inputBuffer.normalized * _speed;
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

        public void SetSpeed(float characterDataSpeed)
        {
            _speed = characterDataSpeed;
        }
    }
}