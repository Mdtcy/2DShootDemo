using System;
using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.Assertions;

namespace GameMain
{
    public class FeedBackComponent : MonoBehaviour
    {
        [Serializable]
        public class FeedBackDictionary:SerializableDictionary<string, MMF_Player>
        {
        }
        
        public FeedBackDictionary FeedBackMap = new FeedBackDictionary();

        public void Play(string name)
        {
            Assert.IsTrue(FeedBackMap.ContainsKey(name), $"[FeedBackComponent] 没有找到FeedBack: {name}");
            FeedBackMap[name].PlayFeedbacks();
        }
    }
}