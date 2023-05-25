using System;
using UnityEngine;

namespace LWShootDemo.BuffSystem.Event.Test
{
    [Serializable]
    public class DamageActionData : ActionData
    {
        public int Damage;

        public override Type ArgType { get; }

        public override IAction CreateAction()
        {
            return new DamageAction(this);
        }
    }
}