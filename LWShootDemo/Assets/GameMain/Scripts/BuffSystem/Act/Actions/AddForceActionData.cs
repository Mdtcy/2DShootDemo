using LWShootDemo.BuffSystem.Event;
using LWShootDemo.BuffSystem.Events;
using UnityEngine;

namespace BuffSystem.Act.Actions
{
    public class AddForceActionData : ActionData<OnHitArgs, AddForceAction>
    {
        public float Intensity;
    }
}