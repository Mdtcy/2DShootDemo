using BuffSystem.Act.Actions;
using LWShootDemo.BuffSystem.Event;
using UnityEngine;

namespace GameMain
{
    public class ProjectileHitDamageActionData : ActionData<OnProjectileHitArgs, ProjectileHitDamageAction>
    {
        public int Damage;
    }
}