using LWShootDemo.Entities;

namespace GameMain
{
    public class CharacterData : EntityDataBase
    {
        public int MaxHp;

        public float Speed;
        
        public int PropID;
        
        public CharacterData(int entityId, int typeId) : base(entityId, typeId)
        {
        }
    }
}