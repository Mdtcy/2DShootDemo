/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年3月14日
 * @modify date 2023年3月14日
 * @desc [多武器]
 */

#pragma warning disable 0649
using System.Collections.Generic;
using LWShootDemo.Entities;
using UnityEngine;

namespace LWShootDemo.Weapons
{
    /// <summary>
    /// 多武器
    /// </summary>
    public class MultiWeapon : Weapon
    {

        #region FIELDS

        [SerializeField]
        private List<Weapon> weapons;

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        public override void Init(Entity entity)
        {
            foreach (var weapon in weapons)
            {
                weapon.Init(entity);
            }
        }

        public override void Use()
        {
            foreach (var weapon in weapons)
            {
                weapon.Use();
            }
        }

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649