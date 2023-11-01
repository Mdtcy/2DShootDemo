using GameMain.Item;

namespace GameMain
{
    public class ItemInteractData : EntityDataBase
    {
        public float PopUpHeight;
        public float PopUpDuration;
        public ItemProp ItemProp;
        
        public ItemInteractData(EntityProp entityProp,
            float popUpHeight, 
            float popUpDuration, 
            ItemProp itemProp) :
            base(entityProp)
        {
            PopUpDuration = popUpDuration;
            PopUpHeight = popUpHeight;
            ItemProp = itemProp;
        }
    }
}