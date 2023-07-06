using Damages;
using GameFramework;
using GameFramework.Event;
using LWShootDemo.BuffSystem.Buffs;
using LWShootDemo.BuffSystem.Event;
using LWShootDemo.Entities;
using LWShootDemo.Motion;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Assertions;
using UnityGameFramework.Runtime;
using GameEntry = GameMain.GameEntry;

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

        [Button]
        public void TestSurcribe()
        {
            GameEntry.Event.Subscribe(TestEvent.EventId, OnTestEvent);
        }
        
        [Button]
        public void TestUnSurcribe()
        {
            GameEntry.Event.Unsubscribe(TestEvent.EventId, OnTestEvent);
        }
        
        [Button]
        public void TestFireTestEvent()
        {
            GameEntry.Event.Fire(this, TestEvent.Create());
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

        public int duration = 10;

        // [Button]
        // public void TestHitEventBuff()
        // {
        //     var addBuffInfo = new AddBuffInfo(BuffData, null, null, 1, duration);
        //     var buffComponent = gameObject.GetOrAddComponent<BuffComponent>();
        //     buffComponent.AddBuff(addBuffInfo);
        //     var arg = new HitArgs
        //     {
        //         Damage = damage
        //     };
        //     buffComponent.TriggerEvent<HitEvent, HitArgs>(arg);
        // }
        //
        // public int damage;
        // [Button]
        // public void TestTestEventBuff()
        // {
        //     var addBuffInfo = new AddBuffInfo(BuffData, null, null, 1, duration);
        //     var buffComponent = gameObject.GetOrAddComponent<BuffComponent>();
        //     buffComponent.AddBuff(addBuffInfo);
        //     var arg = new TestArgs();
        //     arg.Damage = damage;
        //     buffComponent.TriggerEvent<LWShootDemo.BuffSystem.Event.TestEvent, TestArgs>(arg);
        // }

        [Button]
        public void TestTick()
        {
            // todo 都是null有啥影响
            var addBuffInfo = new AddBuffInfo(BuffData, null, null, 1, duration);
            // var buffComponent = gameObject.GetOrAddComponent<BuffComponent>();
            // player.AddBuff(addBuffInfo);
        }

        // public OldEntity player;

        public Vector3 方向;
        
        public float 强度;

        public float 持续时间;

        public AnimationCurve 动画曲线;

        // [Button]
        // public void AddForce()
        // {
        //     var motion = new MotionClip_Force(true, 持续时间, 方向 * 强度, 动画曲线);
        //     player.PlayMotionClip(motion);
        // }
        //
        // [Button]
        // public void 停止Motion()
        // {
        //     player.StopMotionClip();
        // }
    }
}