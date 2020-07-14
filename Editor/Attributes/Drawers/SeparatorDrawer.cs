using System;

using UnityEngine;
using UnityEditor;

using CommonMaxTools.Attributes;
using CommonMaxTools.Editor.Utils;

namespace CommonMaxTools.Editor.Attributes.Drawers
{
    [CustomPropertyDrawer(typeof(SeparatorAttribute))]
    public class SeparatorDrawer : DecoratorDrawer
    {
        private static readonly Color separatorColor = new Color(0f, 0f, 0f, .5f);

        private SeparatorAttribute separator;
        private SeparatorAttribute Separator => separator ?? (separator = attribute as SeparatorAttribute);

        public override void OnGUI(Rect position)
        {
            position.y += 18;
            GUIStyle fontStyle = new GUIStyle() { fontStyle = FontStyle.Bold };

            if (String.IsNullOrWhiteSpace(Separator.title))
            {
                position.height = 1;
                ExtendedEditorGUI.DrawColorBox(position, separatorColor);
            }
            else
            {
                Vector2 textSize = GUI.skin.label.CalcSize(new GUIContent(Separator.title));
                float sideWidth = (position.width - textSize.x) / 2.0f - 5.0f;

                ExtendedEditorGUI.DrawColorBox(new Rect(position.xMin, position.yMin, sideWidth, 1), separatorColor);
                GUI.Label(new Rect(position.xMin + sideWidth + 5.0f, position.yMin - 8.0f, textSize.x, 20), Separator.title, fontStyle);
                ExtendedEditorGUI.DrawColorBox(new Rect(position.xMin + sideWidth + 10.0f + textSize.x, position.yMin, sideWidth, 1), separatorColor);
            }
        }

        public override float GetHeight()
        {
            return 36;
        }
    }
}
