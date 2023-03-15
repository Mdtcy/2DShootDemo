/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年3月11日
 * @modify date 2023年3月11日
 * @desc [记录伤害信息]
 */

#pragma warning disable 0649
using UnityEngine;

namespace LWShootDemo
{
    /// <summary>
    /// 记录伤害信息
    /// </summary>
    public class DamageInfo
    {
        /// <summary>
        /// 伤害值
        /// </summary>
        public int Damage;

        /// <summary>
        /// 伤害的方向
        /// </summary>
        public Vector2 Direction;

        /// <summary>
        /// 是否暴击
        /// </summary>
        public bool IsCrit;

        public DamageInfo(int damage, Vector2 direction, bool isCrit)
        {
            Damage    = damage;
            Direction = direction;
            IsCrit    = isCrit;
        }
    }
}
#pragma warning restore 0649