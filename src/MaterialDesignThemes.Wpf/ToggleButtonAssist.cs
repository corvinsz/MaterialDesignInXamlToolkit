using System.Windows.Media;

namespace MaterialDesignThemes.Wpf;

/// <summary>
/// Provides attached properties for customizing ToggleButton content and switch appearance.
/// </summary>
public static class ToggleButtonAssist
{
    private static readonly DependencyPropertyKey HasOnContentPropertyKey =
        DependencyProperty.RegisterAttachedReadOnly(
            "HasOnContent", typeof(bool), typeof(ToggleButtonAssist),
            new PropertyMetadata(false));

    /// <summary>
    /// Indicates whether on-state content is available.
    /// </summary>
    public static readonly DependencyProperty HasOnContentProperty = HasOnContentPropertyKey.DependencyProperty;

    private static void SetHasOnContent(DependencyObject element, object value)
        => element.SetValue(HasOnContentPropertyKey, value);

    public static bool GetHasOnContent(DependencyObject element)
        => (bool)element.GetValue(HasOnContentProperty);

    /// <summary>
    /// Provides content displayed when the ToggleButton is checked.
    /// </summary>
    public static readonly DependencyProperty OnContentProperty = DependencyProperty.RegisterAttached(
        "OnContent", typeof(object), typeof(ToggleButtonAssist), new PropertyMetadata(default(object), OnContentPropertyChangedCallback));

    private static void OnContentPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        => SetHasOnContent(dependencyObject, dependencyPropertyChangedEventArgs.NewValue != null);

    /// <summary>
    /// Provides content displayed when the ToggleButton is checked.
    /// </summary>
    /// <param name="element"></param>
    /// <param name="value"></param>
    public static void SetOnContent(DependencyObject element, object value)
        => element.SetValue(OnContentProperty, value);

    /// <summary>
    /// Provides content displayed when the ToggleButton is checked.
    /// </summary>
    public static object GetOnContent(DependencyObject element)
        => element.GetValue(OnContentProperty);

    /// <summary>
    /// Provides a data template for checked-state content.
    /// </summary>
    public static readonly DependencyProperty OnContentTemplateProperty = DependencyProperty.RegisterAttached(
        "OnContentTemplate", typeof(DataTemplate), typeof(ToggleButtonAssist), new PropertyMetadata(default(DataTemplate)));

    /// <summary>
    /// Provides a data template for checked-state content.
    /// </summary>
    public static void SetOnContentTemplate(DependencyObject element, DataTemplate value)
        => element.SetValue(OnContentTemplateProperty, value);

    /// <summary>
    /// Provides a data template for checked-state content.
    /// </summary>
    public static DataTemplate GetOnContentTemplate(DependencyObject element)
        => (DataTemplate)element.GetValue(OnContentTemplateProperty);

    /// <summary>
    /// Gets or sets the background brush of the switch track when enabled.
    /// </summary>
    public static readonly DependencyProperty SwitchTrackOnBackgroundProperty =
        DependencyProperty.RegisterAttached(
            "SwitchTrackOnBackground", typeof(SolidColorBrush), typeof(ToggleButtonAssist));

    public static void SetSwitchTrackOnBackground(DependencyObject element, SolidColorBrush value)
        => element.SetValue(SwitchTrackOnBackgroundProperty, value);

    public static SolidColorBrush GetSwitchTrackOnBackground(DependencyObject element)
        => (SolidColorBrush)element.GetValue(SwitchTrackOnBackgroundProperty);

    /// <summary>
    /// Gets or sets the background brush of the switch track when disabled.
    /// </summary>
    public static readonly DependencyProperty SwitchTrackOffBackgroundProperty =
        DependencyProperty.RegisterAttached(
            "SwitchTrackOffBackground", typeof(SolidColorBrush), typeof(ToggleButtonAssist));

    public static void SetSwitchTrackOffBackground(DependencyObject element, SolidColorBrush value)
        => element.SetValue(SwitchTrackOffBackgroundProperty, value);

    public static SolidColorBrush GetSwitchTrackOffBackground(DependencyObject element)
        => (SolidColorBrush)element.GetValue(SwitchTrackOffBackgroundProperty);
}
