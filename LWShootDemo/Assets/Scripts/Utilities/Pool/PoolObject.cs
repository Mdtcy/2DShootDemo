/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年3月13日
 * @modify date 2023年3月13日
 * @desc [PoolObject]
 */

#pragma warning disable 0649
using UnityEngine;

namespace LWShootDemo.Pool
{
    public class PoolObject : MonoBehaviour
    {
        public virtual void OnSpawn() { }
        public virtual void OnDespawn() { }
    }
}
#pragma warning restore 0649