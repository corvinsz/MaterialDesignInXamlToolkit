namespace MaterialDesignThemes.Wpf;

/// <summary>
/// Provides attached properties for configuring navigation rail appearance, content, and selection behavior.
/// </summary>
public static class NavigationRailAssist
{
    private static readonly CornerRadius DefaultCornerRadius = new(2.0);

    #region CornerRadius
    /// <summary>
    /// Controls the corner radius of the selection box.
    /// </summary>
    public static readonly DependencyProperty CornerRadiusProperty
        = DependencyProperty.RegisterAttached("CornerRadius", typeof(CornerRadius), typeof(NavigationRailAssist), new PropertyMetadata(DefaultCornerRadius));

    public static CornerRadius GetCornerRadius(DependencyObject element)
        => (CornerRadius)element.GetValue(CornerRadiusProperty);
    public static void SetCornerRadius(DependencyObject element, CornerRadius value) => element.SetValue(CornerRadiusProperty, value);
    #endregion

    #region Property FloatingContent

    /// <summary>
    /// Defines optional floating content displayed on the navigation rail.
    /// </summary>
    public static readonly DependencyProperty FloatingContentProperty = DependencyProperty.RegisterAttached(
        "FloatingContent", typeof(object), typeof(NavigationRailAssist), new PropertyMetadata(null));

    public static object GetFloatingContent(DependencyObject element) => (object)element.GetValue(FloatingContentProperty);
    public static void SetFloatingContent(DependencyObject element, object value) => element.SetValue(FloatingContentProperty, value);

    #endregion

    #region Property AdditionalEndContent
    /// <summary>
    /// Defines additional content displayed at the end of the navigation rail.
    /// </summary>
    public static readonly DependencyProperty AdditionalEndContentProperty = DependencyProperty.RegisterAttached(
        "AdditionalEndContent", typeof(object), typeof(NavigationRailAssist), new PropertyMetadata(null));
    public static object GetAdditionalEndContent(DependencyObject obj) => (object)obj.GetValue(AdditionalEndContentProperty);
    public static void SetAdditionalEndContent(DependencyObject obj, object value) => obj.SetValue(AdditionalEndContentProperty, value);
    #endregion

    #region Property ShowSelectionBackground

    /// <summary>
    /// Determines whether the selection background is displayed.
    /// </summary>
    public static readonly DependencyProperty ShowSelectionBackgroundProperty = DependencyProperty.RegisterAttached(
        "ShowSelectionBackground", typeof(bool), typeof(NavigationRailAssist), new PropertyMetadata(false));

    public static object GetShowSelectionBackground(DependencyObject element) => (bool)element.GetValue(ShowSelectionBackgroundProperty);
    public static void SetShowSelectionBackground(DependencyObject element, bool value) => element.SetValue(ShowSelectionBackgroundProperty, value);

    #endregion

    #region Property SelectionCornerRadius

    /// <summary>
    /// Defines the corner radius of the selection indicator.
    /// </summary>
    public static readonly DependencyProperty SelectionCornerRadiusProperty = DependencyProperty.RegisterAttached(
        "SelectionCornerRadius", typeof(CornerRadius), typeof(NavigationRailAssist), new PropertyMetadata(default(CornerRadius)));

    public static object GetSelectionCornerRadius(DependencyObject element) => (CornerRadius)element.GetValue(SelectionCornerRadiusProperty);
    public static void SetSelectionCornerRadius(DependencyObject element, CornerRadius value) => element.SetValue(SelectionCornerRadiusProperty, value);

    #endregion

    #region SelectionHeight
    /// <summary>
    /// Defines the height of the selection indicator.
    /// </summary>
    public static readonly DependencyProperty SelectionHeightProperty =
        DependencyProperty.RegisterAttached("SelectionHeight", typeof(int), typeof(NavigationRailAssist), new PropertyMetadata(default(int)));
    public static int GetSelectionHeight(DependencyObject element)
        => (int)element.GetValue(SelectionHeightProperty);
    public static void SetSelectionHeight(DependencyObject element, int value)
        => element.SetValue(SelectionHeightProperty, value);
    #endregion

    #region SelectionWidth
    /// <summary>
    /// Defines the width of the selection indicator.
    /// </summary>
    public static readonly DependencyProperty SelectionWidthProperty =
        DependencyProperty.RegisterAttached("SelectionWidth", typeof(int), typeof(NavigationRailAssist), new PropertyMetadata(default(int)));
    public static int GetSelectionWidth(DependencyObject element)
        => (int)element.GetValue(SelectionWidthProperty);
    public static void SetSelectionWidth(DependencyObject element, int value)
        => element.SetValue(SelectionWidthProperty, value);
    #endregion

    #region UnselectedIcon
    /// <summary>
    /// Defines the icon displayed when the item is not selected.
    /// </summary>
    public static readonly DependencyProperty UnselectedIconProperty =
        DependencyProperty.RegisterAttached("UnselectedIcon", typeof(PackIconKind), typeof(NavigationRailAssist), new PropertyMetadata(PackIconKind.None));
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
        DependencyProperty.RegisterAttached("SelectedIcon", typeof(PackIconKind), typeof(NavigationRailAssist), new PropertyMetadata(PackIconKind.None));
    public static PackIconKind GetSelectedIcon(DependencyObject element)
        => (PackIconKind)element.GetValue(SelectedIconProperty);
    public static void SetSelectedIcon(DependencyObject element, PackIconKind value)
        => element.SetValue(SelectedIconProperty, value);
    #endregion

    #region IconSize
    /// <summary>
    /// Defines the size of navigation rail icons.
    /// </summary>
    public static readonly DependencyProperty IconSizeProperty =
        DependencyProperty.RegisterAttached("IconSize", typeof(int), typeof(NavigationRailAssist), new PropertyMetadata(24));
    public static int GetIconSize(DependencyObject element)
        => (int)element.GetValue(IconSizeProperty);
    public static void SetIconSize(DependencyObject element, int value)
        => element.SetValue(IconSizeProperty, value);
    #endregion

    #region IsTextVisible
    /// <summary>
    /// Determines whether navigation rail text is visible.
    /// </summary>
    public static readonly DependencyProperty IsTextVisibleProperty =
        DependencyProperty.RegisterAttached("IsTextVisible", typeof(bool), typeof(NavigationRailAssist), new PropertyMetadata(true));
    public static bool GetIsTextVisible(DependencyObject element)
        => (bool)element.GetValue(IsTextVisibleProperty);
    public static void SetIsTextVisible(DependencyObject element, bool value)
        => element.SetValue(IsTextVisibleProperty, value);
    #endregion
}
