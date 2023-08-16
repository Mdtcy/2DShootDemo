using UnityEngine;

namespace GameMain
{
    public class AoeState : EntityLogicBase
    {
        private AoeProp _prop;
        
        public bool ContainTag(ProjectileTag tag)
        {
            return _prop.Tag.HasFlag(tag);
        }
    }
}