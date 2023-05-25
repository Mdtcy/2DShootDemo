using System;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace LWShootDemo.BuffSystem.Event
{
    public class HitArgs : EventActArgsBase
    {
        public GameObject Target;
        public float Damage;
    }
    
    public class HitEvent : BuffEvent
    {
        public static readonly int EventId = typeof(HitEvent).GetHashCode();
        public override int ID => EventId;
        public override Type ArgType => typeof(HitArgs);
    }
    
    public class TestArgs : EventActArgsBase
    {
        public GameObject Target;
        public float Damage;
    }
    
    public class TestEvent:BuffEvent
    {
        public static readonly int EventId = typeof(TestEvent).GetHashCode();
        public override int ID => EventId;
        public override Type ArgType => typeof(TestArgs);
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

    public class TestAction1 : Action<TestArgs>
    {

        public override void Execute()
        {
            var d = (TestActionData1) Tdata;
            Log.Info("TestAction1 {0}{1} {2}", d.te, d.tt, Arg.Damage);
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

    public abstract class Action<TArgs> : IAction where TArgs : EventActArgsBase
    {
        protected TArgs Arg => (TArgs)Args;
        
        
        protected ActionData<TArgs> Tdata;
        public Action(ActionData<TArgs> data, TArgs args) : base(data, args)
        {
            Tdata = data;
        }
    }

    public class HitAction1 : Action<HitArgs>
    {
        public HitAction1(HitActionData1 data, HitArgs args) : base(data, args)
        {
        }

        public override void Execute()
        {
            Debug.Log("HitAction1");
        }
    }

    public class HitAction2 : IAction
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