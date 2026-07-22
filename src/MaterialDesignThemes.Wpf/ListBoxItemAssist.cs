using System.Windows.Media;

namespace MaterialDesignThemes.Wpf;

/// <summary>
/// Provides attached properties for customizing the appearance and behavior of <see cref="ListBoxItem"/> controls.
/// </summary>
public static class ListBoxItemAssist
{

    private static readonly CornerRadius DefaultCornerRadius = new(2.0);

    #region AttachedProperty : CornerRadiusProperty
    /// <summary>
    /// Controls the corner radius of the selection box.
    /// </summary>
    public static readonly DependencyProperty CornerRadiusProperty
        = DependencyProperty.RegisterAttached("CornerRadius", typeof(CornerRadius), typeof(ListBoxItemAssist), new PropertyMetadata(DefaultCornerRadius));

    public static CornerRadius GetCornerRadius(DependencyObject element)
        => (CornerRadius)element.GetValue(CornerRadiusProperty);
    public static void SetCornerRadius(DependencyObject element, CornerRadius value) => element.SetValue(CornerRadiusProperty, value);
    #endregion

    #region HoverBackground
    /// <summary>
    /// Gets or sets the background brush applied when the <see cref="ListBoxItem"/> is hovered.
    /// </summary>
    public static readonly DependencyProperty HoverBackgroundProperty =
        DependencyProperty.RegisterAttached("HoverBackground", typeof(Brush), typeof(ListBoxItemAssist), new PropertyMetadata(null));
    public static Brush? GetHoverBackground(DependencyObject obj)
        => (Brush?)obj.GetValue(HoverBackgroundProperty);

    public static void SetHoverBackground(DependencyObject obj, Brush? value)
        => obj.SetValue(HoverBackgroundProperty, value);

    #endregion HoverBackground

    #region SelectedFocusedBackground
    /// <summary>
    /// Gets or sets the background brush applied when the <see cref="ListBoxItem"/> is selected and has keyboard focus.
    /// </summary>
    public static readonly DependencyProperty SelectedFocusedBackgroundProperty =
        DependencyProperty.RegisterAttached("SelectedFocusedBackground", typeof(Brush), typeof(ListBoxItemAssist), new PropertyMetadata(null));
    public static Brush? GetSelectedFocusedBackground(DependencyObject obj)
        => (Brush?)obj.GetValue(SelectedFocusedBackgroundProperty);

    public static void SetSelectedFocusedBackground(DependencyObject obj, Brush? value)
        => obj.SetValue(SelectedFocusedBackgroundProperty, value);

    #endregion SelectedFocusedBackground

    #region SelectedUnfocusedBackground
    /// <summary>
    /// Gets or sets the background brush applied when the <see cref="ListBoxItem"/> is selected but does not have keyboard focus.
    /// </summary>
    public static readonly DependencyProperty SelectedUnfocusedBackgroundProperty =
        DependencyProperty.RegisterAttached("SelectedUnfocusedBackground", typeof(Brush), typeof(ListBoxItemAssist), new PropertyMetadata(null));
    public static Brush? GetSelectedUnfocusedBackground(DependencyObject obj)
        => (Brush?)obj.GetValue(SelectedUnfocusedBackgroundProperty);

    public static void SetSelectedUnfocusedBackground(DependencyObject obj, Brush? value)
        => obj.SetValue(SelectedUnfocusedBackgroundProperty, value);

    #endregion SelectedFocusedBackground

    #region ShowSelection
    /// <summary>
    /// Gets or sets a value indicating whether the selection background is displayed for the <see cref="ListBoxItem"/>.
    /// </summary>
    public static readonly DependencyProperty ShowSelectionProperty =
        DependencyProperty.RegisterAttached("ShowSelection", typeof(bool), typeof(ListBoxItemAssist), new PropertyMetadata(true));
    public static bool GetShowSelection(DependencyObject element)
        => (bool)element.GetValue(ShowSelectionProperty);
    public static void SetShowSelection(DependencyObject element, bool value)
        => element.SetValue(ShowSelectionProperty, value);

    #endregion

    #region Cursor
    /// <summary>
    /// Gets or sets the mouse cursor displayed when the pointer is over the <see cref="ListBoxItem"/>.
    /// </summary>
    public static readonly DependencyProperty CursorProperty =
        DependencyProperty.RegisterAttached("Cursor", typeof(Cursor), typeof(ListBoxItemAssist), new PropertyMetadata(Cursors.Hand));
    public static Cursor GetCursor(DependencyObject obj)
        => (Cursor)obj.GetValue(CursorProperty);

    public static void SetCursor(DependencyObject obj, Cursor value)
        => obj.SetValue(CursorProperty, value);

    #endregion
}
