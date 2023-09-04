using LWShootDemo.Common;
using UnityEngine;

[CreateAssetMenu(fileName = "BuffSystemSettings", menuName = "BuffSystem/BuffSystemSettings")]
public class BuffSystemSetting : ScriptableObject
{
    [SerializeField] 
    private string _buffTagFolderPath = "Assets/Configs/BuffTags";
    public string BuffTagFolderPath => _buffTagFolderPath;
    
    public static BuffSystemSetting Inst()
    {
        return AssetDataBaseUtility.FindAssetByType<BuffSystemSetting>();
    }
}