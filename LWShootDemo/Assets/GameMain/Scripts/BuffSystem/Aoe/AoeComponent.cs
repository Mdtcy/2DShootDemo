using UnityEngine;
using UnityGameFramework.Runtime;

namespace GameMain
{
    public class AoeComponent : GameFrameworkComponent
    {
        public void CreateAoe(AoeProp aoeProp, Vector3 position, Quaternion rotation, Character caster, Transform followTarget = null)
        {
            int id = GameEntry.Entity.GenerateSerialId();
            GameEntry.Entity.ShowAoe(new AoeData(id,
                aoeProp.EntityProp.ID,
                aoeProp,
                caster)
            {
                Position = position,
                Rotation = rotation,
                Scale = Vector3.one,
            });
        }
    }
}