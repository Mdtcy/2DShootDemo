using UnityEngine;

namespace GameMain
{
    public abstract class SOTableList : ScriptableObject
    {

#if UNITY_EDITOR
        public abstract bool CheckValid(ref string message);
#endif
    }
}