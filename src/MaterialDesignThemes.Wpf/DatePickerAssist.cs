namespace MaterialDesignThemes.Wpf;

/// <summary>
/// Provides obsolete attached properties for customizing DatePicker border thickness.
/// </summary>
[Obsolete("This class is obsolete and will be removed in a future version. Please use the TextFieldAssist equivalents instead. For OutlinedBorderInactiveThickness, simply use DatePicker.BorderThickness property instead.")]
public static class DatePickerAssist
{
    /// <summary>
    /// Defines the inactive outlined border thickness of the DatePicker.
    /// </summary>
    public static readonly DependencyProperty OutlinedBorderInactiveThicknessProperty = DependencyProperty.RegisterAttached(
        "OutlinedBorderInactiveThickness", typeof(Thickness), typeof(DatePickerAssist), new FrameworkPropertyMetadata(Constants.DefaultOutlinedBorderInactiveThickness, FrameworkPropertyMetadataOptions.Inherits));
    public static void SetOutlinedBorderInactiveThickness(DependencyObject element, Thickness value) => element.SetValue(OutlinedBorderInactiveThicknessProperty, value);
    public static Thickness GetOutlinedBorderInactiveThickness(DependencyObject element) => (Thickness)element.GetValue(OutlinedBorderInactiveThicknessProperty);

    /// <summary>
    /// Defines the active outlined border thickness of the DatePicker.
    /// </summary>
    public static readonly DependencyProperty OutlinedBorderActiveThicknessProperty = DependencyProperty.RegisterAttached(
        "OutlinedBorderActiveThickness", typeof(Thickness), typeof(DatePickerAssist), new FrameworkPropertyMetadata(Constants.DefaultOutlinedBorderActiveThickness, FrameworkPropertyMetadataOptions.Inherits));
    public static void SetOutlinedBorderActiveThickness(DependencyObject element, Thickness value) => element.SetValue(OutlinedBorderActiveThicknessProperty, value);
    public static Thickness GetOutlinedBorderActiveThickness(DependencyObject element) => (Thickness)element.GetValue(OutlinedBorderActiveThicknessProperty);
}
