using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace GameMain
{
    public class AoeComponent : GameFrameworkComponent
    {
        public int CreateAoe(AoeProp aoeProp, 
            Vector3 position, 
            Quaternion rotation, 
            Character caster, float range = 1, Dictionary<string, object> pParam = null)
        {
            int id = GameEntry.Entity.GenerateSerialId();
            var aoeDate = new AoeData(aoeProp.EntityProp,
                aoeProp,
                caster,
                range,
                pParam)
            {
                Position = position,
                Rotation = rotation,
            };
            GameEntry.Entity.ShowEntity<AoeState>(id, aoeDate);

            return id;
        }
    }
}