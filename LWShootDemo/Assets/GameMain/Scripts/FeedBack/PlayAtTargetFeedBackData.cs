using GameFramework;
using UnityEngine;

namespace GameMain
{
    public class PlayAtTargetFeedBackData : IReference
    {
        public Transform Target;

        public string Key;
        
        public Vector3 Offset;
        
        public bool IsFollow;

        public void Clear()
        {
        }
        
        public static PlayAtTargetFeedBackData Create()
        {
            PlayAtTargetFeedBackData data = ReferencePool.Acquire<PlayAtTargetFeedBackData>();
            return data;
        }
        
        public void ReleaseToPool()
        {
            ReferencePool.Release(this);
        }
    }
}