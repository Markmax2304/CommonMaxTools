using System;

using UnityEngine;

using CommonMaxTools.Extensions;

namespace CommonMaxTools.Tools.Path
{
    public class PathLine : IPathable
    {
        #region Fields

        private Vector2 start;
        private Vector2 end;

        #endregion Fields

        #region Properties

        public float Length { get; private set; }

        public bool IsDestroyed { get; set; }

        #endregion Properties

        #region Public Methods

        public void Initialize(Vector2 start, Vector2 end)
        {
            Vector2 dir = end - start;

            if (dir.IsEmpty())
                throw new PathException("Distance between start and end is almost zero");

            this.start = start;
            this.end = end;
            Length = dir.magnitude;
        }

        public Vector2 GetPosition(float distance)
        {
            float deltaDistance = distance.Normalize(Length);
            return Vector2.Lerp(start, end, deltaDistance);
        }

        public Quaternion GetRotation(float distance)
        {
            Vector2 lineDirection = end - start;
            float angle = Vector2.SignedAngle(Vector2.up, lineDirection);
            return Quaternion.AngleAxis(angle, Vector3.forward);
        }

        #endregion Public Methods
    }
}
