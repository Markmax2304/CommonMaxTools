using System;

using UnityEngine;

using CommonMaxTools.Utility;

namespace CommonMaxTools.Extensions
{
    public static class VectorExtension
    {
        #region Vector Casting

        public static Vector3 ToVector3(this Vector2 vector, float z = 0)
        {
            return new Vector3(vector.x, vector.y, z);
        }

        public static Vector3 ToVector3(this Vector2Int vector, float z = 0)
        {
            return new Vector3(vector.x, vector.y, z);
        }

        public static Vector3Int ToVector3Int(this Vector2Int vector, int z = 0)
        {
            return new Vector3Int(vector.x, vector.y, z);
        }

        public static Vector2Int ToVector2Int(this Vector2 vector)
        {
            return new Vector2Int((int)vector.x, (int)vector.y);
        }

        public static Vector2 ToVector2(this Vector2Int vector)
        {
            return new Vector2(vector.x, vector.y);
        }

        #endregion Vector Casting

        /// <summary>
        /// Rotate vector on 90 degrees. If sign equals 1, vector will be rotated in right side. If sign equals -1 -- in left side.
        /// <para>If sign doesn't equal 1 or -1, it will throw exception</para>
        /// </summary>
        public static Vector2 Rotate90(this Vector2 vector, float sign)
        {
            if (Mathf.Abs(sign) != 1)
                throw new ArgumentException($"sign doesn't equal 1 or -1. sign = {sign}");

            return new Vector2(vector.y * -sign, vector.x * sign);
        }

        public static Vector2Int Rotate90(this Vector2Int vector, int sign)
        {
            if (Mathf.Abs(sign) != 1)
                throw new ArgumentException($"sign doesn't equal 1 or -1. sign = {sign}");

            return new Vector2Int(vector.y * -sign, vector.x * sign);
        }

        /// <summary>
        /// Rotate point around zero coord by passed angle
        /// Just Imagine it like clock arrow that is rotating by time. 
        /// So arrrow is your vector, place where arrow fixed to clock is zero coord, and time is your angle value
        /// </summary>
        /// <returns>Final position of point after rotating</returns>
        public static Vector2 Rotate(this Vector2 vector, float angle)
        {
            angle %= 360f;

            // WORK AROUND: this method can return non-precision result
            // and it's matter if we has deal with rotating by 90, 180 or 270 degrees.
            if(angle % 90f == 0)
            {
                switch(Mathf.Abs(Mathf.FloorToInt(angle / 90f) % 4))
                {
                    // 90 degrees
                    case 1:
                        return vector.Rotate90(Mathf.Sign(angle));
                    // 180 degrees
                    case 2:
                        return -vector;
                    // 270 degrees
                    case 3:
                        return vector.Rotate90(-Mathf.Sign(angle));
                    default:
                        return vector;
                }
            }

            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            return rotation * vector;
        }

        /// <summary>
        /// Checks whether your point is in range between two other points
        /// These two points represents rectangle, and this method just checks that your point places iside this rectangle or not
        /// </summary>
        public static bool InRange(this Vector2 point, Vector2 limitPoint1, Vector2 limitPoint2)
        {
            Vector2 minPoint = Vector2.Min(limitPoint1, limitPoint2);
            Vector2 maxPoint = Vector2.Max(limitPoint1, limitPoint2);

            return point.x.InRange(minPoint.x, maxPoint.x) && point.y.InRange(minPoint.y, maxPoint.y);
        }

        public static bool InRange(this Vector2Int point, Vector2Int limitPoint1, Vector2Int limitPoint2, bool includeMaxValue = true)
        {
            Vector2Int minPoint = Vector2Int.Min(limitPoint1, limitPoint2);
            Vector2Int maxPoint = Vector2Int.Max(limitPoint1, limitPoint2);

            return point.x.InRange(minPoint.x, maxPoint.x, includeMaxValue) && point.y.InRange(minPoint.y, maxPoint.y, includeMaxValue);
        }

        public static bool IsZero(this Vector2 vector)
        {
            return Mathf.Abs(vector.x) <= VectorUtility.Tolerance 
                && Mathf.Abs(vector.y) <= VectorUtility.Tolerance;
        }

        public static bool IsPositive(this Vector2 vector)
        {
            return vector.x > 0 && vector.y > 0;
        }

        public static Vector2 SnapPosition(this Vector2 position, float step)
        {
            return position.SnapPosition(Vector2.zero, step);
        }

        /// <summary>
        /// Set your target point to nearer snapping position
        /// You can imagine this like grid where step is distance between lines, and result is nearer node on this grid to your point
        /// </summary>
        public static Vector2 SnapPosition(this Vector2 position, Vector2 originPosition, float step)
        {
            step = Mathf.Abs(step);

            float tailX = (position.x - originPosition.x) % step;
            float tailY = (position.y - originPosition.y) % step;

            position.x -= tailX;
            if (Mathf.Abs(tailX) / step >= .5f)
                position.x += step * Mathf.Sign(tailX);

            position.y -= tailY;
            if (Mathf.Abs(tailY) / step >= .5f)
                position.y += step * Mathf.Sign(tailY);

            return position;
        }
    }
}
