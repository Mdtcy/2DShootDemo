using System;
using LWShootDemo.BuffSystem.Act;
using UnityEngine.Assertions;

namespace LWShootDemo.BuffSystem.Event
{
    public abstract class ProjectileEvent<TArgs> : ProjectileEvent where TArgs : BaseProjectileEventActArgs
    {
        public override Type ExpectedArgumentType => typeof(TArgs);

        public void Trigger(TArgs args)
        {
            Assert.IsNotNull(args);
            Assert.AreEqual(args.GetType(), ExpectedArgumentType, $"传入参数和事件参数不匹配 {args.GetType()} {ExpectedArgumentType}");
            // 获取一个ActionHandler

            foreach (var data in ActionsData)                                
            {
                if (data.State == ActionState.Disable)
                {
                    continue;
                }

                Assert.IsTrue(ExpectedArgumentType == data.ExpectedArgumentType || ExpectedArgumentType.IsSubclassOf(data.ExpectedArgumentType), $"ActionData的参数类型不对 {data.ExpectedArgumentType}");
                var action = data.CreateAction();
                action.Execute(args);
            }
        }
    }

}