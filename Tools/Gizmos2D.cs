using System.Collections.Generic;

using UnityEngine;

namespace CommonMaxTools.Tools
{
    public static class Gizmos2D
    {
        #region Fields

        private const int segmentCount = 60;

        private static List<Vector2> circlePoints = new List<Vector2>();

        #endregion Fields

        public static void DrawCircle(Vector2 position, float radius, Color color)
        {
            circlePoints.Clear();

            for(int i = 0; i < segmentCount; i++)
            {
                float angle = i * 2f * Mathf.PI / segmentCount;
                Vector2 point = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
                circlePoints.Add(position + point);
            }

            Color currentGizmosColor = Gizmos.color;
            Gizmos.color = color;

            for(int i = 1; i <= segmentCount; i++)
            {
                Gizmos.DrawLine(circlePoints[i - 1], circlePoints[i % segmentCount]);
            }

            Gizmos.color = currentGizmosColor;
        }
    }
}
