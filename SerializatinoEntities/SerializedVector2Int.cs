using System;

using UnityEngine;

namespace CommonMaxTools.SerializatinoEntities
{
    [Serializable]
    public struct SerializedVector2Int
    {
        public int x;
        public int y;

        public static implicit operator SerializedVector2Int(Vector2Int vector)
        {
            return new SerializedVector2Int() { x = vector.x, y = vector.y };
        }

        public static explicit operator Vector2Int(SerializedVector2Int vector)
        {
            return new Vector2Int(vector.x, vector.y);
        }

        public override int GetHashCode()
        {
            return ((Vector2Int)this).GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return Equals((SerializedVector2Int)obj);
        }

        public bool Equals(SerializedVector2Int vector)
        {
            return this.x == vector.x && this.y == vector.y;
        }

        public override string ToString()
        {
            return String.Format("({0}, {1})", x, y);
        }
    }
}
