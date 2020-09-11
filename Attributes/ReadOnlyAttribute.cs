using System;

using UnityEngine;

namespace CommonMaxTools.Attributes
{
    /// <summary>
    /// Attribute for drawing property as read only in inpector. User won't be able to edit this property
    /// 
    /// Inspired and stole from MyBox developed by Deadcows. https://github.com/Deadcows/MyBox
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class ReadOnlyAttribute : PropertyAttribute
    {
        public ReadOnlyAttribute()
        {
        }
    }
}
