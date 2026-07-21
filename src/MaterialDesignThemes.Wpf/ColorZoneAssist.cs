using System.Windows.Media;

namespace MaterialDesignThemes.Wpf;

/// <summary>
/// Provides attached properties for configuring color zones and their appearance.
/// </summary>
public static class ColorZoneAssist
{
    /// <summary>
    /// Defines the color zone mode applied to the element.
    /// </summary>
    public static readonly DependencyProperty ModeProperty = DependencyProperty.RegisterAttached(
        "Mode", typeof(ColorZoneMode), typeof(ColorZoneAssist), new FrameworkPropertyMetadata(default(ColorZoneMode), FrameworkPropertyMetadataOptions.Inherits));

    public static void SetMode(DependencyObject element, ColorZoneMode value)
        => element.SetValue(ModeProperty, value);

    public static ColorZoneMode GetMode(DependencyObject element)
        => (ColorZoneMode)element.GetValue(ModeProperty);

    /// <summary>
    /// Defines the background brush of the color zone.
    /// </summary>
    public static readonly DependencyProperty BackgroundProperty = DependencyProperty.RegisterAttached(
        "Background", typeof(Brush), typeof(ColorZoneAssist), new FrameworkPropertyMetadata(default(Brush)));

    public static void SetBackground(DependencyObject element, Brush value)
        => element.SetValue(BackgroundProperty, value);

    public static Brush GetBackground(DependencyObject element)
        => (Brush)element.GetValue(BackgroundProperty);

    /// <summary>
    /// Defines the foreground brush of the color zone.
    /// </summary>
    public static readonly DependencyProperty ForegroundProperty = DependencyProperty.RegisterAttached(
        "Foreground", typeof(Brush), typeof(ColorZoneAssist), new FrameworkPropertyMetadata(default(Brush)));

    public static void SetForeground(DependencyObject element, Brush value)
        => element.SetValue(ForegroundProperty, value);

    public static Brush GetForeground(DependencyObject element)
        => (Brush)element.GetValue(ForegroundProperty);
}
