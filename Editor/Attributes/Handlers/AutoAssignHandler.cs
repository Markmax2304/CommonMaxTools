using System;
using System.Linq;
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;

using CommonMaxTools.Attributes;
using CommonMaxTools.Extensions;
using CommonMaxTools.Editor.Utils;

namespace CommonMaxTools.Editor.Tools.Handlers
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
            var componentFields = ExtendedEditorUtils.GetComponentsByAttribute<AutoAssignAttribute>();

            foreach (var componentField in componentFields)
            {
                FillProperty(componentField);
            }
        }

        private static void FillProperty(ExtendedEditorUtils.ComponentField componentField)
        {
            try
            {
                var attribute = Attribute.GetCustomAttribute(componentField.Field, typeof(AutoAssignAttribute), true) as AutoAssignAttribute;

                Type fieldType = componentField.Field.FieldType;
                var serializedObject = new SerializedObject(componentField.Component);
                var serializedProperty = serializedObject.FindProperty(componentField.Field.Name);

                if (fieldType.IsArray)
                {
                    Type elementType = fieldType.GetElementType();

                    if (!elementType.IsSubclassOf(typeof(Component)))
                        throw new AutoAssignException($"Type {elementType} isn't derevied from Component");

                    List<Component> components;
                    if (attribute.deepLevel == -1)
                    {
                        components = componentField.Component.GetComponentsInChildren(elementType, true).ToList();
                    }
                    else
                    {
                        components = new List<Component>();
                        componentField.Component.GetComponentsInChildren(elementType, true, attribute.deepLevel, components);
                    }

                    if (components == null || components.Count == 0)
                        throw new AutoAssignException($"Type {elementType} isn't found in hierarchy of related gameobjects");

                    serializedProperty.ReplaceArrayValues(components.ToArray());
                }
                else
                {
                    if (!fieldType.IsSubclassOf(typeof(Component)))
                        throw new AutoAssignException($"Type {fieldType} isn't derevied from Component");

                    Component component = attribute.deepLevel == -1 ?
                        componentField.Component.GetComponentInChildren(fieldType, true) :
                        componentField.Component.GetComponentInChildren(fieldType, true, attribute.deepLevel);

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