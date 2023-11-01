using GameFramework;
using GameFramework.Event;
using UnityGameFramework.Runtime;
using System;
using System.Collections.Generic;

namespace GameMain.Extensions
{
    public class EntityLoader : IReference
    {
        private Dictionary<int, Action<Entity>> _dicCallback;
        private Dictionary<int, Entity> _dicSerial2Entity;

        private List<int> _tempList;

        public object Owner
        {
            get;
            private set;
        }

        public EntityLoader()
        {
            _dicSerial2Entity = new Dictionary<int, Entity>();
            _dicCallback = new Dictionary<int, Action<Entity>>();
            _tempList = new List<int>();
            Owner = null;
        }
        
        public int ShowEntity(Type entityLogicType, EntityDataBase entityData, Action<Entity> onShowSuccess = null, int priority = Constant.AssetPriority.DefaultAsset)
        {
            int serialId = GameEntry.Entity.GenerateSerialId();
            _dicCallback.Add(serialId, onShowSuccess);
            GameEntry.Entity.ShowEntity(serialId, entityLogicType, priority, entityData);
            return serialId;
        }

        public int ShowEntity<T>(EntityDataBase entityData, Action<Entity> onShowSuccess = null, int priority = Constant.AssetPriority.DefaultAsset) where T : EntityLogic
        {
            return ShowEntity(typeof(T), entityData, onShowSuccess, priority);
        }

        public bool HasEntity(int serialId)
        {
            return GetEntity(serialId) != null;
        }

        public Entity GetEntity(int serialId)
        {
            if (_dicSerial2Entity.TryGetValue(serialId, out var entity))
            {
                return entity;
            }

            return null;
        }

        public IEnumerable<Entity> GetAllEntities()
        {
            return _dicSerial2Entity.Values;
        }

        public void HideEntity(int serialId)
        {
            Entity entity = null;
            if (!_dicSerial2Entity.TryGetValue(serialId, out entity))
            {
                Log.Error("Can find entity('serial id:{0}') ", serialId);
            }

            _dicSerial2Entity.Remove(serialId);
            _dicCallback.Remove(serialId);

            Entity[] entities = GameEntry.Entity.GetChildEntities(entity);
            if (entities != null)
            {
                foreach (var item in entities)
                {
                    //若Child Entity由这个Loader对象托管，则由此Loader释放
                    if (_dicSerial2Entity.ContainsKey(item.Id))
                    {
                        HideEntity(item);
                    }
                    else//若Child Entity不由这个Loader对象托管，则从Parent Entity脱离
                        GameEntry.Entity.DetachEntity(item);
                }
            }

            GameEntry.Entity.HideEntity(entity);
        }

        public void HideEntity(Entity entity)
        {
            if (entity == null)
                return;

            HideEntity(entity.Id);
        }

        public void HideAllEntity()
        {
            _tempList.Clear();

            foreach (var entity in _dicSerial2Entity.Values)
            {
                Entity parentEntity = GameEntry.Entity.GetParentEntity(entity);
                //有ParentEntity
                if (parentEntity != null)
                {
                    //若Parent Entity由这个Loader对象托管，则把这个Child Entity从数据中移除，在隐藏Parent Entity，GF内部会处理Child Entity
                    if (_dicSerial2Entity.ContainsKey(parentEntity.Id))
                    {
                        _dicSerial2Entity.Remove(entity.Id);
                        _dicCallback.Remove(entity.Id);
                    }
                    //若Parent Entity不由这个Loader对象托管，则从Parent Entity脱离
                    else
                    {
                        GameEntry.Entity.DetachEntity(entity);
                    }
                }
            }

            foreach (var serialId in _dicSerial2Entity.Keys)
            {
                _tempList.Add(serialId);
            }

            foreach (var serialId in _tempList)
            {
                HideEntity(serialId);
            }

            _dicSerial2Entity.Clear();
            _dicCallback.Clear();
        }

        private void OnShowEntitySuccess(object sender, GameEventArgs e)
        {
            ShowEntitySuccessEventArgs ne = (ShowEntitySuccessEventArgs)e;
            if (ne == null)
            {
                return;
            }

            Action<Entity> callback = null;
            if (!_dicCallback.TryGetValue(ne.Entity.Id, out callback))
            {
                return;
            }

            _dicSerial2Entity.Add(ne.Entity.Id, ne.Entity);
            callback?.Invoke(ne.Entity);
        }

        private void OnShowEntityFail(object sender, GameEventArgs e)
        {
            ShowEntityFailureEventArgs ne = (ShowEntityFailureEventArgs)e;
            if (ne == null)
            {
                return;
            }

            if (_dicCallback.ContainsKey(ne.EntityId))
            {
                _dicCallback.Remove(ne.EntityId);
                Log.Warning("{0} Show entity failure with error message '{1}'.", Owner.ToString(), ne.ErrorMessage);
            }
        }

        public static EntityLoader Create(object owner)
        {
            EntityLoader entityLoader = ReferencePool.Acquire<EntityLoader>();
            entityLoader.Owner = owner;
            GameEntry.Event.Subscribe(ShowEntitySuccessEventArgs.EventId, entityLoader.OnShowEntitySuccess);
            GameEntry.Event.Subscribe(ShowEntityFailureEventArgs.EventId, entityLoader.OnShowEntityFail);

            return entityLoader;
        }
        
        public void ReleaseToPool()
        {
            ReferencePool.Release(this);
        }

        public void Clear()
        {
            Owner = null;
            _dicSerial2Entity.Clear();
            _dicCallback.Clear();
            GameEntry.Event.Unsubscribe(ShowEntitySuccessEventArgs.EventId, OnShowEntitySuccess);
            GameEntry.Event.Unsubscribe(ShowEntityFailureEventArgs.EventId, OnShowEntityFail);
        }
    }
}

