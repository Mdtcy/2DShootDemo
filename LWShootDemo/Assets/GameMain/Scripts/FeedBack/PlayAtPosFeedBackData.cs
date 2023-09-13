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
        
        public static PlayAtPosFeedBackData Create(Vector3 pos, float scale)
        {
            PlayAtPosFeedBackData data = ReferencePool.Acquire<PlayAtPosFeedBackData>();
            data.Pos = pos;
            data.Scale = scale;
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