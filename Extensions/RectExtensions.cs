﻿using UnityEngine;

namespace CommonMaxTools.Extensions
{
    public static class RectExtensions
    {
        /// <summary>
        /// Checks whether other Rect can be located inside host Rect entirely
        /// </summary>
        public static bool Contains(this Rect host, Rect other)
        {
            var dirToMinCorner = host.min - other.min;
            var dirToMaxCorner = host.max - other.max;

            return !dirToMinCorner.IsPositive() && dirToMaxCorner.IsPositive();
        }

        /// <summary>
        /// Create new one rect which can contain both passing rects, and which has common borders with them
        /// </summary>
        public static Rect Merge(this Rect oneRect, Rect otherRect)
        {
            Vector2 min = Vector2.Min(oneRect.min, otherRect.min);
            Vector2 max = Vector2.Max(oneRect.max, otherRect.max);

            return Rect.MinMaxRect(min.x, min.y, max.x, max.y);
        }

        /// <summary>
        /// Scales Rect by passed scale based on passed center
        /// </summary>
        public static Rect ScaleTo(this Rect rect, float scale, Vector2 center)
        {
            rect.max = scale * rect.max + center * (1 - scale);
            rect.min = scale * rect.min + center * (1 - scale);
            return rect;
        }

        /// <summary>
        /// Translates Rect by passed delta distance
        /// </summary>
        public static Rect Translate(this Rect rect, Vector2 moveDelta)
        {
            rect.position += moveDelta;
            return rect;
        }
    }
}
