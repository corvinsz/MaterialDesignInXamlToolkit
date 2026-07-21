namespace MaterialDesignThemes.Wpf;

/// <summary>
/// Provides attached properties for customizing the appearance and layout behavior of <see cref="Menu"/> controls.
/// </summary>
public static class MenuAssist
{
    #region AttachedProperty : TopLevelMenuItemHeight

    /// <summary>
    /// Defines the height of top-level menu items.
    /// </summary>
    public static readonly DependencyProperty TopLevelMenuItemHeightProperty
        = DependencyProperty.RegisterAttached(
            "TopLevelMenuItemHeight",
            typeof(double),
            typeof(MenuAssist));

    public static double GetTopLevelMenuItemHeight(DependencyObject element)
        => (double)element.GetValue(TopLevelMenuItemHeightProperty);

    public static void SetTopLevelMenuItemHeight(DependencyObject element, double value)
        => element.SetValue(TopLevelMenuItemHeightProperty, value);

    #endregion

    #region AttachedProperty : MenuItemsPresenterMargin

    /// <summary>
    /// Defines the margin applied to the menu items presenter.
    /// </summary>
    public static readonly DependencyProperty MenuItemsPresenterMarginProperty =
        DependencyProperty.RegisterAttached(
            "MenuItemsPresenterMargin",
            typeof(Thickness),
            typeof(MenuAssist),
            new FrameworkPropertyMetadata(new Thickness(0, 16, 0, 16), FrameworkPropertyMetadataOptions.Inherits));

    public static Thickness GetMenuItemsPresenterMargin(DependencyObject obj)
        => (Thickness)obj.GetValue(MenuItemsPresenterMarginProperty);

    public static void SetMenuItemsPresenterMargin(DependencyObject obj, Thickness value)
        => obj.SetValue(MenuItemsPresenterMarginProperty, value);

    #endregion
}
