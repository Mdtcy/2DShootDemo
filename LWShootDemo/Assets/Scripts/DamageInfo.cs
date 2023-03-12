/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年3月11日
 * @modify date 2023年3月11日
 * @desc [DamageInfo]
 */

#pragma warning disable 0649
using UnityEngine;

namespace LWShootDemo
{
    public class DamageInfo
    {
        public int Damage;

        public Vector2 Direction;

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