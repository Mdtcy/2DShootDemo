using System.Collections.Generic;
using GameMain.Item;

namespace GameMain
{
    public class ItemBoxData : EntityDataBase
    {
        public List<ItemRarity> Raritys;

        public ItemBoxData(EntityProp entityProp, List<ItemRarity> raritys) : base(entityProp)
        {
            Raritys = raritys;
        }
    }
}