using GameFramework;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [LabelText("日志辅助器")]
    [SerializeReference]
    private GameFrameworkLog.ILogHelper _logHelper;
    
    public override void InstallBindings()
    {
        // Log
        GameFrameworkLog.SetLogHelper(_logHelper);
    }
}
