using System;
using UnityEngine;

namespace LWShootDemo.BuffSystem.Event.Test
{
    [Serializable]
    public class DamageActionData : ActionData
    {
        public int Damage;

        public override IAction CreateAction()
        {
            return new DamageAction(this);
        }

        public override Type ActionArgType => typeof(OnProjectileStartArgs);
    }
}