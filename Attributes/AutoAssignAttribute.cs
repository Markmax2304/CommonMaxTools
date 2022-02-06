using System;

using UnityEngine;

namespace CommonMaxTools.Attributes
{
    /// <summary>
    /// Provide auto assigning of component for fields.
    /// For example, we have a field of transform type and with this attribute 
    /// the field will be filled by own transform (transform of gameobject that contains script with attributed field).
    /// So we don't need to assign it manually.
    /// Support single fields and arrays. For other types will be thrown error.
    /// Works while scene is being saved.
    /// 
    /// Inspired and stole from MyBox by Deadcows. https://github.com/Deadcows/MyBox
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class AutoAssignAttribute : PropertyAttribute
    {
        public readonly int deepLevel;

        /// <summary>
        /// Create a new instance of attributes class
        /// </summary>
        /// <param name="deepLevel">It shows which level are used for searching on. 
        /// If parameter is default, search is starting at the root level and continues untill target component is found or until all child elements will be handled</param>
        public AutoAssignAttribute(int deepLevel = -1)
        {
            this.deepLevel = deepLevel;
        }
    }
}
