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
        private FoldoutHandler foldoutHandler;

        #endregion Fields

        #region MonoBehaviour

        private void OnEnable()
        {
            if (target != null)
            {
                buttonHandler = new ButtonMethodHandler(target);
                foldoutHandler = new FoldoutHandler(target, serializedObject);
            }
        }

        private void OnDisable()
        {
            foldoutHandler?.OnDisable();
        }

        public override void OnInspectorGUI()
        {
            if (foldoutHandler != null && foldoutHandler.Initialize())
                foldoutHandler.OnInspectorGUI();
            else
                base.OnInspectorGUI();

            buttonHandler?.DrawButtons(serializedObject);
        }

        #endregion MonoBehaviour
    }
}
