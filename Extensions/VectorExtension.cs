using System;

using UnityEngine;

using CommonMaxTools.Utility;

namespace CommonMaxTools.Extensions
{
    public static class VectorExtension
    {
        public static Vector3 ToVector3(this Vector2 vector, float z)
        {
            return new Vector3(vector.x, vector.y, z);
        }

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

        public static bool InRange(this Vector2 point, Vector2 limitPoint1, Vector2 limitPoint2)
        {
            Vector2 minPoint = Vector2.Min(limitPoint1, limitPoint2);
            Vector2 maxPoint = Vector2.Max(limitPoint1, limitPoint2);

            return point.x.InRange(minPoint.x, maxPoint.x) && point.y.InRange(minPoint.y, maxPoint.y);
        }

        public static bool IsEmpty(this Vector2 vector)
        {
            return Mathf.Abs(vector.x) <= VectorUtility.Tolerance 
                && Mathf.Abs(vector.y) <= VectorUtility.Tolerance;
        }
    }
}
