namespace GameMain
{
    public class FruitData : EntityDataBase
    {
        public int RecoverHp { get; }

        public FruitData(int entityId, int typeId, int recoverHp) : base(entityId, typeId)
        {
            RecoverHp = recoverHp;
        }
    }
}