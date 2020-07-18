using System;
using UnityEditor;
using UnityEngine;

namespace CommonMaxTools.Attributes
{
    /// <summary>
    /// Provide to execute a method that is declared with this attribute from inspector
    /// In editor UI that looks like a button with method name
    /// 
    /// Inspired by MyBox that is developed by Deadcows. https://github.com/Deadcows/MyBox
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class ButtonMethodAttribute : PropertyAttribute
    {
        public string Text { get; }

        public ButtonMethodAttribute(string text = null)
        {
            Text = text;
        }
    }
}
