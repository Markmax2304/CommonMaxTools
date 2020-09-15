using UnityEngine;

namespace CommonMaxTools.Extensions
{
    public static class VectorExtension
    {
        public static Vector3 ToVector3(this Vector2 vector, float z)
        {
            return new Vector3(vector.x, vector.y, z);
        }
    }
}
