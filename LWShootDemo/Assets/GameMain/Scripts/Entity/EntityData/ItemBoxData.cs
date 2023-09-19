using System.Collections.Generic;
using GameMain.Item;

namespace GameMain
{
    public class ItemBoxData : EntityDataBase
    {
        public List<ItemRarity> Raritys;

        public ItemBoxData(int entityId, int typeId, List<ItemRarity> raritys) : base(entityId, typeId)
        {
            Raritys = raritys;
        }
    }
}