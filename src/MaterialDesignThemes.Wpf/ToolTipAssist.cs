namespace MaterialDesignThemes.Wpf;

/// <summary>
/// Provides helper functionality for customizing ToolTip placement behavior.
/// </summary>
public static class ToolTipAssist
{
    /// <summary>
    /// Gets the custom placement callback used to position ToolTips.
    /// </summary>
    public static CustomPopupPlacementCallback CustomPopupPlacementCallback => CustomPopupPlacementCallbackImpl;

    public static CustomPopupPlacement[] CustomPopupPlacementCallbackImpl(Size popupSize, Size targetSize, Point offset)
    {
        return new[]
        {
            new CustomPopupPlacement(new Point(targetSize.Width/2 - popupSize.Width/2, targetSize.Height + 14), PopupPrimaryAxis.Horizontal)
        };
    }
}
