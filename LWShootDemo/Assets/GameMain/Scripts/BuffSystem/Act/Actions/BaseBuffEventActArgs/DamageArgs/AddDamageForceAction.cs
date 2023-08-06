using GameMain;
using LWShootDemo.BuffSystem.Event;
using LWShootDemo.BuffSystem.Events;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace BuffSystem.Act.Actions
{
    [LabelText("添加伤害力(AddDamageForceAction)")]
    public class AddDamageForceAction : ActionBase<DamageArgs, AddDamageForceActionData>
    {
        protected override void ExecuteInternal(DamageArgs args)
        {
            Character target = null;
            switch (Data.TargetType)
            {
                case AddDamageForceActionData.Target.Attacker:
                    target = args.DamageInfo.attacker;
                    break;
                case AddDamageForceActionData.Target.Defender:
                    target = args.DamageInfo.defender;
                    break;
                default:
                    Log.Error("TargetType is invalid.");
                    break;
            }
            Vector3 dir = Vector3.zero;
            switch (Data.DirectionType)
            {
                case AddDamageForceActionData.Direction.DamageDirection:
                    dir = args.DamageInfo.Direction;
                    break;
                case AddDamageForceActionData.Direction.InverseDamageDirection:
                    dir = -args.DamageInfo.Direction;
                    break;
                default:
                    Log.Error("DirectionType is invalid.");
                    break;
            }
            
            var motionClip = new MotionClip_Force(true, 0.2f, dir, null, Data.Intensity);
            
            target.PlayMotionClip(motionClip);
        }
    }
}