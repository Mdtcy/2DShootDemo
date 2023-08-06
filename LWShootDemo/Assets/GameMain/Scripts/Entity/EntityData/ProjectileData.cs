using UnityEngine;

namespace GameMain
{
    public class ProjectileData : EntityDataBase
    {
        public ProjectileProp Prop;
        public Character Caster;
        
        public ProjectileData(int entityId, 
            int typeId, 
            ProjectileProp projectileProp,
            Character caster) : base(entityId, typeId)
        {
            Prop = projectileProp;
            Caster = caster;
        }

    }
}