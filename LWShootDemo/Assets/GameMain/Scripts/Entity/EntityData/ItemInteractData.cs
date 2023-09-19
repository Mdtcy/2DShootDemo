using GameMain.Item;

namespace GameMain
{
    public class ItemInteractData : EntityDataBase
    {
        public float PopUpHeight;
        public float PopUpDuration;
        public ItemProp ItemProp;
        
        public ItemInteractData(float popUpHeight, float popUpDuration, ItemProp itemProp, int entityId, int typeId) : base(entityId, typeId)
        {
            PopUpDuration = popUpDuration;
            PopUpHeight = popUpHeight;
            ItemProp = itemProp;
        }
    }
}