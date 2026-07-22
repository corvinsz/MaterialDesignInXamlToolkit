namespace MaterialDesignThemes.Wpf;

/// <summary>
/// Provides attached properties for configuring navigation drawer appearance and icons.
/// </summary>
public static class NavigationDrawerAssist
{
    private static readonly CornerRadius DefaultCornerRadius = new CornerRadius(2.0);

    #region CornerRadius
    /// <summary>
    /// Controls the corner radius of the selection box.
    /// </summary>
    public static readonly DependencyProperty CornerRadiusProperty
        = DependencyProperty.RegisterAttached("CornerRadius", typeof(CornerRadius), typeof(NavigationDrawerAssist), new PropertyMetadata(DefaultCornerRadius));

    public static CornerRadius GetCornerRadius(DependencyObject element)
        => (CornerRadius)element.GetValue(CornerRadiusProperty);
    public static void SetCornerRadius(DependencyObject element, CornerRadius value) => element.SetValue(CornerRadiusProperty, value);
    #endregion

    #region UnselectedIcon
    /// <summary>
    /// Defines the icon displayed when the item is not selected.
    /// </summary>
    public static readonly DependencyProperty UnselectedIconProperty =
        DependencyProperty.RegisterAttached("UnselectedIcon", typeof(PackIconKind), typeof(NavigationDrawerAssist), new PropertyMetadata(PackIconKind.None));
    public static PackIconKind GetUnselectedIcon(DependencyObject element)
        => (PackIconKind)element.GetValue(UnselectedIconProperty);
    public static void SetUnselectedIcon(DependencyObject element, PackIconKind value)
        => element.SetValue(UnselectedIconProperty, value);
    #endregion

    #region SelectedIcon
    /// <summary>
    /// Defines the icon displayed when the item is selected.
    /// </summary>
    public static readonly DependencyProperty SelectedIconProperty =
        DependencyProperty.RegisterAttached("SelectedIcon", typeof(PackIconKind), typeof(NavigationDrawerAssist), new PropertyMetadata(PackIconKind.None));
    public static PackIconKind GetSelectedIcon(DependencyObject element)
        => (PackIconKind)element.GetValue(SelectedIconProperty);
    public static void SetSelectedIcon(DependencyObject element, PackIconKind value)
        => element.SetValue(SelectedIconProperty, value);
    #endregion

    #region IconSize
    /// <summary>
    /// Defines the size of the navigation drawer icons.
    /// </summary>
    public static readonly DependencyProperty IconSizeProperty =
        DependencyProperty.RegisterAttached("IconSize", typeof(int), typeof(NavigationDrawerAssist), new PropertyMetadata(24));
    public static int GetIconSize(DependencyObject element)
        => (int)element.GetValue(IconSizeProperty);
    public static void SetIconSize(DependencyObject element, int value)
        => element.SetValue(IconSizeProperty, value);
    #endregion
}
