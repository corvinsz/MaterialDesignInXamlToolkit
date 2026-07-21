namespace MaterialDesignThemes.Wpf;

/// <summary>
/// Provides attached properties for customizing check box appearance.
/// </summary>
public class CheckBoxAssist
{
    private const double DefaultCheckBoxSize = 18.0;

    #region AttachedProperty : CheckBoxSizeProperty
    /// <summary>
    /// Defines the size of the check box indicator.
    /// </summary>
    public static readonly DependencyProperty CheckBoxSizeProperty
        = DependencyProperty.RegisterAttached("CheckBoxSize", typeof(double), typeof(CheckBoxAssist), new PropertyMetadata(DefaultCheckBoxSize));

    public static double GetCheckBoxSize(CheckBox element) => (double)element.GetValue(CheckBoxSizeProperty);
    public static void SetCheckBoxSize(CheckBox element, double checkBoxSize) => element.SetValue(CheckBoxSizeProperty, checkBoxSize);
    #endregion
}
