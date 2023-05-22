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
        // 初始化顺序
        InitExecutionOrder();
        
        // Log
        GameFrameworkLog.SetLogHelper(_logHelper);
    }
    
    void InitExecutionOrder()
    {
        // In many cases you don't need to worry about execution order,
        // however sometimes it can be important
        // If for example we wanted to ensure that AsteroidManager.Initialize
        // always gets called before GameController.Initialize (and similarly for Tick)
        // Then we could do the following:
        // Container.BindExecutionOrder<AsteroidManager>(-10);
        // Container.BindExecutionOrder<GameController>(-20);

        // Note that they will be disposed of in the reverse order given here
    }
}
