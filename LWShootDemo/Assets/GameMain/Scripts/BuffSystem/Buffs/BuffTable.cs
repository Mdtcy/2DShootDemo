
namespace GameMain
{
    public class BuffTable : SOTableList<BuffData>
    {
        public override int StartId => IdUtility.BuffStartId();
    }
}