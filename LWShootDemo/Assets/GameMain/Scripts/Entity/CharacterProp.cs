using System.Collections.Generic;
using LWShootDemo.BuffSystem.Event;
using UnityEngine;

namespace GameMain
{
    public class CharacterProp : IDProp
    {
        [Header("初始Buff")]
        public List<BuffData> InitBuffs = new();
    }
}