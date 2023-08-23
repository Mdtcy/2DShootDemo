namespace GameMain
{
    public class AoeData : EntityDataBase
    {
        public AoeProp Prop;
        public Character Caster;
        public float Radius;
        
        public AoeData(int entityId, int typeId, AoeProp prop, Character caster, float radius) : base(entityId, typeId)
        {
            Prop = prop;
            Caster = caster;
            Radius = radius;
        }
    }
}