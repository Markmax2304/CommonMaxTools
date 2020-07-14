using UnityEngine;
using UnityEditor;

namespace CommonMaxTools.Editor.Utils
{
    public static class EditorExtenstions
    {
        /// <summary>
        /// Replace array values in serialized property with new array
        /// </summary>
        public static void ReplaceArrayValues(this SerializedProperty property, Object[] newValues)
        {
            if (property == null || !property.isArray)
            {
                Debug.LogWarning("Failed to replace array values for serialized property.");
                return;
            }

            property.arraySize = 0;
            property.serializedObject.ApplyModifiedProperties();
            property.arraySize = newValues.Length;
            for(int i = 0; i < newValues.Length; i++)
            {
                property.GetArrayElementAtIndex(i).objectReferenceValue = newValues[i];
            }
            property.serializedObject.ApplyModifiedProperties();
        }
    }
}
