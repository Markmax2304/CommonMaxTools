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
    /// Inspired and stole from MyBox by DeadCow. https://github.com/Deadcows/MyBox
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class AutoAssignAttribute : PropertyAttribute
    {
    }
}
