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

        public DamageInfo(int damage, Vector2 direction)
        {
            this.Damage = damage;
            this.Direction = direction;
        }
    }
}
#pragma warning restore 0649