using GameMain;
using UnityEngine;

namespace LWShootDemo.Skills
{
    public class Skill
    {
        private SkillData _skillData;
        
        private float _elapsedTime;
        
        private bool _hasComplete;
        public bool HasComplete => _hasComplete;

        public void Init(SkillData skillData)
        {
            _skillData = skillData;
            _elapsedTime = 0;
            _hasComplete = false;
        }
        
        public void Update(float deltaTime)
        {
            _elapsedTime += deltaTime;
            
            if(_elapsedTime >= _skillData.Duration)
            {
                Complete();
            }
        }

        private void Complete()
        {
            _hasComplete = true;
        }
        
        public void Interrupt()
        {
            if (CanBeInterrupted())
            {
                _hasComplete = true;    
            }
            else
            {
                
            }
        }

        public bool CanBeInterrupted()
        {
            // 前摇和后摇可以被打断
            return _elapsedTime < _skillData.PreSwing || _elapsedTime >= _skillData.BackSwing;
        }
    }
}