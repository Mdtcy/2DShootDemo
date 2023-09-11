using System;
using BerserkPixel.Tilemap_Generator.Attributes;
using UnityEngine;

namespace BerserkPixel.Tilemap_Generator.SO
{
    [CreateAssetMenu(fileName = "New Map Configuration", menuName = "Avesta/Maps/Cellular")]
    public class CellularMapConfigSO : MapConfigSO, IEquatable<CellularMapConfigSO>
    {
        [DelayedCallback(nameof(MapChange)), Range(0, 100)]
        public float fillPercent = 50;
        
        [DelayedCallback(nameof(MapChange))]
        public string seed;
         
        [DelayedCallback(nameof(MapChange)), Range(0, 10)]
        public int smoothSteps = 5;

        public override bool Equals(MapConfigSO map)
        {
            var other = map as CellularMapConfigSO;

            return Equals(other);
        }

        public bool Equals(CellularMapConfigSO other)
        {
            return other != null &&
                   Math.Abs(fillPercent - other.fillPercent) < _compareThreshold &&
                   string.Compare(seed, other.seed, StringComparison.CurrentCulture) == 0 &&
                   smoothSteps == other.smoothSteps;
        }

        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                var hash = (int) 2166136261;
                // Suitable nullity checks etc, of course :)
                hash = hash * 16777619 + seed.GetHashCode();
                hash = hash * 16777619 + fillPercent.GetHashCode();
                hash = hash * 16777619 + smoothSteps.GetHashCode();
                return hash;
            }
        }
    }
}