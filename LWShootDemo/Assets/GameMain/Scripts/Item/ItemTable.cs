namespace GameMain.Item
{
    public class ItemTable : SOTableList<ItemProp>
    {
        public override int StartId => IdUtility.ItemId();
    }   
}