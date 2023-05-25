using LWShootDemo.BuffSystem.Event.Test;
using UnityEngine;

namespace LWShootDemo.BuffSystem.Event
{
    public class IActionHandler<T, V> where T : ActionData where V : EventActArgsBase
    {
        public virtual void Execute(T data, V args)
        {
            
        }
    }

    public class DamageActionHandler : IActionHandler<DamageActionData, OnProjectileStartArgs>
    {
        
    }
}