using System.Windows.Media;

namespace MaterialDesignThemes.Wpf;

/// <summary>
/// Provides attached properties for customizing validation appearance and behavior.
/// </summary>
public static class ValidationAssist
{
    #region ShowOnFocusProperty

    /// <summary>
    /// Determines whether validation messages are shown only while the control has focus.
    /// </summary>
    public static readonly DependencyProperty OnlyShowOnFocusProperty = DependencyProperty.RegisterAttached(
        "OnlyShowOnFocus",
        typeof(bool),
        typeof(ValidationAssist),
        new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits));

    public static bool GetOnlyShowOnFocus(DependencyObject element)
    {
        return (bool)element.GetValue(OnlyShowOnFocusProperty);
    }

    public static void SetOnlyShowOnFocus(DependencyObject element, bool value)
    {
        element.SetValue(OnlyShowOnFocusProperty, value);
    }

    #endregion

    #region UsePopupProperty

    /// <summary>
    /// Determines whether validation messages are displayed in a popup.
    /// </summary>
    public static readonly DependencyProperty UsePopupProperty = DependencyProperty.RegisterAttached(
        "UsePopup",
        typeof(bool),
        typeof(ValidationAssist),
        new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits));

    public static bool GetUsePopup(DependencyObject element)
    {
        return (bool)element.GetValue(UsePopupProperty);
    }

    public static void SetUsePopup(DependencyObject element, bool value)
    {
        element.SetValue(UsePopupProperty, value);
    }

    #endregion

    /// <summary>
    /// Specifies the placement of the validation popup.
    /// </summary>
    public static readonly DependencyProperty PopupPlacementProperty = DependencyProperty.RegisterAttached(
        "PopupPlacement",
        typeof(PlacementMode),
        typeof(ValidationAssist),
        new FrameworkPropertyMetadata(PlacementMode.Bottom, FrameworkPropertyMetadataOptions.Inherits));

    public static PlacementMode GetPopupPlacement(DependencyObject element)
    {
        return (PlacementMode)element.GetValue(PopupPlacementProperty);
    }

    public static void SetPopupPlacement(DependencyObject element, PlacementMode value)
    {
        element.SetValue(PopupPlacementProperty, value);
    }

    /// <summary>
    /// Indicates whether validation visuals are suppressed.
    /// </summary>
    public static readonly DependencyProperty SuppressProperty = DependencyProperty.RegisterAttached(
        "Suppress", typeof(bool), typeof(ValidationAssist), new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.Inherits));

    /// <summary>
    /// Framework use only.
    /// </summary>
    /// <param name="element"></param>
    /// <param name="value"></param>
    public static void SetSuppress(DependencyObject element, bool value)
    {
        element.SetValue(SuppressProperty, value);
    }

    /// <summary>
    /// Framework use only.
    /// </summary>
    public static bool GetSuppress(DependencyObject element)
    {
        return (bool)element.GetValue(SuppressProperty);
    }

    /// <summary>
    /// Sets the background brush for validation content.
    /// </summary>
    public static readonly DependencyProperty BackgroundProperty = DependencyProperty.RegisterAttached(
        "Background", typeof(Brush), typeof(ValidationAssist), new PropertyMetadata(default(Brush)));

    public static void SetBackground(DependencyObject element, Brush value)
    {
        element.SetValue(BackgroundProperty, value);
    }

    public static Brush GetBackground(DependencyObject element)
    {
        return (Brush)element.GetValue(BackgroundProperty);
    }



    /// <summary>
    /// Sets the font size used for validation content.
    /// </summary>
    public static readonly DependencyProperty FontSizeProperty = DependencyProperty.RegisterAttached("FontSize", typeof(double), typeof(ValidationAssist), new PropertyMetadata(10.0));

    public static void SetFontSize(DependencyObject element, double value)
    {
        element.SetValue(FontSizeProperty, value);
    }

    public static double GetFontSize(DependencyObject element)
    {
        return (double)element.GetValue(FontSizeProperty);
    }

    /// <summary>
    /// Indicates whether the control currently has a validation error.
    /// </summary>
    public static readonly DependencyProperty HasErrorProperty = DependencyProperty.RegisterAttached(
        "HasError",
        typeof(bool),
        typeof(ValidationAssist),
        new PropertyMetadata(default(bool)));

    public static void SetHasError(DependencyObject element, bool value)
    {
        element.SetValue(HasErrorProperty, value);
    }

    public static bool GetHasError(DependencyObject element)
    {
        return (bool)element.GetValue(HasErrorProperty);
    }

    /// <summary>
    /// Specifies the horizontal alignment of validation content.
    /// </summary>
    public static readonly DependencyProperty HorizontalAlignmentProperty = DependencyProperty.RegisterAttached(
        "HorizontalAlignment", typeof(HorizontalAlignment), typeof(ValidationAssist), new PropertyMetadata(HorizontalAlignment.Left));

    public static void SetHorizontalAlignment(DependencyObject element, HorizontalAlignment value) => element.SetValue(HorizontalAlignmentProperty, value);
    public static HorizontalAlignment GetHorizontalAlignment(DependencyObject element) => (HorizontalAlignment)element.GetValue(HorizontalAlignmentProperty);
}
