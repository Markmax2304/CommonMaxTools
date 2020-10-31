using System;

using UnityEngine;

using CommonMaxTools.Extensions;
using CommonMaxTools.Utility;

namespace CommonMaxTools.Tools.Path
{
    public class PathCircleSector : IPathable
    {
        #region Fields

        private Vector2 circleCenter;
        // NOTE: magnitude of this vector equals radius
        private Vector2 startDir;
        private float angle;
        private bool clockwise;

        #endregion Fields

        #region Properties

        public float Length { get; private set; }
        public bool IsDestroyed { get; set; }

        #endregion Properties

        #region Public Methods

        /// <param name="start">Point on circle that's beginning of circle sector</param>
        /// <param name="end">Point on circle that's ending of circle sector</param>
        /// <param name="radius">signed radius of circle. Also defines side of circle.
        /// If sign of radius is positive - circle center will be placed on the left side by vectro from start to end.
        /// If sign is negative - on the right side.</param>
        public void Initialize(Vector2 start, Vector2 end, float radius, bool clockwise)
        {
            Vector2 distanceDir = end - start;
            float distance = distanceDir.magnitude;
            float signRadius = Mathf.Sign(radius);
            radius = Mathf.Abs(radius);

            if (distance > radius * 2)
                throw new PathException("Distance between start and end is larger than diametr");

            if (distanceDir.IsEmpty())
                throw new PathException("Distance between start and end is almost zero");

            CalculateCircleCenter(start, distanceDir, distance, radius, signRadius);
            startDir = start - circleCenter;

            // calculate clockwise and sector angle
            this.clockwise = clockwise ^ !signRadius.SignToBool();
            angle = VectorUtility.Angle360(circleCenter - start, circleCenter - end, this.clockwise);

            Length = angle * Mathf.Deg2Rad * radius;
        }

        public Vector2 GetPosition(float distance)
        {
            return circleCenter + GetDeltaDirection(distance);
        }

        public Quaternion GetRotation(float distance)
        {
            Vector2 deltaDir = GetDeltaDirection(distance);
            float angle = Vector2.SignedAngle(Vector2.up, deltaDir.Rotate90(-clockwise.SignToFloat()));
            return Quaternion.AngleAxis(angle, Vector3.forward);
        }

        #endregion Public Methods

        #region Private Methods

        private void CalculateCircleCenter(Vector2 start, Vector2 distanceDir, float distance, float radius, float signRadius)
        {
            Vector2 middle = start + distanceDir / 2f;
            float altitude = Mathf.Sqrt(Mathf.Pow(radius, 2) - Mathf.Pow(distance / 2f, 2));
            circleCenter = middle + distanceDir.normalized.Rotate90(signRadius) * altitude;
        }

        private Vector2 GetDeltaDirection(float distance)
        {
            float deltaAngle = distance.Normalize(Length) * angle;
            return startDir.Rotate(deltaAngle * -clockwise.SignToFloat());
        }

        #endregion Private Methods
    }
}
