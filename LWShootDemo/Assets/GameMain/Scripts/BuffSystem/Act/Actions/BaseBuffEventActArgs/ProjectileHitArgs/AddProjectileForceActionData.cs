using BuffSystem.Act.Actions;
using LWShootDemo.BuffSystem.Event;
using LWShootDemo.BuffSystem.Events;
using UnityEngine;

namespace GameMain
{
    public class AddProjectileForceActionData : ActionData<OnProjectileHitArgs, AddProjectileForceAction>
    {
        public float Duration;
        
        public float Intensity;
    }
}