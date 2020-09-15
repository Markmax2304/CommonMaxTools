using System.Collections.Generic;

using UnityEngine;

namespace CommonMaxTools.Tools
{
    public static class Gizmos2D
    {
        #region Fields

        private const int segmentCount = 60;
        private const float fillColorReducer = 4f;

        private static Color gizmosColor;

        private static List<Vector2> points = new List<Vector2>();

        #endregion Fields

        public static void DrawCircle(Vector2 position, float radius, Color color)
        {
            CalculateCirclePoints(position, radius);
            SetColor(color);
            DrawMultiLine(true);
            RollBackColor();
        }

        public static void DrawFillCircle(Vector2 position, float radius, Color color, bool isBorder = false)
        {
            CalculateCirclePoints(position, radius);

            Color fillColor = color;
            fillColor.a /= fillColorReducer;
            SetColor(fillColor);

            Mesh mesh = new Mesh();
            int[] triangles = new int[segmentCount * 3];
            Vector3[] vertices = new Vector3[segmentCount + 1];
            Vector3[] normals = new Vector3[segmentCount + 1];

            // set center of circle
            vertices[0] = position;
            normals[0] = Vector3.back;

            for (int i = 0; i < segmentCount; i++)
            {
                vertices[i + 1] = points[i];
                normals[i + 1] = Vector3.back;
                triangles[3 * i] = 0;
                triangles[3 * i + 1] = (i + 1) % segmentCount + 1;
                triangles[3 * i + 2] = i + 1;
            }

            mesh.vertices = vertices;
            mesh.normals = normals;
            mesh.triangles = triangles;

            Gizmos.DrawMesh(mesh);

            RollBackColor();

            if (isBorder)
            {
                SetColor(color);
                DrawMultiLine(true);
                RollBackColor();
            }
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

        private static void CalculateCirclePoints(Vector2 position, float radius)
        {
            points.Clear();

            for (int i = 0; i < segmentCount; i++)
            {
                float angle = i * 2f * Mathf.PI / segmentCount;
                Vector2 point = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
                points.Add(position + point);
            }
        }

        private static void DrawMultiLine(bool cycled)
        {
            int cycles = cycled ? segmentCount + 1 : segmentCount;
            for (int i = 1; i < cycles; i++)
            {
                Gizmos.DrawLine(points[i - 1], points[i % segmentCount]);
            }
        }

        #endregion Private Fields
    }
}
