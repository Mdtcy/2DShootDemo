using GameFramework.DataTable;
using System;
using UnityGameFramework.Runtime;

namespace GameMain
{
    public static class EntityExtension
    {
        // 关于 EntityId 的约定：
        // 0 为无效
        // 正值用于和服务器通信的实体（如玩家角色、NPC、怪等，服务器只产生正值）
        // 负值用于本地生成的临时实体（如特效、FakeObject等）
        private static int _serialId = 0;

        public static EntityLogicBase GetGameEntity(this EntityComponent entityComponent, int entityId)
        {
            UnityGameFramework.Runtime.Entity entity = entityComponent.GetEntity(entityId);
            if (entity == null)
            {
                return null;
            }

            return (EntityLogicBase) entity.Logic;
        }

        public static void HideEntity(this EntityComponent entityComponent, EntityLogicBase entity)
        {
            entityComponent.HideEntity(entity.Entity);
        }

        public static void AttachEntity(this EntityComponent entityComponent, EntityLogicBase entity, int ownerId, string parentTransformPath = null, object userData = null)
        {
            entityComponent.AttachEntity(entity.Entity, ownerId, parentTransformPath, userData);
        }

        public static void ShowEntity(this EntityComponent entityComponent, Type logicType, int priority, EntityDataBase data)
        {
            if (data == null)
            {
                Log.Warning("Data is invalid.");
                return;
            }

            var entityTable = GameEntry.TableConfig.Get<EntityTable>();
            var entityProp = entityTable.Get(data.TypeId);
            // IDataTable<DREntity> dtEntity = GameEntry.DataTable.GetDataTable<DREntity>();
            // DREntity drEntity = dtEntity.GetDataRow(data.TypeId);
            if (entityProp == null)
            {
                Log.Warning("Can not load entity id '{0}' from data table.", data.TypeId.ToString());
                return;
            }

            entityComponent.ShowEntity(data.Id, logicType, AssetUtility.GetEntityAsset(entityProp.AssetName), entityProp.GroupName, priority, data);
        }

        #region Show Entity Extension
        
        public static void ShowEnemy(this EntityComponent entityComponent, EnemyGhoulData ghoulData)
        {
            entityComponent.ShowEntity(typeof(EnemyGhoul), Constant.AssetPriority.EnemyAsset, ghoulData);
        }
        
        public static void ShowPlayer(this EntityComponent entityComponent, PlayerData data)
        {
            entityComponent.ShowEntity(typeof(Player), Constant.AssetPriority.PlayerAsset, data);
        }
        
        public static void ShowProjectile(this EntityComponent entityComponent, ProjectileData data)
        {
            entityComponent.ShowEntity(typeof(Projectile), Constant.AssetPriority.ProjectileAsset, data);
        }

        public static void ShowAoe(this EntityComponent entityComponent, AoeData data)
        {
            entityComponent.ShowEntity(typeof(AoeState), Constant.AssetPriority.AoeAsset, data);
        }

        public static void ShowFruit(this EntityComponent entityComponent, FruitData data)
        {
            entityComponent.ShowEntity(typeof(Fruit), Constant.AssetPriority.FruitAsset, data);
        }

        #endregion

        public static int GenerateSerialId(this EntityComponent entityComponent)
        {
            return --_serialId;
        }
    }
}
