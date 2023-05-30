
using System.Collections.Generic;
using LWShootDemo.BuffSystem.Buffs;
using LWShootDemo.Entities;
using UnityEngine;

namespace Damages
{
    public interface IDamageManager
    {
        public void DoDamage(Entity attacker,
            Entity target,
            int damage,
            Vector2 damageDirection,
            float criticalRate, 
            List<DamageInfoTag> tags,
            List<AddBuffInfo> addBuffInfos);
    }
}