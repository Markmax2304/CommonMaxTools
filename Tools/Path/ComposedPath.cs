using System;
using System.Collections.Generic;

using UnityEngine;

using CommonMaxTools.Utility;

namespace CommonMaxTools.Tools.Path
{
    public class ComposedPath
    {
        #region Fields

        private Vector2 tailPosition;
        private List<IPathable> parts;

        #endregion Fields

        #region Events

        public event Action OnPathUpdated;

        #endregion Events

        #region Properties

        public float Length { get; private set; }

        #endregion Properties

        #region Constructors

        public ComposedPath(Vector2 startPoint)
        {
            parts = new List<IPathable>();
            Length = 0f;

            OnPathUpdated += CalculateLength;
        }

        ~ComposedPath()
        {
            Initialize(Vector2.zero);
        }

        #endregion Constructors

        #region Public Methods

        /// <summary>
        /// Clear all existed parts and reset whole path to defined start point
        /// </summary>
        public void Initialize(Vector2 startPoint)
        {
            tailPosition = startPoint;

            for (int i = 0; i < parts.Count; i++)
                parts[i].IsDestroyed = true;
            
            parts.Clear();
        }

        public void AddLine(Vector2 pos)
        {
            try
            {
                var line = PathFactory.CreatePath<PathLine>();
                line.Initialize(tailPosition, pos);
                parts.Add(line);

                tailPosition = pos;

                OnPathUpdated();
            }
            catch (PathException ex)
            {
#if UNITY_EDITOR
                Debug.LogWarning(ex.Message);
#endif
            }
        }

        public void AddCircleSector(Vector2 pos, float radius, bool clockwise)
        {
            try
            {
                var sector = PathFactory.CreatePath<PathCircleSector>();
                sector.Initialize(tailPosition, pos, radius, clockwise);
                parts.Add(sector);

                tailPosition = pos;

                OnPathUpdated();
            }
            catch (PathException ex)
            {
#if UNITY_EDITOR
                Debug.LogWarning(ex.Message);
#endif
            }
        }

        public Vector2 GetPosition(float distance)
        {
            distance = Mathf.Clamp(distance, 0f, Length);

            for (int i = 0; i < parts.Count; i++)
            {
                float decreasedDistance = distance - parts[i].Length;
                if (decreasedDistance > VectorUtility.Tolerance)
                {
                    distance = decreasedDistance;
                }
                else
                {
                    return parts[i].GetPosition(distance);
                }
            }

            throw new Exception($"Path doesn't contain position by distance {distance}");
        }

        public Quaternion GetRotation(float distance)
        {
            distance = Mathf.Clamp(distance, 0f, Length);

            for (int i = 0; i < parts.Count; i++)
            {
                if (distance > parts[i].Length)
                {
                    distance -= parts[i].Length;
                }
                else
                {
                    return parts[i].GetRotation(distance);
                }
            }

            throw new Exception($"Path doesn't contain rotation by distance {distance}");
        }

        #endregion Public Methods

        #region Private Methods

        private void CalculateLength()
        {
            Length = 0f;

            for (int i = 0; i < parts.Count; i++)
                Length += parts[i].Length;
        }

        #endregion Private Methods
    }

    public interface IPathable
    {
        float Length { get; }
        bool IsDestroyed { get; set; }

        Vector2 GetPosition(float distance);
        Quaternion GetRotation(float distance);
    }

    public class PathException : Exception
    {
        public PathException(string message) : base(message) { }
    }
}
