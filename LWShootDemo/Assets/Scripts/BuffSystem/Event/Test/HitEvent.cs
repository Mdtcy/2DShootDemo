using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace LWShootDemo.BuffSystem.Event
{
    public class HitArgs : BaseEventActArgs
    {
        public GameObject Target;
        public float Damage;
    }
    
    [LabelText("HitEvent")]
    public class HitEvent : BuffEvent<HitArgs>
    {
        public static readonly int EventId = typeof(HitEvent).GetHashCode();
        public override int ID => EventId;
    }
    
    public class TestArgs : BaseEventActArgs
    {
        public GameObject Target;
        public float Damage;
    }
    
    [LabelText("TestEvent")]
    public class TestEvent:BuffEvent<TestArgs>
    {
        public static readonly int EventId = typeof(TestEvent).GetHashCode();
        public override int ID => EventId;
    }
    
    // [LabelText("调试(DebugActionData)")]
    public class DebugActionData : ActionData<BaseEventActArgs, DebugAction>
    {
        public string info;
    }

    [LabelText("调试信息(DebugAction)")]
    public class DebugAction : Action<BaseEventActArgs, DebugActionData>
    {
        protected override void ExecuteInternal(BaseEventActArgs args)
        {
            Log.Info("通用DebugAction" + Data.info + args);
        }
    }

    public class TestActionData1 : ActionData<TestArgs, TestAction1>
    {
        public string te;
        public int tt;
    }

    public class TestAction1 : Action<TestArgs, TestActionData1>
    {
        protected override void ExecuteInternal(TestArgs args)
        {
            Log.Info("TestAction1 {0}{1} {2}", Data.te, Data.tt, args.Damage);
        }
    }

    public class HitActionData1 : ActionData<HitArgs, HitAction1>
    {
    }
    
    public class HitActionData2 : ActionData<HitArgs, HitAction2>
    {
    }

    [LabelText("击打1")]
    public class HitAction1 : Action<HitArgs, HitActionData1>
    {
        protected override void ExecuteInternal(HitArgs args)
        {
            Debug.Log("HitAction1");
        }
    }

    [LabelText("击打2")]
    public class HitAction2 : Action<HitArgs, HitActionData2>
    {
        protected override void ExecuteInternal(HitArgs args)
        {
            Debug.Log("HitAction2");
        }
    }
}