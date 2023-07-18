namespace GameMain
{
    public class EntityTable : SOTableList<EntityProp>
    {
        public override int StartId => IdUtility.EntityStartId();
    }
}