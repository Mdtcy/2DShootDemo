using System.Collections.Generic;
using GameFramework.ObjectPool;
using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.Assertions;
using UnityGameFramework.Runtime;

namespace GameMain
{
    public class FeedBackComponent : GameFrameworkComponent
    {
        // // 默认持续时间
        // [SerializeField]
        // private float _defaultDuration = 3;

        private IObjectPool<FeedBackObject> _feedBackPool = null;

        // 使用中的特效
        private readonly List<FeedBackObject> _usingFeedBacks = new();

        public void Init()
        {
            _feedBackPool = GameEntry.ObjectPool.CreateSingleSpawnObjectPool<FeedBackObject>("FeedBack", 100);
        }

        private void LateUpdate()
        {
            // 倒序遍历，防止遍历过程中删除元素导致的异常
            for (var index = _usingFeedBacks.Count - 1; index >= 0; index--)
            {
                var feedBackObject = _usingFeedBacks[index];
                var mmfPlayer = feedBackObject.Target as MMF_Player;
                
                #region 跟随逻辑

                var followTarget = mmfPlayer.GetComponent<FollowTarget>();

                // 如果特效存在跟随目标，但是跟随目标已经被销毁，那么就回收特效
                if (followTarget != null &&
                    followTarget.enabled &&
                    !followTarget.TargetExist)
                {
                    UnSpawn(feedBackObject);
                }

                #endregion

                // 如果特效播放完毕，那么就回收特效
                if (feedBackObject.HasTimeLimit)
                {
                    feedBackObject.RemainTime -= Time.deltaTime;
                    if (feedBackObject.RemainTime <= 0)
                    {
                        UnSpawn(feedBackObject);
                    }
                }
            }
        }
        

        // 设置跟随目标
        private void SetFollowTarget(Transform followTarget, MMF_Player mmfPlayer, Vector3 offset)
        {
            if (followTarget != null)
            {
                var followTargetComponent = mmfPlayer.GetOrAddComponent<FollowTarget>();
                followTargetComponent.SetTarget(followTarget, offset);
                followTargetComponent.enabled = true;
                mmfPlayer.transform.localPosition = Vector3.zero;
            }
            else
            {
                // 如果没有跟随目标，但是又有跟随组件，那么禁用跟随组件
                var followTargetComponent = mmfPlayer.GetComponent<FollowTarget>();
                if (followTargetComponent)
                {
                    followTargetComponent.enabled = false;
                }
            }
        }

        // 回收特效
        private void UnSpawn(FeedBackObject feedBackObject)
        {
            _feedBackPool.Unspawn(feedBackObject);
            _usingFeedBacks.Remove(feedBackObject);
        }

        /// <summary>
        /// 播放特效
        /// </summary>
        /// <param name="isFollow"></param>
        /// <param name="feedbacks"></param>
        /// <param name="playFeedBackData"></param>
        public void Play(bool isFollow, GameObject feedbacks, PlayFeedBackData playFeedBackData)
        {
            var mmfPlayer = FindMMfPlayer(feedbacks);
            if (mmfPlayer == null)
            {
                Log.Error("【FeedBacksSystem】Play Feedbacks:" + feedbacks.name + " is null!");
                return;
            }

            PlayInternal(isFollow, mmfPlayer, playFeedBackData);
        }

        /// <summary>
        /// 播放特效
        /// </summary>
        /// <param name="isFollow"></param>
        /// <param name="feedbacks"></param>
        /// <param name="playFeedBackData"></param>
        /// <returns></returns>
        private MMF_Player PlayInternal(bool isFollow, MMF_Player feedbacks, PlayFeedBackData playFeedBackData)
        {
            var feedBackObject = isFollow ?
                New(feedbacks, playFeedBackData.Offset, playFeedBackData.FollowTarget) :
                New(feedbacks, playFeedBackData.Offset);
            var mmfPlayer = feedBackObject.Target as MMF_Player;
            mmfPlayer.PlayFeedbacks();
            _usingFeedBacks.Add(feedBackObject);
            
            playFeedBackData.ReleaseToPool();
            
            return mmfPlayer;
        }

        /// <summary>
        /// 停止特效 目标身上对应绑点的feedbacks特效
        /// </summary>
        /// <param name="feedbacks"></param>
        /// <param name="target"></param>
        /// <param name="key"></param>
        public void Stop(GameObject feedbacks, Transform target, string key)
        {
            var unitBindManager = target.GetComponent<UnitBindManager>();
            Assert.IsNotNull(unitBindManager, "【FeedBacksSystem】Stop Feedbacks:" + feedbacks.name + " UnitBindManager is null!");
            var unitBindPoint = unitBindManager.GetBindPointByKey(key);
            Assert.IsNotNull(unitBindPoint, "【FeedBacksSystem】Stop Feedbacks:" + feedbacks.name + " UnitBindPoint is null!");
            foreach (var mmfPlayer in unitBindPoint.GetComponentsInChildren<MMF_Player>())
            {
                if (mmfPlayer.name == feedbacks.name)
                {
                    mmfPlayer.StopFeedbacks();
                }
            }
            Log.Error("【FeedBacksSystem】Stop Feedbacks:" + feedbacks.name + " is null!");
        }

        public FeedBackObject New(MMF_Player pfbFeedbacks, Vector3 offset, Transform followTarget = null)
        {
            MMF_Player mmfPlayer;
            FeedBackObject feedBackObject = _feedBackPool.Spawn(pfbFeedbacks.name);

            if (feedBackObject != null)
            {
                mmfPlayer = (MMF_Player) feedBackObject.Target;
            }
            else
            {
                mmfPlayer = Instantiate(pfbFeedbacks).GetComponent<MMF_Player>();
                mmfPlayer.gameObject.name = pfbFeedbacks.gameObject.name;
                // 第一次创建时初始化
                mmfPlayer.Initialization();
                feedBackObject = FeedBackObject.Create(pfbFeedbacks.gameObject.name, mmfPlayer);
                _feedBackPool.Register(feedBackObject, true);
            }

            // 如果FeedBacks中有FeedBackDuration组件，那么使用该组件的时间，否则使用MMF_Player的总时间
            var durationComponent = mmfPlayer.GetComponent<FeedBackDuration>();
            Assert.IsNotNull(durationComponent, "【FeedBacksSystem】" + mmfPlayer.name + " FeedBackDuration is null!");
            
            feedBackObject.HasTimeLimit = durationComponent.HasTimeLimit;
            feedBackObject.RemainTime = durationComponent.Duration;
            // if (durationComponent != null)
            // {
            //     feedBackObject.HasTimeLimit = durationComponent.HasTimeLimit;
            //     feedBackObject.RemainTime = durationComponent.Duration;
            // }
            // else
            // {
            //     feedBackObject.HasTimeLimit = true;
            //     feedBackObject.RemainTime = mmfPlayer.TotalDuration;
            // }

            // 设置跟随目标
            SetFollowTarget(followTarget, mmfPlayer, offset);

            return feedBackObject;
        }
        
        private MMF_Player FindMMfPlayer(GameObject go)
        {
            if (go == null)
            {
                Log.Error("【FeedBacksSystem】feedbacks is null!");
                return null;
            }
            else
            {
                var mmfPlayer = go.GetComponent<MMF_Player>();
                if (mmfPlayer == null)
                {
                    Log.Error(go.name + "【FeedBacksSystem】not found the MMF_Player!");
                }

                return mmfPlayer;
            }
        }
    }
}