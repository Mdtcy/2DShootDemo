// using System.Collections.Generic;
// using UnityEngine;
//
// namespace LWShootDemo.BuffSystem.Event.Test
// {
//     public class DamageEvent : BuffEvent
//     {
//         public override void Trigger(EventActArgsBase args)
//         {
//             foreach (var action in Actions)
//             {
//                 action.Execute(args);
//             }
//         }
//
//         protected override IEnumerable<IAction> GetValidActions()
//         {
//             // 返回一个合适的动作列表
//             return new List<IAction>
//             {
//                 new DamageAction(),
//                 new TestAction()
//             };
//         }
//     }
//
// }