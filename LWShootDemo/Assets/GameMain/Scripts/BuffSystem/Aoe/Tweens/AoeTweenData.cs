using UnityEngine;

namespace GameMain
{
    [System.Serializable]
    public abstract class AoeTweenData 
    {
        public abstract AoeTween CreateTween();
    }
}