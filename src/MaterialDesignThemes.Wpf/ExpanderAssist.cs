using System.Windows.Media;

namespace MaterialDesignThemes.Wpf;

public enum ExpanderButtonPosition
{
    Default,
    Start,
    End
}

/// <summary>
/// Provides attached properties for configuring Expander appearance and behavior.
/// </summary>
public static class ExpanderAssist
{
    private static readonly Thickness DefaultHorizontalHeaderPadding = new(24, 12, 24, 12);
    private static readonly Thickness DefaultVerticalHeaderPadding = new(12, 24, 12, 24);

    #region AttachedProperty : HorizontalHeaderPaddingProperty
    /// <summary>
    /// Defines the horizontal header padding of the Expander.
    /// </summary>
    public static readonly DependencyProperty HorizontalHeaderPaddingProperty
        = DependencyProperty.RegisterAttached("HorizontalHeaderPadding", typeof(Thickness), typeof(ExpanderAssist),
            new FrameworkPropertyMetadata(DefaultHorizontalHeaderPadding, FrameworkPropertyMetadataOptions.Inherits));

    public static Thickness GetHorizontalHeaderPadding(Expander element)
        => (Thickness)element.GetValue(HorizontalHeaderPaddingProperty);
    public static void SetHorizontalHeaderPadding(Expander element, Thickness value)
        => element.SetValue(HorizontalHeaderPaddingProperty, value);
    #endregion

    #region AttachedProperty : VerticalHeaderPaddingProperty
    /// <summary>
    /// Defines the vertical header padding of the Expander.
    /// </summary>
    public static readonly DependencyProperty VerticalHeaderPaddingProperty
        = DependencyProperty.RegisterAttached("VerticalHeaderPadding", typeof(Thickness), typeof(ExpanderAssist),
            new FrameworkPropertyMetadata(DefaultVerticalHeaderPadding, FrameworkPropertyMetadataOptions.Inherits));

    public static Thickness GetVerticalHeaderPadding(Expander element)
        => (Thickness)element.GetValue(VerticalHeaderPaddingProperty);
    public static void SetVerticalHeaderPadding(Expander element, Thickness value)
        => element.SetValue(VerticalHeaderPaddingProperty, value);
    #endregion

    #region AttachedProperty : HeaderFontSizeProperty
    /// <summary>
    /// Defines the header font size of the Expander.
    /// </summary>
    public static readonly DependencyProperty HeaderFontSizeProperty
        = DependencyProperty.RegisterAttached("HeaderFontSize", typeof(double), typeof(ExpanderAssist),
            new FrameworkPropertyMetadata(15.0));

    public static double GetHeaderFontSize(Expander element)
        => (double)element.GetValue(HeaderFontSizeProperty);
    public static void SetHeaderFontSize(Expander element, double value)
        => element.SetValue(HeaderFontSizeProperty, value);
    #endregion

    #region AttachedProperty : HeaderBackgroundProperty
    /// <summary>
    /// Defines the header background brush of the Expander.
    /// </summary>
    public static readonly DependencyProperty HeaderBackgroundProperty
        = DependencyProperty.RegisterAttached("HeaderBackground", typeof(Brush), typeof(ExpanderAssist));

    public static Brush? GetHeaderBackground(Expander element)
        => (Brush?)element.GetValue(HeaderBackgroundProperty);
    public static void SetHeaderBackground(Expander element, Brush? value)
        => element.SetValue(HeaderBackgroundProperty, value);
    #endregion

    #region AttachedProperty : ExpanderButtonContentProperty
    /// <summary>
    /// Defines the content displayed by the Expander button.
    /// </summary>
    public static readonly DependencyProperty ExpanderButtonContentProperty
        = DependencyProperty.RegisterAttached("ExpanderButtonContent", typeof(object), typeof(ExpanderAssist));

    public static object? GetExpanderButtonContent(Expander element)
        => (object?)element.GetValue(ExpanderButtonContentProperty);
    public static void SetExpanderButtonContent(Expander element, object? value)
        => element.SetValue(ExpanderButtonContentProperty, value);
    #endregion

    #region AttachedProperty : ExpanderButtonPositionProperty
    /// <summary>
    /// Defines the position of the Expander button.
    /// </summary>
    public static readonly DependencyProperty ExpanderButtonPositionProperty
        = DependencyProperty.RegisterAttached("ExpanderButtonPosition", typeof(ExpanderButtonPosition), typeof(ExpanderAssist), new PropertyMetadata(ExpanderButtonPosition.Default));

    public static ExpanderButtonPosition GetExpanderButtonPosition(Expander element)
        => (ExpanderButtonPosition)element.GetValue(ExpanderButtonPositionProperty);
    public static void SetExpanderButtonPosition(Expander element, ExpanderButtonPosition value)
        => element.SetValue(ExpanderButtonPositionProperty, value);
    #endregion
}
