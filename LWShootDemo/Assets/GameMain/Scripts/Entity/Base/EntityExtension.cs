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
        private static int s_SerialId = 0;
        
        public static EntityLogicBase GetGameEntity(this EntityComponent entityComponent, int entityId)
        {
            Entity entity = entityComponent.GetEntity(entityId);
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

        public static void AttachEntity(this EntityComponent entityComponent,
            EntityLogicBase entity,
            int ownerId,
            string parentTransformPath = null,
            object userData = null)
        {
            entityComponent.AttachEntity(entity.Entity, ownerId, parentTransformPath, userData);
        }

        public static void ShowEntity(this EntityComponent entityComponent,
            int id,
            Type logicType,
            int priority,
            EntityDataBase data)
        {
            if (data == null)
            {
                Log.Warning("Data is invalid.");
                return;
            } 

            entityComponent.ShowEntity(id, logicType, data.EntityProp.AssetPath, data.EntityProp.GroupName,
                priority, data);
        }

        public static void ShowEntity<T>(this EntityComponent entityComponent,
            int id,
            EntityDataBase data,
            int priority = Constant.AssetPriority.DefaultAsset) where T : EntityLogicBase
        {
            ShowEntity(entityComponent, id, typeof(T), priority, data);
        }
        
        public static int GenerateSerialId(this EntityComponent entityComponent)
        {
            return --s_SerialId;
        }
    }
}