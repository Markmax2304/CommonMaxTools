using UnityEngine;

namespace CommonMaxTools.Attributes
{
    /// <summary>
    /// This attribute doesn't accept to assign negative value to variables through Inspector.
    /// <para>Supports int, float and all Vector types</para>
    /// 
    /// Inspired and stole from MyBox by Deadcows. https://github.com/Deadcows/MyBox
    /// </summary>
    public class PositiveValueAttribute : PropertyAttribute
    {
        public readonly bool inclusiveZero;

        public PositiveValueAttribute(bool inclusiveZero = true)
        {
            this.inclusiveZero = inclusiveZero;
        }
    }
}
