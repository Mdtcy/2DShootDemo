using GameFramework;
using UnityEngine;

namespace GameMain
{
    public class PlayAtPosFeedBackData : IReference
    {
        public Vector3 Pos;
        
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
            ReferencePool.Release(this);
        }
    }
}