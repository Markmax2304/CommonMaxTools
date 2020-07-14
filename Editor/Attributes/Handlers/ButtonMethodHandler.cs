using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

using UnityEditor;
using UnityEngine;

using CommonMaxTools.Attributes;
using CommonMaxTools.Extensions;
using CommonMaxTools.Editor.Utils;

namespace CommonMaxTools.Editor.Attributes.Handlers
{
    public class ButtonMethodHandler
    {
        #region Fields

        private readonly UnityEngine.Object target;
        private readonly SerializedObject serializedObject;

        private readonly IEnumerable<MethodInfo> methods;

        #endregion Fields

        #region Constructors

        public ButtonMethodHandler(UnityEngine.Object target, SerializedObject serializedObject)
        {
            this.target = target;
            this.serializedObject = serializedObject;

            methods = EditorUtils.GetMethodsInfoByAttributes<ButtonMethodAttribute>(target).ToList();
        }

        #endregion Constructors

        #region Public Methods

        public void DrawButtons()
        {
            if (methods.IsNullOrEmpty())
                return;

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Button Methods", GetHeaderGUIStyle());

            foreach (var method in methods)
            {
                ExtendedEditorGUI.ButtonHandle(serializedObject.targetObject, method);
            }
        }

        public float CalculateButtonsHeight()
        {
            float height = 0f;

            if (methods.IsNullOrEmpty())
                return height;

            foreach (var method in methods)
                height += EditorGUIUtility.singleLineHeight;

            return height + EditorGUIUtility.singleLineHeight * 2;
        }

        #endregion Public Methods

        #region Private Methods

        private static GUIStyle GetHeaderGUIStyle()
        {
            GUIStyle style = new GUIStyle()
            {
                fontStyle = FontStyle.BoldAndItalic,
                alignment = TextAnchor.UpperCenter
            };

            return style;
        }

        #endregion Private Methods
    }
}
