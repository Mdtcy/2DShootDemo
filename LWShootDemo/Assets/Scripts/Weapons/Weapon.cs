/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年3月12日
 * @modify date 2023年3月12日
 * @desc [武器]
 */

#pragma warning disable 0649
using LWShootDemo.Entities;
using UnityEngine;

namespace LWShootDemo.Weapons
{
    /// <summary>
    /// 武器
    /// </summary>
    public abstract class Weapon : MonoBehaviour
    {
        /// <summary>
        /// 初始化武器
        /// </summary>
        /// <param name="oldEntity"></param>
        public abstract void Init(OldEntity oldEntity);

        /// <summary>
        /// 使用
        /// </summary>
        public abstract void Use();

        public abstract void RotateTo(Vector3 dir);
    }
}
#pragma warning restore 0649