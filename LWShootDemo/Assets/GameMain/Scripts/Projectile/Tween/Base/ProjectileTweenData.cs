using UnityEngine;

namespace GameMain
{
    [System.Serializable]
    public abstract class ProjectileTweenData
    {
        public abstract HomingProjectileTween1 CreateTween();
    }
}