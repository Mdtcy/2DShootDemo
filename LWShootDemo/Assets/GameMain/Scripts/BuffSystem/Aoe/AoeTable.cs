namespace GameMain
{
    public class AoeTable : SOTableList<AoeProp>
    {
        public override int StartId => IdUtility.AoeStartId();
    }
}