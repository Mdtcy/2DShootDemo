using System.Collections.Generic;

namespace GameMain
{
    public class AoeData : EntityDataBase
    {
        public AoeProp Prop;
        public Character Caster;
        public float Radius;
        public Dictionary<string, object> Params;

        public AoeData(int entityId,
            int typeId, 
            AoeProp prop, 
            Character caster, 
            float radius,
            Dictionary<string, object> pParams = null) : base(entityId, typeId)
        {
            Prop = prop;
            Caster = caster;
            Radius = radius;
            Params = pParams;
        }
    }
}