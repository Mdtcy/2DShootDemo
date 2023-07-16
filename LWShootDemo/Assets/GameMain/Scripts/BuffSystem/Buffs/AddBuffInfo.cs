using LWShootDemo.BuffSystem.Event;
using UnityEngine;

namespace LWShootDemo.BuffSystem.Buffs
{
    public class AddBuffInfo
    {
        /// <summary>
        /// buff的负责人是谁，可以是null
        /// </summary>
        public GameObject Caster;

        /// <summary>
        /// buff要添加给谁，这个必须有
        /// </summary>
        public GameObject Target;

        /// <summary>
        /// buff的model，这里当然可以从数据里拿，也可以是逻辑脚本现生成的
        /// </summary>
        public BuffData BuffData;

        /// <summary>
        /// 要添加的层数，负数则为减少
        /// </summary>
        public int AddStack;

        /// <summary>
        /// 关于时间，是改变还是设置为, true代表设置为，false代表改变
        /// </summary>
        public bool DurationSetTo;

        /// <summary>
        /// 是否是一个永久的buff，即便=true，时间设置也是有意义的，因为时间如果被减少到0以下，即使是永久的也会被删除
        /// </summary>
        public bool Permanent;

        /// <summary>
        /// 时间值，设置为这个值，或者加上这个值，单位：秒
        /// </summary>
        public float Duration;
        
        public AddBuffInfo(BuffData buffData, 
            GameObject caster,
            GameObject target,
            int stack, 
            float duration,
            bool durationSetTo = true,
            bool permanent = false)
        {
            this.BuffData = buffData;
            this.Caster = caster;
            this.Target = target;
            this.AddStack = stack;
            this.Duration = duration;
            this.DurationSetTo = durationSetTo;
            this.Permanent = permanent;
        }
    }
}