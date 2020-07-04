using System;

using UnityEngine;
using UnityEditor;

using CommonMaxTools.Attributes;
using CommonMaxTools.Editor.Utils;
using CommonMaxTools.Editor.Tools;

namespace CommonMaxTools.Editor.Attributes
{
    [InitializeOnLoad]
    internal static class AutoAssignHandler
    {
        static AutoAssignHandler()
        {
            EditorEventsProcessor.OnSaveScene += ProcessAssigningProperties;
        }

        #region Private Methods

        private static void ProcessAssigningProperties()
        {
            var componentFields = EditorUtils.GetComponentsByAttribute<AutoAssignAttribute>();

            foreach (var componentField in componentFields)
            {
                FillProperty(componentField);
            }
        }

        private static void FillProperty(EditorUtils.ComponentField componentField)
        {
            try
            {
                Type fieldType = componentField.Field.FieldType;
                var serializedObject = new SerializedObject(componentField.Component);
                var serializedProperty = serializedObject.FindProperty(componentField.Field.Name);

                if (fieldType.IsArray)
                {
                    Type elementType = fieldType.GetElementType();

                    if (!elementType.IsSubclassOf(typeof(Component)))
                        throw new AutoAssignException($"Type {elementType} isn't derevied from Component");

                    UnityEngine.Object[] components = componentField.Component.GetComponentsInChildren(elementType, true);

                    if (components == null || components.Length == 0)
                        throw new AutoAssignException($"Type {elementType} isn't found in hierarchy of related gameobjects");

                    serializedProperty.ReplaceArrayValues(components);
                }
                else
                {
                    if (!fieldType.IsSubclassOf(typeof(Component)))
                        throw new AutoAssignException($"Type {fieldType} isn't derevied from Component");

                    UnityEngine.Object component = componentField.Component.GetComponentInChildren(fieldType, true);

                    if (component == null)
                        throw new AutoAssignException($"Type {fieldType} isn't found in hierarchy of related gameobjects");

                    serializedProperty.objectReferenceValue = component;
                }

                serializedObject.ApplyModifiedProperties();
            }
            catch (AutoAssignException ex)
            {
                Debug.LogError(string.Format("{0} caused: {1} is failed to Auto Assign property. {2}",
                        componentField.Component.name, componentField.Field.Name, ex.Message), componentField.Component.gameObject);
            }
        }

        #endregion Private Methods
    }
}