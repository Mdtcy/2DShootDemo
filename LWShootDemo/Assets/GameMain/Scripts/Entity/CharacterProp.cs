using System;
using System.Collections.Generic;
using LWShootDemo.BuffSystem.Event;
using LWShootDemo.Entities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameMain
{
    public class CharacterProp : IDProp
    {
        [LabelText("阵营")]
        public Side Side;
        
        [Header("初始数值")]
        public List<InitNumeric> InitNumerics = new();

        [Header("初始Buff")]
        public List<AddBuffData> InitBuffs = new();

        [Serializable]
        public class AddBuffData
        {
            [LabelText("Buff")]
            public BuffData Buff;

            public int Stack = 1;
            
            [HideIf("IsPermanent")]
            public float Duration = 10;

            public bool IsPermanent = true;
        }
        
        [Serializable]
        public class InitNumeric
        {
            public NumericType Type;
            
            public int Value;
        }
    }
}