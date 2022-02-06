using System.Collections.Generic;

using UnityEngine;

using CommonMaxTools.Extensions;

namespace CommonMaxTools.Utility
{
    public static class VectorUtility
    {
        public const float Tolerance = 1E-6f;

        #region Vector2

        /// <summary>
        /// Calculate angle between two vectors in range from 0 to 360 degrees
        /// </summary>
        public static float Angle360(Vector2 from, Vector2 to, bool clockwise = false)
        {
            float angle = Vector2.SignedAngle(from, to);

            if (angle < 0f)
                angle = 360f + angle;

            return clockwise ? 360f - angle : angle;
        }

        public static Vector2 RandomRange(Vector2 to)
        {
            return RandomRange(Vector2.zero, to);
        }
        
        public static Vector2 RandomRange(Vector2 from, Vector2 to)
        {
            return new Vector2(Random.Range(from.x, to.x), Random.Range(from.y, to.y));
        }

        public static void SwapVectors(ref Vector2 value1, ref Vector2 value2)
        {
            Vector2 temp = value1;
            value1 = value2;
            value2 = temp;
        }

        public static Vector2 AbsVector(Vector2 value)
        {
            return new Vector2(Mathf.Abs(value.x), Mathf.Abs(value.y));
        }

        /// <summary>
        /// Calculate vector multiplying. 
        /// It's absolutly the same formula as finding determinant of 2D matrix
        /// </summary>
        public static float CalculateVectorProduct(Vector2 v1, Vector2 v2)
        {
            return v1.x * v2.y - v1.y * v2.x;
        }

        /// <summary>
        /// Gets area with sign of polygon. Sign depends on order of points - clockwise or opposite.
        /// <para>NOTE! points must be sorted in order of how they are placing on edge of polygon.</para>
        /// </summary>
        public static float CalculateSignAreaOfPolygon(List<Vector2> points)
        {
            float result = 0;

            for (int i = 0; i < points.Count; i++)
            {
                Vector2 point = points[CollectionUtility.RoundIndex(i, points.Count)];
                Vector2 nextPoint = points[CollectionUtility.RoundIndex(i + 1, points.Count)];

                result += VectorUtility.CalculateVectorProduct(point, nextPoint);
            }

            return result / 2f;
        }

        #endregion Vector2

        #region Matrix

        // TODO: Redesign method for all rotation cases
        /// <summary>
        /// [DEPRECATED] Use Quaternion.AngleAxis() instead
        /// </summary>
        public static Matrix4x4 CreateMatrixRotatinZ(float angle)
        {
            Matrix4x4 matrix = Matrix4x4.identity;
            float relativeAngleRad = Mathf.Deg2Rad * angle;

            matrix[0, 0] = Mathf.Cos(relativeAngleRad);
            matrix[0, 1] = Mathf.Sin(relativeAngleRad);
            matrix[1, 0] = -Mathf.Sin(relativeAngleRad);
            matrix[1, 1] = Mathf.Cos(relativeAngleRad);

            return matrix;
        }

        #endregion Matrix

        #region Equations

        public static bool IsPointBelongedCircleSector(Vector2 point, Vector2 circleCenter, float radius, Vector2 startDirection, float angleSector)
        {
            startDirection.Normalize();
            Vector2 endDirection = startDirection.Rotate(angleSector);
            float angleSign = Mathf.Sign(angleSector);

            // direction sign is being defined by in what quarter of circle it placed
            // 1 and 3 quarter - positive
            // 2 and 4 quarter - negative
            int quarterIndex = Mathf.FloorToInt(VectorUtility.Angle360(Vector2.right, startDirection) / 90f);
            float startDirSign = quarterIndex % 2 == 0 ? 1 : -1;
            quarterIndex = Mathf.FloorToInt(VectorUtility.Angle360(Vector2.right, endDirection) / 90f);
            float endDirSign = quarterIndex % 2 == 0 ? 1 : -1;

            // if point isn't placed on circle bounds, we should skip other verification
            if (GetCircleEquationDifference(point, circleCenter, radius) != 0f)
                return false;

            // verify point is placed between two dirction lines
            bool inStartBound = GetLineEquationDifference(point, circleCenter, circleCenter + startDirection) * startDirSign * angleSign >= 0f;
            bool inEndBound = GetLineEquationDifference(point, circleCenter, circleCenter + endDirection) * endDirSign * angleSign <= 0f;

            return Mathf.Abs(angleSector) > 180f ? inStartBound || inEndBound : inStartBound && inEndBound;
        }

        /// <summary>
        /// Gets value of straight-line inequality.
        /// <para>if value less than 0 - point in positive area from Y axis</para>
        /// <para>if value equals 0 - point on line</para>
        /// <para>if value bigger than 0 - point in negative area from Y axis </para>
        /// <para>NOTE: returned value is distance from point to line(perpendicular)</para>
        /// </summary>
        public static float GetLineEquationDifference(Vector2 point, Vector2 linePoint1, Vector2 linePoint2)
        {
            float lineDeltaY = linePoint2.y - linePoint1.y;
            float lineDeltaX = linePoint2.x - linePoint1.x;

            // representation of straight-line equation 
            // (x - x1)/(x2 - x1) == (y - y1)/(y2 - y1)
            float result = (point.y - linePoint1.y) * Mathf.Abs(lineDeltaX) * Mathf.Sign(lineDeltaY) - (point.x - linePoint1.x) * Mathf.Abs(lineDeltaY) * Mathf.Sign(lineDeltaX);

            // always when point is placed on line, result is very small value but not zero
            if (Mathf.Abs(result) <= Tolerance)
                return 0f;

            // WORK AROUND: for line directed on 90 and 180 degrees
            Vector2 lineDir = (linePoint2 - linePoint1).normalized;
            if (lineDir == Vector2.up || lineDir == Vector2.left)
                result = -result;

            return result;
        }

        /// <summary>
        /// Gets value of circle inequality.
        /// <para>if value less than 0 - point inside circle</para>
        /// <para>if value equals 0 - point on circle border</para>
        /// <para>if value bigger than 0 - point outside circle</para>
        /// </summary>
        public static float GetCircleEquationDifference(Vector2 point, Vector2 circleCenter, float radius)
        {
            // representation of circle equation
            // (x - a)^2 + (y - b)^2 == r^2
            float result = Mathf.Pow(point.x - circleCenter.x, 2) + Mathf.Pow(point.y - circleCenter.y, 2) - Mathf.Pow(radius, 2);

            // always when point is placed on circle bounds, result is very small value but not zero
            if (Mathf.Abs(result) <= Tolerance)
                result = 0f;

            return result;
        }

        #endregion Equations
    }
}
