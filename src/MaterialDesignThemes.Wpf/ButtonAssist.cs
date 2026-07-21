namespace MaterialDesignThemes.Wpf;

/// <summary>
/// Provides attached properties for customizing button appearance.
/// </summary>
public static class ButtonAssist
{
    private static readonly CornerRadius DefaultCornerRadius = new CornerRadius(2.0);

    #region AttachedProperty : CornerRadiusProperty
    /// <summary>
    /// Controls the corner radius of the surrounding box.
    /// </summary>
    /// <summary>
    /// Defines the corner radius applied to the button container.
    /// </summary>
    public static readonly DependencyProperty CornerRadiusProperty
        = DependencyProperty.RegisterAttached("CornerRadius", typeof(CornerRadius), typeof(ButtonAssist), new PropertyMetadata(DefaultCornerRadius));

    public static CornerRadius GetCornerRadius(DependencyObject element) => (CornerRadius)element.GetValue(CornerRadiusProperty);
    public static void SetCornerRadius(DependencyObject element, CornerRadius value) => element.SetValue(CornerRadiusProperty, value);
    #endregion
}
