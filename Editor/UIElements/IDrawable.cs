
namespace CommonMaxTools.Editor.UIElements
{
    /// <summary>
    /// interface that serves as marker for any drawable entities on Editor GUI. For example, custom button, label and etc.
    /// </summary>
    public interface IDrawable
    {
        IPlaceHolder PlaceHolder { get; }

        void OnGUI();
    }
}
