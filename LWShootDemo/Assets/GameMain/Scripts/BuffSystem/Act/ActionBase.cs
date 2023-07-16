using UnityEngine;

namespace LWShootDemo.BuffSystem.Event
{
    public abstract class ActionBase
    {
        public virtual void Initialize(ActionData actionData)
        {
        }

        /// <summary>
        /// 外部调用的接口
        /// </summary>
        /// <param name="args"></param>
        public virtual void Execute(IEventActArgs args)
        {
        }
    }
}