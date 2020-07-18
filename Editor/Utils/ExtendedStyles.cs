using UnityEngine;
using UnityEditor;

namespace CommonMaxTools.Editor.Utils
{
    internal static class ExtendedStyles
    {
        public static readonly GUIStyle Foldout;
        public static readonly GUIStyle Box;
        public static readonly GUIStyle BoxChild;

        static ExtendedStyles()
        {
            var uiTex_in = Resources.Load<Texture2D>("IN foldout focus-6510");
            var uiTex_in_on = Resources.Load<Texture2D>("IN foldout focus on-5718");

            Color color_on = EditorGUIUtility.isProSkin ? Color.white : new Color(.2f, .4f, .8f, .5f);

            Foldout = new GUIStyle(EditorStyles.foldout);
            InitializeFoldoutStyle(color_on, uiTex_in, uiTex_in_on);

            Box = new GUIStyle(GUI.skin.box);
            Box.padding = new RectOffset(15, 0, 0, 0);

            BoxChild = new GUIStyle(GUI.skin.box);
            InitializeBoxChildStyle(color_on, uiTex_in, uiTex_in_on);

            // set white texture and white text color for dark theme of editor
            EditorStyles.foldout.active.textColor = color_on;
            EditorStyles.foldout.active.background = uiTex_in;
            EditorStyles.foldout.onActive.textColor = color_on;
            EditorStyles.foldout.onActive.background = uiTex_in_on;

            EditorStyles.foldout.focused.textColor = color_on;
            EditorStyles.foldout.focused.background = uiTex_in;
            EditorStyles.foldout.onFocused.textColor = color_on;
            EditorStyles.foldout.onFocused.background = uiTex_in_on;

            EditorStyles.foldout.hover.textColor = color_on;
            EditorStyles.foldout.hover.background = uiTex_in;

            EditorStyles.foldout.onHover.textColor = color_on;
            EditorStyles.foldout.onHover.background = uiTex_in_on;
        }

        #region Private Methods

        private static void InitializeFoldoutStyle(Color color_on, Texture2D uiTex_in, Texture2D uiTex_in_on)
        {
            Foldout.fontStyle = FontStyle.Bold;

            Foldout.active.textColor = color_on;
            Foldout.active.background = uiTex_in;
            Foldout.onActive.textColor = color_on;
            Foldout.onActive.background = uiTex_in_on;

            Foldout.focused.textColor = color_on;
            Foldout.focused.background = uiTex_in;
            Foldout.onFocused.textColor = color_on;
            Foldout.onFocused.background = uiTex_in_on;

            Foldout.hover.textColor = color_on;
            Foldout.hover.background = uiTex_in;
            Foldout.onHover.textColor = color_on;
            Foldout.onHover.background = uiTex_in_on;
        }

        private static void InitializeBoxChildStyle(Color color_on, Texture2D uiTex_in, Texture2D uiTex_in_on)
        {
            BoxChild.active.textColor = color_on;
            BoxChild.active.background = uiTex_in;
            BoxChild.onActive.textColor = color_on;
            BoxChild.onActive.background = uiTex_in_on;

            BoxChild.focused.textColor = color_on;
            BoxChild.focused.background = uiTex_in;
            BoxChild.onFocused.textColor = color_on;
            BoxChild.onFocused.background = uiTex_in_on;
        }

        #endregion Private Methods
    }
}
