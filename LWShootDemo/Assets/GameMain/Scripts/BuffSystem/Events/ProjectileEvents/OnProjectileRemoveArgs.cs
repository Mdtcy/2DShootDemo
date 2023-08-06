using GameFramework;
using LWShootDemo.BuffSystem.Event;
using UnityEngine;

namespace GameMain
{
    public class OnProjectileRemoveArgs : BaseProjectileEventActArgs
    {
        public static OnProjectileRemoveArgs Create()
        {
            var args = ReferencePool.Acquire<OnProjectileRemoveArgs>();
            return args;
        }
    }
}