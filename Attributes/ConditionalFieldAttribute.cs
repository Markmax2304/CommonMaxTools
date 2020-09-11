using System;
using System.Linq;

using UnityEngine;

namespace CommonMaxTools.Attributes
{
    /// <summary>
    /// Attribute that configures the visibility of this variable depends on condition of specified variable.
    /// <para>Specidied variable is matching with <see cref="comparedValues"/></para>
    /// 
    /// Inspired and stole from MyBox developed by Deadcows. https://github.com/Deadcows/MyBox
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class ConditionalFieldAttribute : PropertyAttribute
    {
        public string checkedFieldName;
        public bool inverse;
        public string[] comparedValues;

        public ConditionalFieldAttribute(string checkedFieldName, bool inverse = false, params object[] comparedValues)
        {
            this.checkedFieldName = checkedFieldName;
            this.inverse = inverse;
            this.comparedValues = comparedValues.Select(x => x.ToString().ToUpper()).ToArray();
        }
    }
}
