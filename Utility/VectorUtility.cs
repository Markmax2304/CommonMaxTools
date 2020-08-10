using UnityEngine;

namespace CommonMaxTools.Utility
{
    public static class VectorUtility
    {
        #region Vector2

        /// <summary>
        /// Calculate angle between two vectors in range from 0 to 360 degrees
        /// </summary>
        public static float Angle360(Vector2 from, Vector2 to, bool clockwise = false)
        {
            float angle = Vector2.SignedAngle(from, to);

            if (angle < 0f)
                // special formula for converting negative angle to positive in range from 0 to 360
                angle = Mathf.Abs(-180 - angle) + 180f;

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

        #endregion Vector2

        #region Matrix

        // TODO: Redesign method for all rotation cases
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
    }
}
