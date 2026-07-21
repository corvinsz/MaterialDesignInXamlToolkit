namespace MaterialDesignThemes.Wpf;

/// <summary>
/// Provides obsolete attached properties for customizing TimePicker appearance.
/// </summary>
[Obsolete("This class is obsolete and will be removed in a future version. Please use the TextFieldAssist equivalents instead. For OutlinedBorderInactiveThickness, simply use TimePicker.BorderThickness property instead.")]
public static class TimePickerAssist
{
    /// <summary>
    /// Gets or sets the inactive border thickness for outlined TimePicker styles.
    /// </summary>
    public static readonly DependencyProperty OutlinedBorderInactiveThicknessProperty = DependencyProperty.RegisterAttached(
        "OutlinedBorderInactiveThickness", typeof(Thickness), typeof(TimePickerAssist), new FrameworkPropertyMetadata(Constants.DefaultOutlinedBorderInactiveThickness, FrameworkPropertyMetadataOptions.Inherits));
    public static void SetOutlinedBorderInactiveThickness(DependencyObject element, Thickness value) => element.SetValue(OutlinedBorderInactiveThicknessProperty, value);
    public static Thickness GetOutlinedBorderInactiveThickness(DependencyObject element) => (Thickness)element.GetValue(OutlinedBorderInactiveThicknessProperty);

    /// <summary>
    /// Gets or sets the active border thickness for outlined TimePicker styles.
    /// </summary>
    public static readonly DependencyProperty OutlinedBorderActiveThicknessProperty = DependencyProperty.RegisterAttached(
        "OutlinedBorderActiveThickness", typeof(Thickness), typeof(TimePickerAssist), new FrameworkPropertyMetadata(Constants.DefaultOutlinedBorderActiveThickness, FrameworkPropertyMetadataOptions.Inherits));
    public static void SetOutlinedBorderActiveThickness(DependencyObject element, Thickness value) => element.SetValue(OutlinedBorderActiveThicknessProperty, value);
    public static Thickness GetOutlinedBorderActiveThickness(DependencyObject element) => (Thickness)element.GetValue(OutlinedBorderActiveThicknessProperty);
}
