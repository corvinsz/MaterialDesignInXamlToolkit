using System.Windows.Media;

namespace MaterialDesignThemes.Wpf;

/// <summary>
/// Provides attached properties for customizing the appearance of <see cref="MenuItem"/> controls.
/// </summary>
public static class MenuItemAssist
{
    #region AttachedProperty : HighlightedBackgroundProperty

    /// <summary>
    /// Defines the background brush applied when a menu item is highlighted.
    /// </summary>
    public static readonly DependencyProperty HighlightedBackgroundProperty =
        DependencyProperty.RegisterAttached(
            "HighlightedBackground",
            typeof(Brush),
            typeof(MenuItemAssist),
            new PropertyMetadata(null));

    public static Brush? GetHighlightedBackground(DependencyObject obj)
        => (Brush?)obj.GetValue(HighlightedBackgroundProperty);

    public static void SetHighlightedBackground(DependencyObject obj, Brush? value)
        => obj.SetValue(HighlightedBackgroundProperty, value);

    #endregion
}
