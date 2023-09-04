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
            GameEntry.Entity.ShowAoe(new AoeData(id,
                aoeProp.EntityProp.ID,
                aoeProp,
                caster,
                range,
                pParam)
            {
                Position = position,
                Rotation = rotation,
            });

            return id;
        }
    }
}