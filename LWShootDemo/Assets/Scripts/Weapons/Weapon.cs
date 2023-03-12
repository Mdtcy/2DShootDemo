/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年3月12日
 * @modify date 2023年3月12日
 * @desc []
 */

#pragma warning disable 0649
using LWShootDemo.Entities;
using UnityEngine;

namespace LWShootDemo.Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        public abstract void Init(Entity entity);
        public abstract void Use();
    }
}
#pragma warning restore 0649