using System;
using GameFramework;
using GameFramework.Event;
using LWShootDemo.BuffSystem.Buffs;
using LWShootDemo.BuffSystem.Event;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Assertions;
using UnityGameFramework.Runtime;
using Zenject;

namespace DefaultNamespace
{

    public class Test : MonoBehaviour
    {
        public float a;
        public float b;

        public string c;

        [Button]
        public void TestA()
        {
            Assert.AreEqual(c, "12", "c!=12");
            Assert.AreEqual(a,b,"a!=b");
            Assert.IsTrue(a==b,"a!=b");
        }

        [Button]
        public void TestLog()
        {
            Log.Debug("Debug");
            Log.Info("Info");
            Log.Warning("Warning");
            Log.Error("Error");
            Log.Fatal("Fatal");
        }

        [Inject]
        private IEventManager _eventManager;
        
        [Button]
        public void TestSurcribe()
        {
            _eventManager.Subscribe(TestEvent.EventId, OnTestEvent);
        }
        
        [Button]
        public void TestUnSurcribe()
        {
            _eventManager.Unsubscribe(TestEvent.EventId, OnTestEvent);
        }
        
        [Button]
        public void TestFireTestEvent()
        {
            _eventManager.Fire(this, TestEvent.Create());
        }

        private void OnTestEvent(object sender, GameEventArgs e)
        {
            Log.Info("OnTestEvent");
        }

        public class TestEvent:GameEventArgs
        {
            public static TestEvent Create()
            {
                return ReferencePool.Acquire<TestEvent>();
            }

            public override void Clear()
            {
                Log.Info("OnTEstEventClear");
            }

            /// <summary>
            /// 显示物体成功事件编号。
            /// </summary>
            public static readonly int EventId = typeof(TestEvent).GetHashCode();

            public override int Id => EventId;
            
        }

        public class AClass : IReference
        {
            public void Clear()
            {
                Log.Info("Clear");
            }
        }

        private AClass _aClass;
        [Button]
        public void TestAcquireReference()
        {
            _aClass = ReferencePool.Acquire<AClass>();
        }

        [Button]
        public void TestReleaseReference()
        {
            ReferencePool.Release(_aClass);
        }

        public BuffData BuffData;

        [Button]
        public void TestHitEventBuff()
        {
            var buff = new Buff(BuffData);
            buff.OnHitEvent(new HitArgs());
        }

        public int damage;
        [Button]
        public void TestTestEventBuff()
        {
            var buff = new Buff(BuffData);
            var arg = new TestArgs();
            arg.Damage = damage;
            buff.OnTestEvent(arg);
        }

        [Button]
        public void TestTick()
        {
            var buff = new Buff(BuffData);
            var buffComponent = gameObject.AddComponent<BuffComponent>();
            buffComponent.Buffs.Add(buff);
        }
    }
}