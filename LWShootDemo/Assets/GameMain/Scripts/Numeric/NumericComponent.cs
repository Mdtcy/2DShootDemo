using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityGameFramework.Runtime;

namespace GameMain
{
    public class NumericComponent : MonoBehaviour
    {
        public readonly Dictionary<int, int> NumericDic = new Dictionary<int, int>();

        private Entity _entity;
        public void Init(Entity entity)
        {
            _entity = entity;
        }
        
        public float GetAsFloat(NumericType numericType)
        {
            return (float)GetByKey((int)numericType) / 10000;
        }

        public int GetAsInt(NumericType numericType)
        {
            return GetByKey((int)numericType);
        }

        public void Set(NumericType nt, float value)
        {
            Assert.IsTrue(nt > NumericType.Max || nt == NumericType.Hp, $"Set只能设置大于Max的值,当前值为{nt}");
            Assert.IsTrue(nt > NumericType.Float, "NumericType must be float type");
            this[nt] = (int) (value * 10000);
        }

        public void Set(NumericType nt, int value)
        {
            Assert.IsTrue(nt > NumericType.Max || nt == NumericType.Hp, $"Set只能设置大于Max的值,当前值为{nt}");
            Assert.IsTrue(nt < NumericType.Float, "NumericType must be int type");
            this[nt] = value;
        }

        private int this[NumericType numericType]
        {
            get
            {
                return this.GetByKey((int) numericType);
            }
            set
            {
                int v = this.GetByKey((int) numericType);
                if (v == value)
                {
                    return;
                }

                NumericDic[(int)numericType] = value;

                UpdateNumeric(numericType);
            }
        }
        private int GetByKey(int key)
        {
            int value = 0;
            this.NumericDic.TryGetValue(key, out value);
            return value;
        }
        private void UpdateNumeric(NumericType numericType)
        {
            if (numericType < NumericType.Max)
            {
                return;
            }
            int final = (int) numericType / 10;
            int bas = final * 10 + 1; 
            int add = final * 10 + 2;
            int pct = final * 10 + 3;
            int finalAdd = final * 10 + 4;
            int finalPct = final * 10 + 5;

            // 一个数值可能会多种情况影响，比如速度,加个buff可能增加速度绝对值100，也有些buff增加10%速度，所以一个值可以由5个值进行控制其最终结果
            // final = (((base + add) * (100 + pct) / 100) + finalAdd) * (100 + finalPct) / 100;
            this.NumericDic[final] = ((this.GetByKey(bas) + this.GetByKey(add)) * (100 + this.GetByKey(pct)) / 100 + this.GetByKey(finalAdd)) * (100 + this.GetByKey(finalPct)) / 100;
            
            GameEntry.Event.Fire(NumericChangeEventArgs.EventId, NumericChangeEventArgs.Create(_entity, numericType, final));
        }

        public void OnHide()
        {
            NumericDic.Clear();
        }
    }
}