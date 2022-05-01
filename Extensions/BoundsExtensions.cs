using UnityEngine;

namespace CommonMaxTools.Extensions
{
    public static class BoundsExtensions
    {
        // TODO: exchange by + operator
        /// <summary>
        /// 
        /// </summary>
        public static Bounds Merge(this Bounds oneRect, Bounds otherRect)
        {
            Vector3 min = Vector3.Min(oneRect.min, otherRect.min);
            Vector3 max = Vector3.Max(oneRect.max, otherRect.max);

            return new Bounds() { min = min, max = max };
        }

        /// <summary>
        /// 
        /// </summary>
        public static BoundsInt Merge(this BoundsInt oneRect, BoundsInt otherRect)
        {
            Vector3Int min = Vector3Int.Min(oneRect.min, otherRect.min);
            Vector3Int max = Vector3Int.Max(oneRect.max, otherRect.max);

            return new BoundsInt() { min = min, max = max };
        }
    }
}
