using System;
using System.Collections.Generic;

using UnityEditor;

using CommonMaxTools.Attributes;
using CommonMaxTools.Editor.Utils;

namespace CommonMaxTools.Editor.Attributes.Handlers
{
    public class FoldoutHandler
    {
        private readonly UnityEngine.Object target;
        private readonly SerializedObject serializedObject;

        List<FoldoutGroup> foldoutGroups = new List<FoldoutGroup>();
        List<SerializedProperty> nonFoldProps = new List<SerializedProperty>();

        private bool initialized = false;
        private bool isFoldoutPresence = false;

        public FoldoutHandler(UnityEngine.Object target, SerializedObject serializedObject)
        {
            this.target = target;
            this.serializedObject = serializedObject;
        }

        #region Public Methods

        public void OnInspectorGUI()
        {
            Initialize();
            DrawHeader();
            DrawBody();

            serializedObject.ApplyModifiedProperties();
        }

        public void OnDisable()
        {
            if (target == null)
                return;

            foreach(var fold in foldoutGroups)
            {
                EditorPrefs.SetBool(string.Format("{0}{1}", fold.attribute.name, target.name), fold.expanded);
            }
        }

        public bool Initialize()
        {
            if (initialized)
                return isFoldoutPresence;

            foldoutGroups.Clear();
            nonFoldProps.Clear();

            var fields = ExtendedEditorUtils.GetFieldInfosOfComponent(target);
            FoldoutGroup currentFoldGroup = null;

            // collect fields of script
            foreach (var field in fields)
            {
                var foldoutAttr = Attribute.GetCustomAttribute(field, typeof(FoldoutAttribute)) as FoldoutAttribute;

                if (foldoutAttr == null)
                {
                    if (currentFoldGroup != null && currentFoldGroup.attribute.foldEverything)
                        currentFoldGroup.fieldNames.Add(field.Name);

                    continue;
                }

                var fold = foldoutGroups.Find(f => f.attribute.name == foldoutAttr.name);
                if (fold == null)
                {
                    fold = new FoldoutGroup(foldoutAttr);
                    fold.expanded = EditorPrefs.GetBool(string.Format("{0}{1}", fold.attribute.name, target.name), false);
                    foldoutGroups.Add(fold);
                }

                fold.fieldNames.Add(field.Name);
                currentFoldGroup = fold;
            }

            // collect serialized properties for each field of script
            var serializedProperty = serializedObject.GetIterator();
            var next = serializedProperty.NextVisible(true);

            if (next)
            {
                do
                {
                    if (!HandleFoldoutProperty(serializedProperty))
                    {
                        nonFoldProps.Add(serializedProperty.Copy());
                    }
                } 
                while (serializedProperty.NextVisible(false));
            }

            initialized = true;
            isFoldoutPresence = foldoutGroups.Count > 0;

            return isFoldoutPresence;
        }

        #endregion Public Methods

        #region Private Methods

        private bool HandleFoldoutProperty(SerializedProperty serializedProperty)
        {
            foreach (var fold in foldoutGroups)
            {
                if (fold.fieldNames.Contains(serializedProperty.name))
                {
                    fold.properties.Add(serializedProperty.Copy());
                    return true;
                }
            }

            return false;
        }

        private void DrawHeader()
        {
            using(new EditorGUI.DisabledScope(nonFoldProps[0].propertyPath == "m_Script"))
            {
                EditorGUILayout.PropertyField(nonFoldProps[0], true);
            }
        }

        private void DrawBody()
        {
            foreach(var foldoutGroup in foldoutGroups)
            {
                EditorGUILayout.BeginVertical(ExtendedStyles.Box);
                DrawFoldoutGroup(foldoutGroup);
                EditorGUILayout.EndVertical();

                EditorGUI.indentLevel = 0;
            }

            EditorGUILayout.Space();

            for(int i = 1; i < nonFoldProps.Count; i++)
            {
                EditorGUILayout.PropertyField(nonFoldProps[i], true);
            }

            EditorGUILayout.Space();
        }

        private void DrawFoldoutGroup(FoldoutGroup foldout)
        {
            foldout.expanded = EditorGUILayout.Foldout(foldout.expanded, foldout.attribute.name,
                true, ExtendedStyles.Foldout);

            if (foldout.expanded)
            {
                EditorGUI.indentLevel = 1;

                EditorGUILayout.BeginVertical(ExtendedStyles.BoxChild);

                foreach (var property in foldout.properties)
                {
                    EditorGUILayout.PropertyField(property, true);
                }

                EditorGUILayout.EndVertical();
            }
        }

        #endregion Private Methods

        #region Nested Types

        private class FoldoutGroup
        {
            public readonly FoldoutAttribute attribute;
            public readonly HashSet<string> fieldNames;
            public readonly List<SerializedProperty> properties;
            public bool expanded;

            public FoldoutGroup(FoldoutAttribute attribute)
            {
                this.attribute = attribute;
                fieldNames = new HashSet<string>();
                properties = new List<SerializedProperty>();
            }
        }

        #endregion Nested Types
    }
}
