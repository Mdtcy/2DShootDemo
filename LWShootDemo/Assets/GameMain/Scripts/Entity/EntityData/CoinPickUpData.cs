namespace GameMain
{
    public class CoinPickUpData : EntityDataBase
    {
        public float Speed;

        public CoinPickUpData(int entityId, int typeId, float speed) : base(entityId, typeId)
        {
            Speed = speed;
        } 
    }
}