using System;
using GameFramework;
using GameFramework.Event;
using GameFramework.Game;
using GameFramework.ObjectPool;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityGameFramework.Runtime;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField]
    private UnityGameContext _unityGameContext;
    
    [LabelText("日志辅助器")]
    [SerializeReference]
    private GameFrameworkLog.ILogHelper _logHelper;
    
    [TitleGroup("ReferencePool")]
    [LabelText("是否开启强制检查")]
    [SerializeField]
    private ReferenceStrictCheckType _enableStrictCheck = ReferenceStrictCheckType.AlwaysEnable;
    

    public override void InstallBindings()
    {
        // Log
        GameFrameworkLog.SetLogHelper(_logHelper);
        
        // 初始化顺序
        InitExecutionOrder();
        
        // ReferencePool
        InitReferencePool();

        // Event
        Container
            .Bind<IEventManager>()
            .To<EventManager>()
            .AsSingle()
            .OnInstantiated((ctx, obj) =>
            {
                var eventManager = (EventManager) obj;
                eventManager.Priority = 100;
                _unityGameContext.AddModule(eventManager);
                Log.Info("初始化 EventManager 优先级: {0}", eventManager.Priority);
            });
        
        // ObjectPool
        Container
            .Bind<IObjectPoolManager>()
            .To<ObjectPoolManager>()
            .AsSingle()
            .OnInstantiated((ctx, obj) =>
        {
            var objectPoolManager = (ObjectPoolManager) obj;
            objectPoolManager.Priority = 90;
            _unityGameContext.AddModule(objectPoolManager);
            Log.Info("初始化 ObjectPoolManager 优先级: {0}", objectPoolManager.Priority);
        });
    }

    private void InitReferencePool()
    {
        Log.Info("初始化 ReferencePool");
        switch (_enableStrictCheck)
        {
            case ReferenceStrictCheckType.AlwaysEnable:
                ReferencePool.EnableStrictCheck = true;
                break;

            case ReferenceStrictCheckType.OnlyEnableWhenDevelopment:
                ReferencePool.EnableStrictCheck = Debug.isDebugBuild;
                break;

            case ReferenceStrictCheckType.OnlyEnableInEditor:
                ReferencePool.EnableStrictCheck = Application.isEditor;
                break;

            default:
                ReferencePool.EnableStrictCheck = false;
                break;
        }

        if (ReferencePool.EnableStrictCheck)
        {
            Log.Warning("Strict checking is enabled for the Reference Pool. It will drastically affect the performance.");
        }
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
        // todo 需要看看示例里具体是啥样

        // Note that they will be disposed of in the reverse order given here
    }
}
