using System;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace LWShootDemo.BuffSystem.Event
{
    public class HitArgs : BaseEventActArgs
    {
        public GameObject Target;
        public float Damage;
    }
    
    public class HitEvent : BuffEvent
    {
        public static readonly int EventId = typeof(HitEvent).GetHashCode();
        public override int ID => EventId;
        public override Type ExpectedArgumentType => typeof(HitArgs);
    }
    
    public class TestArgs : BaseEventActArgs
    {
        public GameObject Target;
        public float Damage;
    }
    
    public class TestEvent:BuffEvent
    {
        public static readonly int EventId = typeof(TestEvent).GetHashCode();
        public override int ID => EventId;
        public override Type ExpectedArgumentType => typeof(TestArgs);
    }
    
    public class DebugActionData : ActionData<BaseEventActArgs>
    {
        public string info;
        public override IAction CreateAction(BaseEventActArgs args)
        {
            return new DebugAction(this, args);
        }
    }

    public class DebugAction : Action<BaseEventActArgs, DebugActionData>
    {
        public DebugAction(DebugActionData data, BaseEventActArgs args) : base(data, args)
        {
        }

        public override void Execute()
        {
            Log.Info("通用DebugAction" + Data.info);
        }
    }


    public class TestActionData1 : ActionData<TestArgs>
    {
        public string te;
        public int tt;
        public override IAction CreateAction(TestArgs args)
        {
            return new TestAction1(this, args);
        }
    }

    public class TestAction1 : Action<TestArgs, TestActionData1>
    {
        public override void Execute()
        {
            Log.Info("TestAction1 {0}{1} {2}", Data.te, Data.tt, Arg.Damage);
        }

        public TestAction1(TestActionData1 data, TestArgs args) : base(data, args)
        {
        }
    }

    public class HitActionData1 : ActionData<HitArgs>
    {
        public override IAction CreateAction(HitArgs args)
        {
            return new HitAction1(this, args);
        }
    }
    
    public class HitActionData2 : ActionData<HitArgs>
    {
        public override IAction CreateAction(HitArgs args)
        {
            return new HitAction2(this, args);
        }
    }

    public abstract class Action<TArgs, TActData> : IAction where TArgs : IEventActArgs where TActData : ActionData<TArgs>
    {
        protected readonly TArgs Arg;
        
        protected readonly TActData Data;
        public Action(TActData data, TArgs args) 
        {
            Data = data;
            Arg = args;
        }
    }

    public class HitAction1 : Action<HitArgs, HitActionData1>
    {
        public HitAction1(HitActionData1 data, HitArgs args) : base(data, args)
        {
        }

        public override void Execute()
        {
            Debug.Log("HitAction1");
        }
    }

    public class HitAction2 : Action<HitArgs, HitActionData2>
    {
        public HitAction2(HitActionData2 data, HitArgs args) : base(data, args)
        {
        }

        public override void Execute()
        {
            Debug.Log("HitAction2");
        }
    }
}