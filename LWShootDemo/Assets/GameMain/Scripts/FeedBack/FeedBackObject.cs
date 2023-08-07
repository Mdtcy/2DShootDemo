using GameFramework;
using GameFramework.ObjectPool;
using MoreMountains.Feedbacks;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameMain
{
    public class FeedBackObject : ObjectBase
    {
        [LabelText("是否有时间限制")]
        public bool HasTimeLimit;
        
        [ShowIf("HasTimeLimit")]
        [LabelText("剩余时间")]
        public float RemainTime;
        
        private static Scene _poolScene;
        private static Scene PoolScene
        {
            get
            {
                if (!_poolScene.isLoaded)
                {
                    _poolScene = SceneManager.CreateScene("FeedBackPool");
                }
        
                return _poolScene;
            }
        }

        protected override void Release(bool isShutdown)
        {
            MMF_Player mmfPlayer = (MMF_Player)Target;
            if (mmfPlayer == null)
            {
                return;
            }
            
            Object.Destroy(mmfPlayer.gameObject);
        }

        public static FeedBackObject Create(string pfbName, MMF_Player target)
        {
            FeedBackObject feedBackObject = ReferencePool.Acquire<FeedBackObject>();
            feedBackObject.Initialize(pfbName, target);
            return feedBackObject;
        }

        protected override void OnSpawn()
        {
            base.OnSpawn();
            MMF_Player mmfPlayer = (MMF_Player)Target;
            SceneManager.MoveGameObjectToScene(mmfPlayer.gameObject, PoolScene);
            mmfPlayer.gameObject.SetActive(true);
        }

        protected override void OnUnspawn()
        {
            base.OnUnspawn();
            MMF_Player mmfPlayer = (MMF_Player)Target;
            HasTimeLimit = false;
            RemainTime = float.MaxValue;
            mmfPlayer.StopFeedbacks();
            mmfPlayer.gameObject.SetActive(false);
        }
    }
}