using System;

using UnityEngine;

namespace CommonMaxTools.Extensions
{
    public static class PrimitiveTypeExtension
    {
        #region Int

        public static bool InRange(this int value, int minValue, int maxValue, bool includeMaxValue = true)
        {
            return (value >= minValue && value < maxValue) || (includeMaxValue && value == maxValue);
        }

        #endregion Int

        #region Float

        /// <summary>
        /// Normalize float value to bounds [0, 1] where 0 - 0 and 1 - <see cref="maxValue"/>
        /// <para>If value is less than zero, method returs zero</para>
        /// <para>If value is bigger than <see cref="maxValue"/>, method returns one</para>
        /// </summary>
        /// <param name="value">Value to normalize</param>
        /// <param name="maxValue"> Max value of normalization.</param>
        /// <returns>Normalized value of [0, 1] bounds</returns>
        public static float Normalize(this float value, float maxValue)
        {
            value = Mathf.Clamp(value, 0f, maxValue);
            return value / maxValue;
        }

        public static float Inverse01(this float value)
        {
            if (!value.InRange(0, 1))
                throw new Exception($"Value to inverse must be in range from 0 to 1. This value is {value}");

            return 1f - value;
        }

        /// <summary>
        /// Check that value is in range, inclusive <see cref="minValue"/> and <see cref="maxValue"/>
        /// </summary>
        public static bool InRange(this float value, float minValue, float maxValue)
        {
            return value >= minValue && value <= maxValue;
        }

        /// <summary>
        /// Converts to bool, if value is positive or zero - returns true, otherwise - false
        /// </summary>
        public static bool SignToBool(this float value)
        {
            return value >= 0;
        }

        #endregion Float

        #region Bool

        public static float SignToFloat(this bool value)
        {
            return value ? 1f : -1f;
        }

        #endregion Bool
    }
}
