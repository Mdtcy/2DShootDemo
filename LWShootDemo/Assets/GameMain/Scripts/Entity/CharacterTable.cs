namespace GameMain
{
    public class CharacterTable : SOTableList<CharacterProp>
    {
        public override int StartId => IdUtility.CharacterStartId();
    }
}