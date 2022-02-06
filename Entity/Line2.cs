using UnityEngine;

using CommonMaxTools.Utility;

namespace CommonMaxTools.Entity
{
    public struct Line2
    {
        public Vector2 start;
        public Vector2 end;

        public Vector2 Vector => end - start;
        public float Magnitude => Vector.magnitude;
        public float SqrMagnitude => Vector.sqrMagnitude;

        public Line2(Vector2 start, Vector2 end)
        {
            this.start = start;
            this.end = end;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="line1"></param>
        /// <param name="line2"></param>
        /// <returns></returns>
        public static bool IsLinesIntersected(Line2 line1, Line2 line2)
        {
            float prod1 = VectorUtility.CalculateVectorProduct(line1.Vector, line2.start - line1.start);
            float prod2 = VectorUtility.CalculateVectorProduct(line1.Vector, line2.end - line1.start);

            //means that lines lay on the same direction, and we should detect they are lay on each other or not
            if (prod1 == 0 && prod2 == 0)
                return CalculateMaxDistance(line1, line2, true) <= line1.SqrMagnitude + line2.SqrMagnitude;

            float prod3 = VectorUtility.CalculateVectorProduct(line2.Vector, line1.start - line2.start);
            float prod4 = VectorUtility.CalculateVectorProduct(line2.Vector, line1.end - line2.start);

            return prod1 * prod2 <= 0f && prod3 * prod4 <= 0f;
        }

        /// <summary>
        /// Computes the longest distance between two lines. It's just max possible distance, no any extra conditions. 
        /// Obviously It must be distance between any start/end points and another one of other line.
        /// </summary>
        /// <param name="square">Defines that distance whether must be returned as square root or as regular value.</param>
        public static float CalculateMaxDistance(Line2 line1, Line2 line2, bool square)
        {
            Vector2 l1 = line1.start - line2.start;
            Vector2 l2 = line1.start - line2.end;
            Vector2 l3 = line1.end - line2.start;
            Vector2 l4 = line1.end - line2.end;

            float dis1 = square ? l1.sqrMagnitude : l1.magnitude;
            float dis2 = square ? l2.sqrMagnitude : l2.magnitude;
            float dis3 = square ? l3.sqrMagnitude : l3.magnitude;
            float dis4 = square ? l4.sqrMagnitude : l4.magnitude;

            return Mathf.Max(Mathf.Max(dis1, dis2), Mathf.Max(dis3, dis4));
        }

        // TODO: implement Equals and Hash methods
    }
}
