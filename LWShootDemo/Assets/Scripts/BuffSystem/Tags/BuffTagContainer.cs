using LWShootDemo.BuffSystem.Tags;

[System.Serializable]
public class BuffTagContainer : TagContainer<BuffTag>
{
    protected override string TagsPath => BuffSystemSetting.Inst().BuffTagFolderPath;
}
