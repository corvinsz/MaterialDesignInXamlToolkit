using System.Windows.Media;

namespace MaterialDesignThemes.Wpf;

/// <summary>
/// Provides attached properties for configuring ripple effect behavior and appearance.
/// </summary>
public static class RippleAssist
{
    #region ClipToBounds

    /// <summary>
    /// Determines whether the ripple effect is clipped to the element bounds.
    /// </summary>
    public static readonly DependencyProperty ClipToBoundsProperty = DependencyProperty.RegisterAttached(
        "ClipToBounds", typeof(bool), typeof(RippleAssist), new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.Inherits));

    public static void SetClipToBounds(DependencyObject element, bool value)
    {
        element.SetValue(ClipToBoundsProperty, value);
    }

    public static bool GetClipToBounds(DependencyObject element)
    {
        return (bool)element.GetValue(ClipToBoundsProperty);
    }

    #endregion

    #region StayOnCenter

    /// <summary>
    /// Determines whether the ripple originates from the center of the content.
    /// </summary>
    public static readonly DependencyProperty IsCenteredProperty = DependencyProperty.RegisterAttached(
        "IsCentered", typeof(bool), typeof(RippleAssist), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits));

    /// <summary>
    /// Sets whether the ripple originates from the center of the content.
    /// </summary>
    /// <param name="element"></param>
    /// <param name="value"></param>
    public static void SetIsCentered(DependencyObject element, bool value)
    {
        element.SetValue(IsCenteredProperty, value);
    }

    /// <summary>
    /// Gets whether the ripple originates from the center of the content.
    /// </summary>
    /// <param name="element"></param>        
    public static bool GetIsCentered(DependencyObject element)
    {
        return (bool)element.GetValue(IsCenteredProperty);
    }

    #endregion

    #region IsDisabled

    /// <summary>
    /// Determines whether the ripple effect is disabled.
    /// </summary>
    public static readonly DependencyProperty IsDisabledProperty = DependencyProperty.RegisterAttached(
        "IsDisabled", typeof(bool), typeof(RippleAssist), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits));

    /// <summary>
    /// Determines whether the ripple effect is disabled.
    /// </summary>
    /// <param name="element"></param>
    /// <param name="value"></param>
    public static void SetIsDisabled(DependencyObject element, bool value)
    {
        element.SetValue(IsDisabledProperty, value);
    }

    /// <summary>
    /// Gets whether the ripple effect is disabled.
    /// </summary>
    /// <param name="element"></param>        
    public static bool GetIsDisabled(DependencyObject element)
    {
        return (bool)element.GetValue(IsDisabledProperty);
    }

    #endregion

    #region RippleSizeMultiplier

    /// <summary>
    /// Defines the size multiplier applied to the ripple effect.
    /// </summary>
    public static readonly DependencyProperty RippleSizeMultiplierProperty = DependencyProperty.RegisterAttached(
        "RippleSizeMultiplier", typeof(double), typeof(RippleAssist), new FrameworkPropertyMetadata(1.0, FrameworkPropertyMetadataOptions.Inherits));

    public static void SetRippleSizeMultiplier(DependencyObject element, double value)
    {
        element.SetValue(RippleSizeMultiplierProperty, value);
    }

    public static double GetRippleSizeMultiplier(DependencyObject element)
    {
        return (double)element.GetValue(RippleSizeMultiplierProperty);
    }

    #endregion

    #region Feedback

    /// <summary>
    /// Defines the brush used to render the ripple feedback.
    /// </summary>
    public static readonly DependencyProperty FeedbackProperty = DependencyProperty.RegisterAttached(
        "Feedback", typeof(Brush), typeof(RippleAssist), new FrameworkPropertyMetadata(default(Brush), FrameworkPropertyMetadataOptions.Inherits | FrameworkPropertyMetadataOptions.AffectsRender));

    public static void SetFeedback(DependencyObject element, Brush value)
    {
        element.SetValue(FeedbackProperty, value);
    }

    public static Brush GetFeedback(DependencyObject element)
    {
        return (Brush)element.GetValue(FeedbackProperty);
    }

    #endregion

    #region RippleOnTop

    /// <summary>
    /// Determines whether the ripple is rendered above the content.
    /// </summary>
    public static readonly DependencyProperty RippleOnTopProperty = DependencyProperty.RegisterAttached(
        "RippleOnTop", typeof(bool), typeof(RippleAssist),
        new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.Inherits | FrameworkPropertyMetadataOptions.AffectsRender));

    public static void SetRippleOnTop(DependencyObject element, bool value)
    {
        element.SetValue(RippleOnTopProperty, value);
    }

    public static bool GetRippleOnTop(DependencyObject element)
    {
        return (bool)element.GetValue(RippleOnTopProperty);
    }

    #endregion

}
