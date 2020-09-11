using System;

using UnityEngine;
using UnityEditor;

using CommonMaxTools.Attributes;
using CommonMaxTools.Extensions;
using CommonMaxTools.Editor.Extensions;

namespace CommonMaxTools.Editor.Attributes.Drawers
{
    [CustomPropertyDrawer(typeof(ConditionalFieldAttribute))]
    public class ConditionalFieldDrawer : PropertyDrawer
    {
        #region Fields

        private ConditionalFieldAttribute ConditionalAttribute => conditionalAttribute ?? (conditionalAttribute = attribute as ConditionalFieldAttribute);
        private ConditionalFieldAttribute conditionalAttribute;

        private bool isVisible;
        private SerializedProperty comparativeProperty;

        #endregion Fields

        #region Property Drawer Methods

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (!isVisible)
                return;

            EditorGUI.PropertyField(position, property, label, true);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            // Check out visibility once and here because this method is always invoking before OnGUI
            isVisible = ConditionalFieldUtility.IsVisible(GetComparativeProperty(property), ConditionalAttribute.inverse, conditionalAttribute.comparedValues);

            if (!isVisible)
                return 0f;

            return EditorGUI.GetPropertyHeight(property);
        }

        #endregion Property Drawer Methods

        #region Private Methods

        private SerializedProperty GetComparativeProperty(SerializedProperty property)
        {
            if (comparativeProperty == null)
                comparativeProperty = ConditionalFieldUtility.FindComparativeProperty(property, ConditionalAttribute.checkedFieldName);

            return comparativeProperty;
        }

        #endregion Private Methods

        #region Nested Types

        public static class ConditionalFieldUtility
        {
            public static bool IsVisible(SerializedProperty comparativeProperty, bool inverse, string[] comparedValues)
            {
                if (comparativeProperty == null)
                    return true;

                string propertyString = comparativeProperty.AsStringValue().ToUpper();

                if (!comparedValues.IsNullOrEmpty())
                {
                    bool matchAny = CompareWithValues(propertyString, comparedValues);
                    return matchAny ^ inverse;
                }

                bool valueAssigned = propertyString != "FALSE" && propertyString != "0" && propertyString != "NULL";
                return valueAssigned ^ inverse;
            }

            public static SerializedProperty FindComparativeProperty(SerializedProperty property, string findPropertyName)
            {
                if (property.depth == 0)
                    return property.serializedObject.FindProperty(findPropertyName);

                throw new ArgumentException("[ConditionalField] Attribute doesn't support processing condition for array or nested properties");
            }

            private static bool CompareWithValues(string value, string[] comparedValues)
            {
                foreach(var compareValue in comparedValues)
                {
                    if (compareValue == value)
                        return true;
                }

                return false;
            }
        }

        #endregion Nested Types
    }
}
