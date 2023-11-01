namespace GameMain
{
    public class CoinPickUpData : EntityDataBase
    {
        public float Speed;

        public CoinPickUpData(EntityProp entityProp, float speed) : base(entityProp)
        {
            Speed = speed;
        } 
    }
}