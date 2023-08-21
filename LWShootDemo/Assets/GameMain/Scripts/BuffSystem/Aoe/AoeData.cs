namespace GameMain
{
    public class AoeData : EntityDataBase
    {
        public AoeProp Prop;
        public Character Caster;
        
        public AoeData(int entityId, int typeId, AoeProp prop, Character caster) : base(entityId, typeId)
        {
            Prop = prop;
            Caster = caster;
        }
    }
}