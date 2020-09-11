using System;

using UnityEngine;

namespace CommonMaxTools.Attributes
{
    /// <summary>
    /// Attribute for drawing all data of reference object that is assigned to field of other script.
    /// <para>Another words, it's some kind of nested inspector in general inspector</para>
    /// <para>For better displaying, fields of reference object is placed in colored box: if object is MonoBehaviour - red box, if ScriptableObject - green box</para>
    /// 
    /// Inspired and improved from MyBox developed by Deadcows. https://github.com/Deadcows/MyBox
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class DisplayInspectorAttribute : PropertyAttribute
    {
        public readonly float red;
        public readonly float green;
        public readonly float blue;

        public bool IsColorValid => red != -1f && green != -1f && blue != -1f;

        /// <summary>
        /// Initialize a new instance of <see cref="DisplayInspectorAttribute"/> 
        /// <para>If all parameters is defined, background box will be painted in this color</para>
        /// </summary>
        public DisplayInspectorAttribute(float red = -1f, float green = -1f, float blue = -1f)
        {
            this.red = red;
            this.green = green;
            this.blue = blue;
        }
    }
}
