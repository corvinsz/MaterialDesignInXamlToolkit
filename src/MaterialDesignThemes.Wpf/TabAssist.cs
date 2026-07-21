namespace MaterialDesignThemes.Wpf;


/// <summary>
/// Defines how tab headers behave when there is insufficient space.
/// </summary>
public enum TabControlHeaderBehavior
{
    Scrolling,
    Wrapping
}

/// <summary>
/// Provides attached properties for customizing tab controls, headers, navigation panels, and related behavior.
/// </summary>
public static class TabAssist
{
    /// <summary>
    /// Gets or sets whether the tab uses a filled visual style.
    /// </summary>
    public static readonly DependencyProperty HasFilledTabProperty = DependencyProperty.RegisterAttached(
        "HasFilledTab", typeof(bool), typeof(TabAssist), new PropertyMetadata(false));

    public static void SetHasFilledTab(DependencyObject element, bool value)
        => element.SetValue(HasFilledTabProperty, value);

    public static bool GetHasFilledTab(DependencyObject element)
        => (bool)element.GetValue(HasFilledTabProperty);

    /// <summary>
    /// Gets or sets whether all tabs use a uniform width.
    /// </summary>
    public static readonly DependencyProperty HasUniformTabWidthProperty = DependencyProperty.RegisterAttached(
        "HasUniformTabWidth", typeof(bool), typeof(TabAssist), new PropertyMetadata(false));

    public static void SetHasUniformTabWidth(DependencyObject element, bool value)
        => element.SetValue(HasUniformTabWidthProperty, value);

    public static bool GetHasUniformTabWidth(DependencyObject element)
        => (bool)element.GetValue(HasUniformTabWidthProperty);

    /// <summary>
    /// Gets or sets the margin applied to the tab header panel.
    /// </summary>
    public static readonly DependencyProperty HeaderPanelMarginProperty = DependencyProperty.RegisterAttached(
        "HeaderPanelMargin", typeof(Thickness), typeof(TabAssist), new PropertyMetadata(default(Thickness)));

    public static void SetHeaderPanelMargin(DependencyObject element, Thickness value)
        => element.SetValue(HeaderPanelMarginProperty, value);

    public static Thickness GetHeaderPanelMargin(DependencyObject element)
        => (Thickness)element.GetValue(HeaderPanelMarginProperty);

    /// <summary>
    /// Gets or sets custom content displayed in the tab header panel.
    /// </summary>
    public static object? GetHeaderPanelCustomContent(DependencyObject obj)
        => (object?)obj.GetValue(HeaderPanelCustomContentProperty);

    public static void SetHeaderPanelCustomContent(DependencyObject obj, object? value)
        => obj.SetValue(HeaderPanelCustomContentProperty, value);

    public static readonly DependencyProperty HeaderPanelCustomContentProperty = DependencyProperty.RegisterAttached(
        "HeaderPanelCustomContent", typeof(object), typeof(TabAssist), new PropertyMetadata(default));

    /// <summary>
    /// Gets whether the tab headers are currently overflowing.
    /// </summary>
    internal static readonly DependencyPropertyKey IsOverflowingPropertyKey = DependencyProperty.RegisterAttachedReadOnly(
        "IsOverflowing", typeof(bool), typeof(TabAssist), new PropertyMetadata(false));

    public static readonly DependencyProperty IsOverflowingProperty = IsOverflowingPropertyKey.DependencyProperty;

    public static bool GetIsOverflowing(DependencyObject obj)
        => (bool)obj.GetValue(IsOverflowingProperty);

    internal static void SetIsOverflowing(DependencyObject obj, bool value)
    => obj.SetValue(IsOverflowingPropertyKey, value);

    /// <summary>
    /// Gets or sets whether the navigation panel is enabled.
    /// </summary>
    public static bool GetUseNavigationPanel(DependencyObject obj)
        => (bool)obj.GetValue(UseNavigationPanelProperty);

    public static void SetUseNavigationPanel(DependencyObject obj, bool value)
        => obj.SetValue(UseNavigationPanelProperty, value);

    public static readonly DependencyProperty UseNavigationPanelProperty = DependencyProperty.RegisterAttached(
        "UseNavigationPanel", typeof(bool), typeof(TabAssist), new PropertyMetadata(false));

    /// <summary>
    /// Gets or sets the margin applied to the navigation panel.
    /// </summary>
    public static Thickness GetNavigationPanelMargin(DependencyObject obj)
        => (Thickness)obj.GetValue(NavigationPanelMarginProperty);

    public static void SetNavigationPanelMargin(DependencyObject obj, Thickness value)
        => obj.SetValue(NavigationPanelMarginProperty, value);

    public static readonly DependencyProperty NavigationPanelMarginProperty = DependencyProperty.RegisterAttached(
        "NavigationPanelMargin", typeof(Thickness), typeof(TabAssist), new PropertyMetadata(default(Thickness)));

    /// <summary>
    /// Gets or sets the placement of the navigation panel.
    /// </summary>
    public static NavigationPanelPlacement GetNavigationPanelPlacement(DependencyObject obj)
        => (NavigationPanelPlacement)obj.GetValue(NavigationPanelPlacementProperty);

    public static void SetNavigationPanelPlacement(DependencyObject obj, NavigationPanelPlacement value)
        => obj.SetValue(NavigationPanelPlacementProperty, value);

    public static readonly DependencyProperty NavigationPanelPlacementProperty = DependencyProperty.RegisterAttached(
        "NavigationPanelPlacement", typeof(NavigationPanelPlacement), typeof(TabAssist), new PropertyMetadata(NavigationPanelPlacement.Split));

    /// <summary>
    /// Gets or sets the behavior of the navigation panel.
    /// </summary>
    public static NavigationPanelBehavior GetNavigationPanelBehavior(DependencyObject obj)
        => (NavigationPanelBehavior)obj.GetValue(NavigationPanelBehaviorProperty);

    public static void SetNavigationPanelBehavior(DependencyObject obj, NavigationPanelBehavior value)
        => obj.SetValue(NavigationPanelBehaviorProperty, value);

    public static readonly DependencyProperty NavigationPanelBehaviorProperty = DependencyProperty.RegisterAttached(
        "NavigationPanelBehavior", typeof(NavigationPanelBehavior), typeof(TabAssist), new PropertyMetadata(NavigationPanelBehavior.Scroll));

    /// <summary>
    /// Gets or sets the visibility state used to bind the items host behavior.
    /// </summary>
    public static Visibility GetBindableIsItemsHost(DependencyObject obj)
        => (Visibility)obj.GetValue(BindableIsItemsHostProperty);

    public static void SetBindableIsItemsHost(DependencyObject obj, Visibility value)
        => obj.SetValue(BindableIsItemsHostProperty, value);

    public static readonly DependencyProperty BindableIsItemsHostProperty =
        DependencyProperty.RegisterAttached("BindableIsItemsHost", typeof(Visibility), typeof(TabAssist), new PropertyMetadata(Visibility.Collapsed, OnBindableIsItemsHostChanged));

    /// <summary>
    /// Gets or sets the cursor displayed over tab headers.
    /// </summary>
    public static Cursor GetTabHeaderCursor(DependencyObject obj)
        => (Cursor)obj.GetValue(TabHeaderCursorProperty);

    public static void SetTabHeaderCursor(DependencyObject obj, Cursor value)
        => obj.SetValue(TabHeaderCursorProperty, value);

    public static readonly DependencyProperty TabHeaderCursorProperty =
        DependencyProperty.RegisterAttached("TabHeaderCursor", typeof(Cursor), typeof(TabAssist), new PropertyMetadata(Cursors.Hand));

    /// <summary>
    /// Gets or sets the header layout behavior.
    /// </summary>
    public static TabControlHeaderBehavior GetHeaderBehavior(DependencyObject obj)
        => (TabControlHeaderBehavior)obj.GetValue(HeaderBehaviorProperty);

    public static void SetHeaderBehavior(DependencyObject obj, TabControlHeaderBehavior value)
        => obj.SetValue(HeaderBehaviorProperty, value);

    public static readonly DependencyProperty HeaderBehaviorProperty =
        DependencyProperty.RegisterAttached("HeaderBehavior", typeof(TabControlHeaderBehavior), typeof(TabAssist),
            new PropertyMetadata(TabControlHeaderBehavior.Scrolling));

    /// <summary>
    /// Gets the header padding value.
    /// </summary>
    public static double GetHeaderPadding(DependencyObject obj)
        => (double)obj.GetValue(HeaderPaddingProperty);

    /// <summary>
    /// Gets or sets whether header padding is applied.
    /// </summary>
    public static bool GetUseHeaderPadding(DependencyObject obj)
        => (bool)obj.GetValue(UseHeaderPaddingProperty);

    public static void SetUseHeaderPadding(DependencyObject obj, bool value)
        => obj.SetValue(UseHeaderPaddingProperty, value);

    public static readonly DependencyProperty UseHeaderPaddingProperty =
        DependencyProperty.RegisterAttached("UseHeaderPadding", typeof(bool), typeof(TabAssist), new PropertyMetadata(false));

    /// <summary>
    /// Gets or sets the padding applied to tab headers.
    /// </summary>
    public static void SetHeaderPadding(DependencyObject obj, double value)
        => obj.SetValue(HeaderPaddingProperty, value);

    public static readonly DependencyProperty HeaderPaddingProperty =
        DependencyProperty.RegisterAttached("HeaderPadding", typeof(double),
            typeof(TabAssist), new PropertyMetadata(0d));

    /// <summary>
    /// Gets or sets the duration of tab header scrolling animations.
    /// </summary>
    public static TimeSpan GetScrollDuration(DependencyObject obj)
        => (TimeSpan)obj.GetValue(ScrollDurationProperty);

    public static void SetScrollDuration(DependencyObject obj, TimeSpan value)
        => obj.SetValue(ScrollDurationProperty, value);

    public static readonly DependencyProperty ScrollDurationProperty =
        DependencyProperty.RegisterAttached("ScrollDuration", typeof(TimeSpan),
            typeof(TabAssist), new PropertyMetadata(TimeSpan.Zero));
}

public enum NavigationPanelPlacement
{
    Split,
    Left,
    Right
}

public enum NavigationPanelBehavior
{
    Scroll,
    Select
}
