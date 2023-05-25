using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LWShootDemo.BuffSystem.Event.Test
{
    public class DamageAction : IAction
    {
        private readonly DamageActionData _data;

        public DamageAction(DamageActionData data)
        {
            _data = data;
        }

        public override void Execute(EventActArgsBase args)
        {
            Debug.Log($"Damage: {_data.Damage}");
        }
    }
}