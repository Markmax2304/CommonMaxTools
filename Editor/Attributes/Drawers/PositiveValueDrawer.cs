using System;

using UnityEngine;
using UnityEditor;

using CommonMaxTools.Attributes;
using CommonMaxTools.Editor.Extensions;

namespace CommonMaxTools.Editor.Attributes.Drawers
{
    [CustomPropertyDrawer(typeof(PositiveValueAttribute))]
    public class PositiveValueDrawer : PropertyDrawer
    {
        private PositiveValueAttribute positiveValueAttribute => attribute as PositiveValueAttribute;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.IsNumerical())
            {
                if (HandleValuesToPositive(property))
                    property.serializedObject.ApplyModifiedProperties();
            }
            else
            {
                throw new Exception($"PositiveValue attribute is attached to non-numerical variable. Type of variable is {property.propertyType}");
            }

            EditorGUI.PropertyField(position, property, label, true);
        }

        #region Private Methods

        private bool HandleValuesToPositive(SerializedProperty property)
        {
            switch (property.propertyType)
            {
                case SerializedPropertyType.Integer:
                    return HandleInteger(property);
                case SerializedPropertyType.Float:
                    return HandleFloat(property);
                case SerializedPropertyType.Vector2:
                    return HandleVector2(property);
                case SerializedPropertyType.Vector3:
                    return HandleVector3(property);
                case SerializedPropertyType.Vector4:
                    return HandleVector4(property);
                case SerializedPropertyType.Vector2Int:
                    return HandleVector2Int(property);
                case SerializedPropertyType.Vector3Int:
                    return HandleVector3Int(property);
                default:
                    return false;
            }
        }

        private bool HandleInteger(SerializedProperty property)
        {
            int value = property.intValue;
            bool isNegativeValue = ConvertToPositive(ref value);

            if (isNegativeValue)
                property.intValue = value;

            return isNegativeValue;
        }

        private bool HandleFloat(SerializedProperty property)
        {
            float value = property.floatValue;
            bool isNegativeValue = ConvertToPositive(ref value);

            if (isNegativeValue)
                property.floatValue = value;

            return isNegativeValue;
        }

        private bool HandleVector2(SerializedProperty property)
        {
            Vector2 vector = property.vector2Value;

            bool result = ConvertToPositive(ref vector.x);
            result |= ConvertToPositive(ref vector.y);

            property.vector2Value = vector;
            return result;
        }

        private bool HandleVector3(SerializedProperty property)
        {
            Vector3 vector = property.vector3Value;

            bool result = ConvertToPositive(ref vector.x);
            result |= ConvertToPositive(ref vector.y);
            result |= ConvertToPositive(ref vector.z);

            property.vector3Value = vector;
            return result;
        }

        private bool HandleVector4(SerializedProperty property)
        {
            Vector4 vector = property.vector4Value;

            bool result = ConvertToPositive(ref vector.x);
            result |= ConvertToPositive(ref vector.y);
            result |= ConvertToPositive(ref vector.z);
            result |= ConvertToPositive(ref vector.w);

            property.vector4Value = vector;
            return result;
        }

        private bool HandleVector2Int(SerializedProperty property)
        {
            Vector2Int vector = property.vector2IntValue;
            int x = vector.x;
            int y = vector.y;

            bool result = ConvertToPositive(ref x);
            result |= ConvertToPositive(ref y);

            property.vector2IntValue = new Vector2Int(x, y);
            return result;
        }

        private bool HandleVector3Int(SerializedProperty property)
        {
            Vector3Int vector = property.vector3IntValue;
            int x = vector.x;
            int y = vector.y;
            int z = vector.z;

            bool result = ConvertToPositive(ref x);
            result |= ConvertToPositive(ref y);
            result |= ConvertToPositive(ref z);

            property.vector3IntValue = new Vector3Int(x, y, z);
            return result;
        }

        private bool ConvertToPositive(ref int value)
        {
            bool isNegativeValue = positiveValueAttribute.inclusiveZero ? value < 0 : value <= 0;

            if (isNegativeValue)
                value = positiveValueAttribute.inclusiveZero ? 0 : 1;

            return isNegativeValue;
        }

        private bool ConvertToPositive(ref float value)
        {
            bool isNegativeValue = positiveValueAttribute.inclusiveZero ? value < 0 : value <= 0;

            if (isNegativeValue)
                value = positiveValueAttribute.inclusiveZero ? 0f : 0.01f;

            return isNegativeValue;
        }

        #endregion Private Methods
    }
}
