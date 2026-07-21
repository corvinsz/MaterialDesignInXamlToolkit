using System.Windows.Media;

namespace MaterialDesignThemes.Wpf;

/// <summary>
/// Provides attached properties for displaying progress indicators on buttons.
/// </summary>
public static class ButtonProgressAssist
{
    private const double DefaultMaximum = 100.0;

    #region AttachedProperty : MinimumProperty
    /// <summary>
    /// Defines the minimum value of the button progress range.
    /// </summary>
    public static readonly DependencyProperty MinimumProperty
        = DependencyProperty.RegisterAttached("Minimum", typeof(double), typeof(ButtonProgressAssist), new FrameworkPropertyMetadata(default(double)));

    public static double GetMinimum(ButtonBase element) => (double)element.GetValue(MinimumProperty);
    public static void SetMinimum(ButtonBase element, double value) => element.SetValue(MinimumProperty, value);
    #endregion

    #region AttachedProperty : MaximumProperty
    /// <summary>
    /// Defines the maximum value of the button progress range.
    /// </summary>
    public static readonly DependencyProperty MaximumProperty
        = DependencyProperty.RegisterAttached("Maximum", typeof(double), typeof(ButtonProgressAssist), new FrameworkPropertyMetadata(DefaultMaximum));

    public static double GetMaximum(ButtonBase element) => (double)element.GetValue(MaximumProperty);
    public static void SetMaximum(ButtonBase element, double value) => element.SetValue(MaximumProperty, value);
    #endregion

    #region AttachedProperty : ValueProperty
    /// <summary>
    /// Defines the current progress value displayed by the button.
    /// </summary>
    public static readonly DependencyProperty ValueProperty
        = DependencyProperty.RegisterAttached("Value", typeof(double), typeof(ButtonProgressAssist), new FrameworkPropertyMetadata(default(double)));

    public static double GetValue(ButtonBase element) => (double)element.GetValue(ValueProperty);
    public static void SetValue(ButtonBase element, double value) => element.SetValue(ValueProperty, value);
    #endregion

    #region AttachedProperty : IsIndeterminate
    /// <summary>
    /// Indicates whether the progress is shown in an indeterminate state.
    /// </summary>
    public static readonly DependencyProperty IsIndeterminateProperty
        = DependencyProperty.RegisterAttached("IsIndeterminate", typeof(bool), typeof(ButtonProgressAssist), new FrameworkPropertyMetadata(default(bool)));

    public static bool GetIsIndeterminate(ButtonBase element) => (bool)element.GetValue(IsIndeterminateProperty);
    public static void SetIsIndeterminate(ButtonBase element, bool isIndeterminate) => element.SetValue(IsIndeterminateProperty, isIndeterminate);
    #endregion

    #region AttachedProperty : IndicatorForegroundProperty
    /// <summary>
    /// Defines the foreground brush of the progress indicator.
    /// </summary>
    public static readonly DependencyProperty IndicatorForegroundProperty
        = DependencyProperty.RegisterAttached("IndicatorForeground", typeof(Brush), typeof(ButtonProgressAssist), new FrameworkPropertyMetadata(default(Brush)));

    public static Brush GetIndicatorForeground(ButtonBase element) => (Brush)element.GetValue(IndicatorForegroundProperty);
    public static void SetIndicatorForeground(ButtonBase element, Brush indicatorForeground) => element.SetValue(IndicatorForegroundProperty, indicatorForeground);
    #endregion

    #region AttachedProperty : IndicatorBackgroundProperty
    /// <summary>
    /// Defines the background brush of the progress indicator.
    /// </summary>
    public static readonly DependencyProperty IndicatorBackgroundProperty
        = DependencyProperty.RegisterAttached("IndicatorBackground", typeof(Brush), typeof(ButtonProgressAssist), new FrameworkPropertyMetadata(default(Brush)));

    public static Brush GetIndicatorBackground(ButtonBase element) => (Brush)element.GetValue(IndicatorBackgroundProperty);
    public static void SetIndicatorBackground(ButtonBase element, Brush indicatorBackground) => element.SetValue(IndicatorBackgroundProperty, indicatorBackground);
    #endregion

    #region AttachedProperty : IsIndicatorVisibleProperty
    /// <summary>
    /// Indicates whether the progress indicator is visible.
    /// </summary>
    public static readonly DependencyProperty IsIndicatorVisibleProperty
        = DependencyProperty.RegisterAttached("IsIndicatorVisible", typeof(bool), typeof(ButtonProgressAssist), new FrameworkPropertyMetadata(default(bool)));

    public static bool GetIsIndicatorVisible(ButtonBase element) => (bool)element.GetValue(IsIndicatorVisibleProperty);
    public static void SetIsIndicatorVisible(ButtonBase element, bool isIndicatorVisible) => element.SetValue(IsIndicatorVisibleProperty, isIndicatorVisible);
    #endregion

    #region AttachedProperty : OpacityProperty
    /// <summary>
    /// Defines the opacity of the progress indicator.
    /// </summary>
    public static readonly DependencyProperty OpacityProperty
        = DependencyProperty.RegisterAttached("Opacity", typeof(double), typeof(ButtonProgressAssist), new FrameworkPropertyMetadata(default(double)));

    public static double GetOpacity(ButtonBase element) => (double)element.GetValue(OpacityProperty);
    public static void SetOpacity(ButtonBase element, double opacity) => element.SetValue(OpacityProperty, opacity);
    #endregion
}
