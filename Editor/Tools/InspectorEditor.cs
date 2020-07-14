using UnityEditor;

using CommonMaxTools.Editor.Attributes.Handlers;

namespace CommonMaxTools.Editor.Tools
{
    /// <summary>
    /// Custom editor that is responsible for drawing a special UI elements in Inspector.
    /// For example, buttons that is invoking related methods
    /// </summary>
    [CustomEditor(typeof(UnityEngine.Object), true), CanEditMultipleObjects]
    public class InspectorEditor : UnityEditor.Editor
    {
        #region Fields

        private ButtonMethodHandler buttonHandler;

        #endregion Fields

        #region MonoBehaviour

        private void OnEnable()
        {
            buttonHandler = new ButtonMethodHandler(target, serializedObject);
        }

        private void OnDisable()
        {
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            buttonHandler.DrawButtons();
        }

        #endregion MonoBehaviour
    }
}
