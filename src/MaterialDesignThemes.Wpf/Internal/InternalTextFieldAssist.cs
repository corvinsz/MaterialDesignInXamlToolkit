namespace MaterialDesignThemes.Wpf.Internal;

/// <summary>
/// Provides internal attached properties used to coordinate text field behavior.
/// </summary>
public static class InternalTextFieldAssist
{
    /// <summary>
    /// Indicates whether the associated text field should be treated as being hovered.
    /// </summary>
    public static readonly DependencyProperty IsMouseOverProperty = DependencyProperty.RegisterAttached(
        "IsMouseOver", typeof(bool), typeof(InternalTextFieldAssist), new PropertyMetadata(default(bool)));
    public static void SetIsMouseOver(DependencyObject element, bool value) => element.SetValue(IsMouseOverProperty, value);
    public static bool GetIsMouseOver(DependencyObject element) => (bool)element.GetValue(IsMouseOverProperty);
}
