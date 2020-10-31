using System.Collections.Generic;

using UnityEngine;

namespace CommonMaxTools.Tools
{
    public static class Gizmos2D
    {
        #region Fields

        private const int SEGMENT_COUNT = 60;
        private const float fillColorReducer = 4f;

        private static Color gizmosColor;

        private static List<Vector2> drawedPoints = new List<Vector2>();

        #endregion Fields

        public static void DrawCircle(Vector2 center, float radius, Color color)
        {
            CalculateCirclePoints(center, radius);
            DrawMultiLine(drawedPoints, true, color);
        }

        public static void DrawFillCircle(Vector2 center, float radius, Color color, bool isBorder = true)
        {
            CalculateCirclePoints(center, radius);

            Color fillColor = color;
            fillColor.a /= fillColorReducer;
            SetColor(fillColor);

            Mesh mesh = new Mesh();
            int[] triangles = new int[SEGMENT_COUNT * 3];
            Vector3[] vertices = new Vector3[SEGMENT_COUNT + 1];
            Vector3[] normals = new Vector3[SEGMENT_COUNT + 1];

            // set center of circle
            vertices[0] = center;
            normals[0] = Vector3.back;

            for (int i = 0; i < SEGMENT_COUNT; i++)
            {
                vertices[i + 1] = drawedPoints[i];
                normals[i + 1] = Vector3.back;
                triangles[3 * i] = 0;
                triangles[3 * i + 1] = (i + 1) % SEGMENT_COUNT + 1;
                triangles[3 * i + 2] = i + 1;
            }

            mesh.vertices = vertices;
            mesh.normals = normals;
            mesh.triangles = triangles;

            Gizmos.DrawMesh(mesh);

            RollBackColor();

            if (isBorder)
                DrawMultiLine(drawedPoints, true, color);
        }

        public static void DrawCircleSector(Vector2 center, Vector2 startDir, float angle, float radius, Color color)
        {
            drawedPoints.Clear();

            startDir.Normalize();
            float deltaAngle = 360f / SEGMENT_COUNT;
            float side = Mathf.Sign(angle);
            angle = Mathf.Abs(angle);
            int segmentCount = Mathf.CeilToInt(angle / deltaAngle);

            for (int i = 0; i <= segmentCount; i++)
            {
                float angleCounter = i * 2f * Mathf.PI * Mathf.Rad2Deg / SEGMENT_COUNT;
                angleCounter = Mathf.Clamp(angleCounter, -angle, angle);
                Quaternion rotation = Quaternion.AngleAxis(angleCounter * side, Vector3.forward);
                Vector2 point = (rotation * startDir) * Mathf.Abs(radius);
                drawedPoints.Add(center + point);
            }

            DrawMultiLine(drawedPoints, false, color);

            // TODO: implement drawing of limited radiuses for sector
        }

        public static void DrawFillCircleSector()
        {
            // TODO: implement
            throw new System.NotImplementedException();
        }

        public static void DrawMultiLine(List<Vector2> points, bool cycled, Color color)
        {
            SetColor(color);
            
            int cycles = cycled ? points.Count + 1 : points.Count;
            for (int i = 1; i < cycles; i++)
            {
                Gizmos.DrawLine(points[i - 1], points[i % points.Count]);
            }

            RollBackColor();
        }

        #region Private Fields

        private static void SetColor(Color color)
        {
            gizmosColor = Gizmos.color;
            Gizmos.color = color;
        }

        private static void RollBackColor()
        {
            Gizmos.color = gizmosColor;
        }

        private static void CalculateCirclePoints(Vector2 center, float radius)
        {
            drawedPoints.Clear();

            for (int i = 0; i < SEGMENT_COUNT; i++)
            {
                float angle = i * 2f * Mathf.PI / SEGMENT_COUNT;
                Vector2 point = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
                drawedPoints.Add(center + point);
            }
        }

        #endregion Private Fields
    }
}
