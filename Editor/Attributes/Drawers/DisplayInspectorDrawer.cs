using System;
using System.Linq;
using System.Collections.Generic;

using UnityEditor;
using UnityEngine;

using CommonMaxTools.Attributes;
using CommonMaxTools.Editor.Utils;
using CommonMaxTools.Editor.Attributes.Handlers;

namespace CommonMaxTools.Editor.Attributes.Drawers
{
    [CustomPropertyDrawer(typeof(DisplayInspectorAttribute))]
    public class DisplayInspectorDrawer : PropertyDrawer
    {
        #region Constants

        private const float interval = 4f;
        private const float border = 2f;
        private static readonly Color scriptableObjectColor = new Color(.5f, .9f, .5f, .5f);
        private static readonly Color monobehaviourColor = new Color(.9f, .5f, .5f, .5f);
        private static readonly Color defaultColor = new Color(.5f, .5f, .9f, .5f);

        #endregion Constants

        #region Fields

        private DisplayInspectorAttribute displainInspector;
        private DisplayInspectorAttribute Attribute => displainInspector ?? (displainInspector = attribute as DisplayInspectorAttribute);

        private List<PropertyPathWrapper> pathWrappers = new List<PropertyPathWrapper>();
        private ButtonMethodHandler buttonHandler;

        private SerializedObject serializedObject;
        private Rect generalRect;

        #endregion Fields

        #region Property Drawer Methods

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            position.height = EditorGUI.GetPropertyHeight(property);
            EditorGUI.PropertyField(position, property, label);
            position.y += EditorGUI.GetPropertyHeight(property) + interval;

            generalRect = position;

            if (property.objectReferenceValue == null)
                return;

            if (buttonHandler == null)
                buttonHandler = new ButtonMethodHandler(property.objectReferenceValue);

            float startY = position.y;
            serializedObject = new SerializedObject(property.objectReferenceValue);

            CollectProperties();
            ProcessPropertyRects(pathWrappers);

            generalRect.y += buttonHandler.CalculateButtonsHeight();

            DrawBorderBox(startY, property.objectReferenceValue.GetType());
            DrawProperties(pathWrappers);
            buttonHandler.DrawButtons(serializedObject);

            if (GUI.changed)
                serializedObject.ApplyModifiedProperties();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (property.objectReferenceValue == null)
                return base.GetPropertyHeight(property, label);

            float height = EditorGUI.GetPropertyHeight(property) + interval;
            serializedObject = new SerializedObject(property.objectReferenceValue);

            if (buttonHandler == null)
                buttonHandler = new ButtonMethodHandler(property.objectReferenceValue);

            CollectProperties();
            ProcessPropertyRects(pathWrappers);

            height += CalculateEntireHeight(pathWrappers);

            return height;
        }

        #endregion Property Drawer Methods

        #region Private Methods

        private void CollectProperties()
        {
            pathWrappers.Clear();

            var propertyObject = serializedObject.GetIterator();
            propertyObject.Next(true);
            propertyObject.NextVisible(false);

            while (propertyObject.NextVisible(propertyObject.isExpanded))
            {
                if (pathWrappers.Count == 0 || !pathWrappers.Last().TryAddChild(propertyObject.propertyPath))
                {
                    pathWrappers.Add(new PropertyPathWrapper(propertyObject.propertyPath));
                }
            }
        }

        private void ProcessPropertyRects(IEnumerable<PropertyPathWrapper> wrappers)
        {
            foreach (var pathWrapper in wrappers)
            {
                var propertyObject = serializedObject.FindProperty(pathWrapper.path);

                Rect fieldPosition = generalRect;

                int depth = propertyObject.depth + 1;
                fieldPosition.x = generalRect.x + 10f * depth + border;
                fieldPosition.width = generalRect.width - 10f * depth - border * 2;
                fieldPosition.height = CalculateHeight(pathWrapper);

                pathWrapper.rect = fieldPosition;
                generalRect.y += fieldPosition.height + interval;

                if (pathWrapper.HasChildren)
                    ProcessPropertyRects(pathWrapper.children);
            }
        }

        private float CalculateHeight(PropertyPathWrapper pathWrapper)
        {
            SerializedProperty sp = serializedObject.FindProperty(pathWrapper.path);
            float height = EditorGUI.GetPropertyHeight(sp);

            if (!pathWrapper.HasChildren)
                return height;

            foreach(var child in pathWrapper.children)
            {
                height -= EditorGUI.GetPropertyHeight(serializedObject.FindProperty(child.path));
            }

            return height;
        }

        private void DrawProperties(IEnumerable<PropertyPathWrapper> wrappers)
        {
            foreach (var pathWrapper in wrappers)
            {
                var propertyObject = serializedObject.FindProperty(pathWrapper.path);
                EditorGUI.PropertyField(pathWrapper.rect, propertyObject);

                if (pathWrapper.HasChildren)
                    DrawProperties(pathWrapper.children);
            }
        }

        private float CalculateEntireHeight(IEnumerable<PropertyPathWrapper> wrappers)
        {
            float height = 0;

            foreach(var pathWrapper in wrappers)
            {
                height += CalculateHeight(pathWrapper) + interval;

                if (pathWrapper.HasChildren)
                    height += CalculateEntireHeight(pathWrapper.children);
            }

            return height;
        }

        private void DrawBorderBox(float startY, Type targetType)
        {
            var bgRect = generalRect;
            bgRect.y = startY - border;
            bgRect.height = CalculateEntireHeight(pathWrappers) + border * 2 - interval;
            bgRect.height += buttonHandler.CalculateButtonsHeight();

            var bgColor = Attribute.IsColorValid ? new Color(Attribute.red, Attribute.green, Attribute.blue, .5f) :
                targetType.IsSubclassOf(typeof(MonoBehaviour)) ? monobehaviourColor : 
                targetType.IsSubclassOf(typeof(ScriptableObject)) ? scriptableObjectColor : defaultColor;

            ExtendedEditorGUI.DrawColorBox(bgRect, bgColor);
        }

        #endregion Private Methods

        #region Nested Types

        private class PropertyPathWrapper
        {
            public string path;
            public Rect rect;
            public List<PropertyPathWrapper> children;

            public bool HasChildren => children != null && children.Count > 0;

            public PropertyPathWrapper(string path)
            {
                this.path = path;
            }

            public bool TryAddChild(string path)
            {
                if (!path.StartsWith(this.path))
                    return false;

                if (children == null)
                    children = new List<PropertyPathWrapper>();

                if (!TryAddChildIntoChildren(path))
                    children.Add(new PropertyPathWrapper(path));

                return true;
            }

            private bool TryAddChildIntoChildren(string path)
            {
                foreach(var child in children)
                {
                    if (child.TryAddChild(path))
                        return true;
                }

                return false;
            }
        }

        #endregion Nested Types
    }
}
