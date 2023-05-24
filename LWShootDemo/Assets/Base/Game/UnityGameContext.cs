using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameFramework.Game
{
    public class UnityGameContext : MonoBehaviour
    {
        private GameFrameworkLinkedList<GameFrameworkModule> _gameFrameworkModules = new();

        public void AddModule(GameFrameworkModule module)
        {
            LinkedListNode<GameFrameworkModule> current = _gameFrameworkModules.First;
            while (current != null)
            {
                if (module.Priority > current.Value.Priority)
                {
                    break;
                }

                current = current.Next;
            }

            if (current != null)
            {
                _gameFrameworkModules.AddBefore(current, module);
            }
            else
            {
                _gameFrameworkModules.AddLast(module);
            }
        }

        private void Update()
        {
            foreach (GameFrameworkModule module in _gameFrameworkModules)
            {
                module.Update(Time.deltaTime, Time.unscaledDeltaTime);
            }
        }

        private void OnDestroy()
        {
            for (LinkedListNode<GameFrameworkModule> current = _gameFrameworkModules.Last; current != null; current = current.Previous)
            {
                current.Value.Shutdown();
            }

            _gameFrameworkModules.Clear();
            ReferencePool.ClearAll();
            Utility.Marshal.FreeCachedHGlobal();
            GameFrameworkLog.SetLogHelper(null);
        }
    }
}