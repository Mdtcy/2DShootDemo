using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Assertions;
using UnityGameFramework.Runtime;

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
    }
}