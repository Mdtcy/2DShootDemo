using LWShootDemo.BuffSystem.Event;
using UnityEngine;

namespace GameMain
{
    [CreateAssetMenu]
    public class BuffTable : SOTableList<BuffData>
    {
        public override int StartId => IdUtility.BuffStartId();
    }
}