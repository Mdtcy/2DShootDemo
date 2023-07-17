using LWShootDemo.Entities;

namespace GameMain
{
    public class CharacterData : EntityDataBase
    {
        public Side Side;
        
        public int MaxHp;

        public float Speed;
        
        public CharacterData(int entityId, int typeId) : base(entityId, typeId)
        {
        }
    }
}