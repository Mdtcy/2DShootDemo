using System.Collections.Generic;
using GameFramework;
using Sirenix.OdinInspector;
using UnityGameFramework.Runtime;

namespace GameMain
{
    public class TableConfigComponent : GameFrameworkComponent
    {
        [ReadOnly]
        [ShowInInspector]
        private Dictionary<string, SOTableList> _tableConfigDict = new();
        
        public void Register(string name, SOTableList soTableList)
        {
            Log.Debug($"【TableConfigComponent】Register Table: {name}");
            _tableConfigDict.Add(name, soTableList);
        }
        
        public T Get<T>() where T : SOTableList
        {
            string name = typeof(T).Name;
            if (_tableConfigDict.TryGetValue(name, out var value))
            {	
                return value as T;
            }
            else
            {
                throw new GameFrameworkException($"【TableConfigComponent】Not found {name}");
            }
        }
    }
}