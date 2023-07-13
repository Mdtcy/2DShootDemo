using GameMain;
using UnityEngine;

namespace LWShootDemo.Skills
{
    public class SkillSystem : MonoBehaviour
    {
        private Skill _currentSkill;
        
        public bool TryToCast(SkillData skillData)
        {
            if(CanCast(skillData))
            {
                Cast(skillData);
                return true;   
            }
            else
            {
                return false;
            }
        }

        public bool CanCast(SkillData skillData)
        {
            if (_currentSkill != null)
            {
                return _currentSkill.CanBeInterrupted();
            }
            else
            {
                return true;   
            }
        }

        public void Cast(SkillData skillData)
        {
            if (_currentSkill != null)
            {
                _currentSkill.Interrupt();
            }

            var skill = new Skill();
            skill.Init(skillData);
            
            // todo 
            TimelineObj timeline = new TimelineObj(
                DesingerTables.TimelineModels.data[skillData.TimeLine], this.gameObject, skill);
            
            GameEntry.TimeLine.AddTimeline(timeline);
        }

        private void Update()
        {
            if (_currentSkill != null)
            {
                _currentSkill.Update(Time.deltaTime);
                if (_currentSkill.HasComplete)
                {
                    _currentSkill = null;
                }
            }
        }
    }
}