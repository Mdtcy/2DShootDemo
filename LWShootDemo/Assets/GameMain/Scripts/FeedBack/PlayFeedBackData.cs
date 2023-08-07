using GameFramework;
using UnityEngine;

namespace GameMain
{
    public class PlayFeedBackData : IReference
    {
        public Transform Target;

        public string Key;
        
        public Vector3 Offset;
        
        public Transform FollowTarget;
        
        public void Clear()
        {
        }
        
        public static PlayFeedBackData Create()
        {
            PlayFeedBackData data = ReferencePool.Acquire<PlayFeedBackData>();
            return data;
        }
        
        public void ReleaseToPool()
        {
            ReferencePool.Release(this);
        }
    }
}