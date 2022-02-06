using UnityEngine;

namespace CommonMaxTools.Editor.UIElements
{
    /// <summary>
    /// interface serves to keep rect location for drawable entities on GUI Editor
    /// </summary>
    public interface IPlaceHolder
    {
        Rect Location { get; set; }
        bool IsPositionChanged { get; }

        void Update();
    }
}
