using System;

using UnityEngine;

namespace CommonMaxTools.Attributes
{
    /// <summary>
    /// Decorative attribute that is drawn separated line with a title above the field
    /// 
    /// Inspired and stole from MyBox by DeadCow. https://github.com/Deadcows/MyBox
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class SeparatorAttribute : PropertyAttribute
    {
        public readonly string title;

        public SeparatorAttribute(string title = null)
        {
            this.title = title;
        }
    }
}
