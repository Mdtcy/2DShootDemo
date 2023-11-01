namespace GameMain
{
    public class FruitData : EntityDataBase
    {
        public int RecoverHp { get; }

        public FruitData(EntityProp entityProp, int recoverHp) : base(entityProp)
        {
            RecoverHp = recoverHp;
        }
    }
}