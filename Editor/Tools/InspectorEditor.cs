using System.Collections.Generic;
using System.Reflection;
using System.Linq;

using UnityEngine;
using UnityEditor;

using CommonMaxTools.Editor.Utils;
using CommonMaxTools.Attributes;

namespace CommonMaxTools.Editor.Tools
{
    /// <summary>
    /// Custom editor that is responsible for drawing a special UI elements in Inspector.
    /// For example, buttons that is invoking related methods
    /// </summary>
    [CustomEditor(typeof(UnityEngine.Object), true)]
    public class InspectorEditor : UnityEditor.Editor
    {
        #region Fields

        private IEnumerable<MethodInfo> methods;

        #endregion Fields

        #region MonoBehaviour

        private void OnEnable()
        {
            methods = EditorUtils.GetMethodsInfoByAttributes<ButtonMethodAttribute>(target);
        }

        private void OnDisable()
        {
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            DrawButtons();
        }

        #endregion MonoBehaviour

        #region Private Methods

        private void DrawButtons()
        {
            if (methods == null || methods.Count() == 0)
                return;

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Button Methods", GetHeaderGUIStyle());

            foreach (var method in methods)
            {
                ExtendedEditorGUI.ButtonHandle(serializedObject.targetObject, method);
            }
        }

        private static GUIStyle GetHeaderGUIStyle()
        {
            GUIStyle style = new GUIStyle(EditorStyles.centeredGreyMiniLabel);
            style.fontStyle = FontStyle.Bold;
            style.alignment = TextAnchor.UpperCenter;

            return style;
        }

        #endregion Private Methods
    }
}
