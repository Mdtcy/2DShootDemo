using UnityEngine;
using Zenject;

namespace GameFramework.ObjectPool
{
    /// <summary>
    /// 对象池组件 todo 去除功能 仅用于显示调试信息
    /// </summary>
    [DisallowMultipleComponent]
    [AddComponentMenu("Game Framework/Object Pool")]
    public class ObjectPoolComponent : MonoBehaviour
    {
        [Inject]
        private IObjectPoolManager _objectPoolManager;
        public object Count => _objectPoolManager.Count;

        public ObjectPoolBase[] GetAllObjectPools(bool sort)
        {
            return _objectPoolManager.GetAllObjectPools(sort);
        }
    }
}