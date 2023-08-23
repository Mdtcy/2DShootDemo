using GameFramework;
using UnityEngine;

namespace GameMain
{
    public class PlayAtPosFeedBackData : IReference
    {
        public Vector3 Pos;
        public float Scale;
        
        public void Clear()
        {
        }
        
        public static PlayAtPosFeedBackData Create()
        {
            PlayAtPosFeedBackData data = ReferencePool.Acquire<PlayAtPosFeedBackData>();
            return data;
        }
        
        public void ReleaseToPool()
        {
            Pos = Vector3.zero;
            Scale = 0;
            ReferencePool.Release(this);
        }
    }
}