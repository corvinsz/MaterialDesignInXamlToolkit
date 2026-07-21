namespace MaterialDesignThemes.Wpf;

/// <summary>
/// Provides attached properties for configuring scrollbar appearance.
/// </summary>
public static class ScrollBarAssist
{
    /// <summary>
    /// Controls the visibility of scrollbar buttons.
    /// </summary>
    public static readonly DependencyProperty ButtonsVisibilityProperty =
        DependencyProperty.RegisterAttached("ButtonsVisibility", typeof(Visibility), typeof(ScrollBarAssist), new PropertyMetadata(Visibility.Visible));

    public static void SetButtonsVisibility(DependencyObject element, Visibility value)
    {
        element.SetValue(ButtonsVisibilityProperty, value);
    }

    public static Visibility GetButtonsVisibility(DependencyObject element)
    {
        return (Visibility)element.GetValue(ButtonsVisibilityProperty);
    }

    /// <summary>
    /// Defines the corner radius of the scrollbar thumb.
    /// </summary>
    public static readonly DependencyProperty ThumbCornerRadiusProperty = DependencyProperty.RegisterAttached(
        "ThumbCornerRadius", typeof(CornerRadius), typeof(ScrollBarAssist), new PropertyMetadata(default(CornerRadius)));

    public static void SetThumbCornerRadius(DependencyObject element, CornerRadius value)
    {
        element.SetValue(ThumbCornerRadiusProperty, value);
    }

    public static CornerRadius GetThumbCornerRadius(DependencyObject element)
    {
        return (CornerRadius)element.GetValue(ThumbCornerRadiusProperty);
    }

    /// <summary>
    /// Defines the width of the scrollbar thumb.
    /// </summary>
    public static readonly DependencyProperty ThumbWidthProperty = DependencyProperty.RegisterAttached(
        "ThumbWidth", typeof(double), typeof(ScrollBarAssist), new PropertyMetadata(double.NaN));

    public static void SetThumbWidth(DependencyObject element, double value)
    {
        element.SetValue(ThumbWidthProperty, value);
    }

    public static double GetThumbWidth(DependencyObject element)
    {
        return (double)element.GetValue(ThumbWidthProperty);
    }

    /// <summary>
    /// Defines the height of the scrollbar thumb.
    /// </summary>
    public static readonly DependencyProperty ThumbHeightProperty = DependencyProperty.RegisterAttached(
        "ThumbHeight", typeof(double), typeof(ScrollBarAssist), new PropertyMetadata(double.NaN));

    public static void SetThumbHeight(DependencyObject element, double value)
    {
        element.SetValue(ThumbHeightProperty, value);
    }

    public static double GetThumbHeight(DependencyObject element)
    {
        return (double)element.GetValue(ThumbHeightProperty);
    }
}
