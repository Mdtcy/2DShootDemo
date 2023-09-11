using System;
using UnityEngine;

namespace BerserkPixel.Tilemap_Generator.SO
{
    public abstract class MapConfigSO : ScriptableObject, IEquatable<MapConfigSO>
    {
        [Delayed]
        public int width;
        [Delayed]
        public int height;

        protected const float _compareThreshold = 0.1f;

        public Action<MapConfigSO> OnMapChange;

        private void OnValidate()
        {
            if (width <= 0) width = 1;
            if (height <= 0) height = 1;
        }

        public void MapChange()
        {
            OnMapChange?.Invoke(this);
        }

        public abstract bool Equals(MapConfigSO map);

        public override bool Equals(object other)
        {
            //Sequence of checks should be exactly the following.
            //If you don't check "other" on null, then "other.GetType()" further can 
            //throw NullReferenceException
            if (other == null)
                return false;

            // Comparing by references here is not necessary.
            // If you're sure that many compares will end up be references comparing 
            // then you can implement it
            if (ReferenceEquals(this, other))
                return true;

            //If parent and inheritor instances can possibly be treated as equal then 
            //you can immediately move to comparing their fields.
            return GetType() == other.GetType() && Equals(other as MapConfigSO);
        }

        public abstract override int GetHashCode();
    }
}