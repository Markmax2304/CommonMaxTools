using System;

using UnityEngine;

namespace CommonMaxTools.Attributes
{
    /// <summary>
    /// Foldout attributes for inspector fields.
    /// <para>Fields is marked by this attribute with the same name is drawing in row and unite under general contest that has name from the attribute declaration</para>
    /// 
    /// Inspired and stole from MyBox by Deadcows. https://github.com/Deadcows/MyBox
    /// <para>But Deadcows was stole it yourself from another repo that is also specified in Deadcows's repo</para>
    /// <para>NOTE TODO: The attribute shows a little strange view when marked field also has DisplayInspector attr that in turn has ButtonMethod attr inside itself</para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class FoldoutAttribute : PropertyAttribute
    {
        public readonly string name;
        public readonly bool foldEverything;

        public FoldoutAttribute(string name, bool foldEverything = false)
        {
            this.name = name;
            this.foldEverything = foldEverything;
        }
    }
}
