using UnityEngine;

namespace GameMain
{
    public class ProjectileData : EntityDataBase
    {
        public ProjectileProp Prop;
        public Character Caster;
        
        public ProjectileData(EntityProp entityProp, 
            ProjectileProp projectileProp,
            Character caster) : base(entityProp)
        {
            Prop = projectileProp;
            Caster = caster;
        }

    }
}