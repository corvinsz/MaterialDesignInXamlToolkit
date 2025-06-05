using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace MaterialDesignThemes.Wpf;

public enum RichToolTipPlacementMode
{
    Left,
    Top,
    Right,
    Bottom
}

[TemplatePart(Name = PopupPartName, Type = typeof(Popup))]
[TemplatePart(Name = PopupContentControlPartName, Type = typeof(ContentControl))]
[TemplatePart(Name = TogglePartName, Type = typeof(ToggleButton))]
[TemplateVisualState(GroupName = "PopupStates", Name = PopupIsOpenStateName)]
[TemplateVisualState(GroupName = "PopupStates", Name = PopupIsClosedStateName)]
[ContentProperty("PopupContent")]
public class RichToolTip : ContentControl
{
    public const string PopupPartName = "PART_Popup";
    public const string TogglePartName = "PART_Toggle";
    public const string PopupContentControlPartName = "PART_PopupContentControl";
    public const string PopupIsOpenStateName = "IsOpen";
    public const string PopupIsClosedStateName = "IsClosed";

    /// <summary>
    /// Routed command to be used inside of a popup content to close it.
    /// </summary>
    public static readonly RoutedCommand ClosePopupCommand = new();

    private PopupEx? _popup;
    private ToggleButton? _toggleButton;
    private Point _popupPointFromLastRequest;

    static RichToolTip()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(RichToolTip), new FrameworkPropertyMetadata(typeof(RichToolTip)));
        ToolTipService.IsEnabledProperty.OverrideMetadata(typeof(RichToolTip), new FrameworkPropertyMetadata(null, CoerceToolTipIsEnabled));
        EventManager.RegisterClassHandler(typeof(RichToolTip), Mouse.LostMouseCaptureEvent, new MouseEventHandler(OnLostMouseCapture));
        EventManager.RegisterClassHandler(typeof(RichToolTip), Mouse.MouseDownEvent, new MouseButtonEventHandler(OnMouseButtonDown), true);
    }

    public RichToolTip()
    {
    }

    public static readonly DependencyProperty ToggleCheckedContentCommandProperty = DependencyProperty.Register(
        nameof(ToggleCheckedContentCommand), typeof(ICommand), typeof(RichToolTip), new PropertyMetadata(default(ICommand?)));

    /// <summary>
    /// Command to execute if toggle is checked (popup is open) and <see cref="ToggleCheckedContent"/> is set.
    /// </summary>
    public ICommand? ToggleCheckedContentCommand
    {
        get => (ICommand?)GetValue(ToggleCheckedContentCommandProperty);
        set => SetValue(ToggleCheckedContentCommandProperty, value);
    }

    public static readonly DependencyProperty ToggleCheckedContentCommandParameterProperty = DependencyProperty.Register(
        nameof(ToggleCheckedContentCommandParameter), typeof(object), typeof(RichToolTip), new PropertyMetadata(default(object?)));

    /// <summary>
    /// Command parameter to use in conjunction with <see cref="ToggleCheckedContentCommand"/>.
    /// </summary>
    public object? ToggleCheckedContentCommandParameter
    {
        get => GetValue(ToggleCheckedContentCommandParameterProperty);
        set => SetValue(ToggleCheckedContentCommandParameterProperty, value);
    }

    public static readonly DependencyProperty PopupContentProperty = DependencyProperty.Register(
        nameof(PopupContent), typeof(object), typeof(RichToolTip), new PropertyMetadata(default(object?)));

    /// <summary>
    /// Content to display in the content.
    /// </summary>
    public object? PopupContent
    {
        get => GetValue(PopupContentProperty);
        set => SetValue(PopupContentProperty, value);
    }

    public static readonly DependencyProperty PopupContentTemplateProperty = DependencyProperty.Register(
        nameof(PopupContentTemplate), typeof(DataTemplate), typeof(RichToolTip), new PropertyMetadata(default(DataTemplate?)));

    /// <summary>
    /// Popup content template.
    /// </summary>
    public DataTemplate? PopupContentTemplate
    {
        get => (DataTemplate?)GetValue(PopupContentTemplateProperty);
        set => SetValue(PopupContentTemplateProperty, value);
    }

    public static readonly DependencyProperty IsPopupOpenProperty = DependencyProperty.Register(
        nameof(IsPopupOpen), typeof(bool), typeof(RichToolTip), new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, IsPopupOpenPropertyChangedCallback));

    private static void IsPopupOpenPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
    {
        var richToolTip = (RichToolTip)dependencyObject;
        var newValue = (bool)dependencyPropertyChangedEventArgs.NewValue;
        if (newValue)
        {
            Mouse.Capture(richToolTip, CaptureMode.SubTree);
        }
        else
        {
            Mouse.Capture(null);
        }

        richToolTip._popup?.RefreshPosition();

        VisualStateManager.GoToState(richToolTip, newValue ? PopupIsOpenStateName : PopupIsClosedStateName, true);

        if (newValue)
        {
            richToolTip.OnOpened();
        }
        else
        {
            richToolTip.OnClosed();
        }
    }

    /// <summary>
    /// Gets or sets whether the popup is currently open.
    /// </summary>
    public bool IsPopupOpen
    {
        get => (bool)GetValue(IsPopupOpenProperty);
        set => SetValue(IsPopupOpenProperty, value);
    }

    public static readonly DependencyProperty PlacementModeProperty = DependencyProperty.Register(
        nameof(PlacementMode), typeof(RichToolTipPlacementMode), typeof(RichToolTip), new PropertyMetadata(default(RichToolTipPlacementMode), PlacementModePropertyChangedCallback));

    private static void PlacementModePropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
    {
        ((RichToolTip)dependencyObject)._popup?.RefreshPosition();
    }

    /// <summary>
    /// Gets or sets how the popup is aligned in relation to the toggle.
    /// </summary>
    public RichToolTipPlacementMode PlacementMode
    {
        get => (RichToolTipPlacementMode)GetValue(PlacementModeProperty);
        set => SetValue(PlacementModeProperty, value);
    }

    internal static readonly DependencyProperty PlacementTargetProperty = DependencyProperty.Register(
        nameof(PlacementTarget), typeof(UIElement), typeof(RichToolTip), new PropertyMetadata(default(UIElement)));

    internal UIElement? PlacementTarget
    {
        get => (UIElement?)GetValue(PlacementTargetProperty);
        set => SetValue(PlacementTargetProperty, value);
    }


    /// <summary>
    /// Get or sets how to unfurl controls when opening the popups. Only child elements of type <see cref="ButtonBase"/> are animated.
    /// </summary>
    public static readonly DependencyProperty UnfurlOrientationProperty = DependencyProperty.Register(
        nameof(UnfurlOrientation), typeof(Orientation), typeof(RichToolTip), new PropertyMetadata(Orientation.Vertical));

    /// <summary>
    /// Gets or sets how to unfurl controls when opening the popups. Only child elements of type <see cref="ButtonBase"/> are animated.
    /// </summary>
    public Orientation UnfurlOrientation
    {
        get => (Orientation)GetValue(UnfurlOrientationProperty);
        set => SetValue(UnfurlOrientationProperty, value);
    }

    /// <summary>
    /// Get or sets the popup horizontal offset in relation to the button.
    /// </summary>
    public static readonly DependencyProperty PopupHorizontalOffsetProperty = DependencyProperty.Register(
        nameof(PopupHorizontalOffset), typeof(double), typeof(RichToolTip), new PropertyMetadata(default(double)));

    /// <summary>
    /// Get or sets the popup horizontal offset in relation to the button.
    /// </summary>
    public double PopupHorizontalOffset
    {
        get => (double)GetValue(PopupHorizontalOffsetProperty);
        set => SetValue(PopupHorizontalOffsetProperty, value);
    }

    /// <summary>
    /// Get or sets the popup vertical offset in relation to the button.
    /// </summary>
    public static readonly DependencyProperty PopupVerticalOffsetProperty = DependencyProperty.Register(
        nameof(PopupVerticalOffset), typeof(double), typeof(RichToolTip), new PropertyMetadata(default(double)));

    /// <summary>
    /// Get or sets the popup vertical offset in relation to the button.
    /// </summary>
    public double PopupVerticalOffset
    {
        get => (double)GetValue(PopupVerticalOffsetProperty);
        set => SetValue(PopupVerticalOffsetProperty, value);
    }

    /// <summary>
    /// Get or sets the corner radius of the popup card.
    /// </summary>
    public static readonly DependencyProperty PopupUniformCornerRadiusProperty = DependencyProperty.Register(
        nameof(PopupUniformCornerRadius), typeof(double), typeof(RichToolTip), new PropertyMetadata(default(double)));

    /// <summary>
    /// Get or sets the corner radius of the popup card.
    /// </summary>
    public double PopupUniformCornerRadius
    {
        get => (double)GetValue(PopupUniformCornerRadiusProperty);
        set => SetValue(PopupUniformCornerRadiusProperty, value);
    }

    /// <summary>
    /// Gets or sets the elevation of the popup card.
    /// </summary>
    public static readonly DependencyProperty PopupElevationProperty = DependencyProperty.Register(
        nameof(PopupElevation), typeof(Elevation), typeof(RichToolTip), new PropertyMetadata(Elevation.Dp0));

    /// <summary>
    /// Gets or sets the elevation of the popup card.
    /// </summary>
    public Elevation PopupElevation
    {
        get => (Elevation)GetValue(PopupElevationProperty);
        set => SetValue(PopupElevationProperty, value);
    }

    /// <summary>
    /// Framework use. Provides the method used to position the popup.
    /// </summary>
    public CustomPopupPlacementCallback PopupPlacementMethod => GetPopupPlacement;

    /// <summary>
    /// Event raised when the checked toggled content (if set) is clicked.
    /// </summary>
    public static readonly RoutedEvent ToggleCheckedContentClickEvent = EventManager.RegisterRoutedEvent("ToggleCheckedContentClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(RichToolTip));

    /// <summary>
    /// Event raised when the checked toggled content (if set) is clicked.
    /// </summary>
    [Category("Behavior")]
    public event RoutedEventHandler ToggleCheckedContentClick { add { AddHandler(ToggleCheckedContentClickEvent, value); } remove { RemoveHandler(ToggleCheckedContentClickEvent, value); } }

    /// <summary>
    /// Raises <see cref="ToggleCheckedContentClickEvent"/>.
    /// </summary>
    protected virtual void OnToggleCheckedContentClick()
    {
        var newEvent = new RoutedEventArgs(ToggleCheckedContentClickEvent, this);
        RaiseEvent(newEvent);
    }

    public static readonly RoutedEvent OpenedEvent =
        EventManager.RegisterRoutedEvent(
            "Opened",
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(RichToolTip));

    /// <summary>
    /// Raised when the popup is opened.
    /// </summary>
    public event RoutedEventHandler Opened
    {
        add { AddHandler(OpenedEvent, value); }
        remove { RemoveHandler(OpenedEvent, value); }
    }

    /// <summary>
    /// Raises <see cref="OpenedEvent"/>.
    /// </summary>
    protected virtual void OnOpened()
    {
        var newEvent = new RoutedEventArgs(OpenedEvent, this);
        RaiseEvent(newEvent);
    }

    public static readonly RoutedEvent ClosedEvent =
        EventManager.RegisterRoutedEvent(
            "Closed",
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(RichToolTip));

    /// <summary>
    /// Raised when the popup is closed.
    /// </summary>
    public event RoutedEventHandler Closed
    {
        add { AddHandler(ClosedEvent, value); }
        remove { RemoveHandler(ClosedEvent, value); }
    }

    /// <summary>
    /// Raises <see cref="ClosedEvent"/>.
    /// </summary>
    protected virtual void OnClosed()
    {
        var newEvent = new RoutedEventArgs(ClosedEvent, this);
        RaiseEvent(newEvent);
    }

    public override void OnApplyTemplate()
    {
        if (_toggleButton is not null)
        {
            _toggleButton.PreviewMouseLeftButtonUp -= ToggleButtonOnPreviewMouseLeftButtonUp;
        }

        base.OnApplyTemplate();

        _popup = GetTemplateChild(PopupPartName) as PopupEx;
        _toggleButton = GetTemplateChild(TogglePartName) as ToggleButton;

        _popup?.CommandBindings.Add(new CommandBinding(ClosePopupCommand, ClosePopupHandler));

        if (_toggleButton is not null)
        {
            _toggleButton.PreviewMouseLeftButtonUp += ToggleButtonOnPreviewMouseLeftButtonUp;
        }

        VisualStateManager.GoToState(this, IsPopupOpen ? PopupIsOpenStateName : PopupIsClosedStateName, false);
    }

    protected override void OnIsKeyboardFocusWithinChanged(DependencyPropertyChangedEventArgs e)
    {
        base.OnIsKeyboardFocusWithinChanged(e);

        if (IsPopupOpen && !IsKeyboardFocusWithin)
        {
            Close();
        }
    }

    private void ClosePopupHandler(object? sender, ExecutedRoutedEventArgs executedRoutedEventArgs)
        => IsPopupOpen = false;

    protected void Open()
    {
        if (!IsPopupOpen)
        {
            SetCurrentValue(IsPopupOpenProperty, true);
        }
    }

    protected void Close()
    {
        if (IsPopupOpen)
        {
            SetCurrentValue(IsPopupOpenProperty, false);
        }
    }

    private CustomPopupPlacement[] GetPopupPlacement(Size popupSize, Size targetSize, Point _)
    {
        double x, y;
        double offsetX = PopupHorizontalOffset;
        double offsetY = PopupVerticalOffset;

        Size popupSizeTransformed = new Size(DpiHelper.TransformFromDeviceX(this, popupSize.Width), DpiHelper.TransformFromDeviceY(this, popupSize.Height));
        Size targetSizeTransformed = new Size(DpiHelper.TransformFromDeviceX(this, targetSize.Width), DpiHelper.TransformFromDeviceY(this, targetSize.Height));

        switch (PlacementMode)
        {
            case RichToolTipPlacementMode.Bottom:
                x = (targetSizeTransformed.Width - popupSizeTransformed.Width) / 2 + offsetX - UseOffsetIfRtl(targetSizeTransformed.Width);
                y = targetSizeTransformed.Height + offsetY;
                break;
            case RichToolTipPlacementMode.Top:
                x = (targetSizeTransformed.Width - popupSizeTransformed.Width) / 2 + offsetX - UseOffsetIfRtl(targetSizeTransformed.Width);
                y = 0 - popupSizeTransformed.Height + offsetY;
                break;
            case RichToolTipPlacementMode.Left:
                x = 0 - popupSizeTransformed.Width + offsetX + UseOffsetIfRtl(popupSizeTransformed.Width);
                y = 0 - (popupSizeTransformed.Height - targetSizeTransformed.Height) / 2 + offsetY;
                break;
            case RichToolTipPlacementMode.Right:
                x = targetSizeTransformed.Width + offsetX - UseOffsetIfRtl(popupSizeTransformed.Width + targetSizeTransformed.Width * 2);
                y = 0 - (popupSizeTransformed.Height - targetSizeTransformed.Height) / 2 + offsetY;
                break;
            default:
                throw new ArgumentOutOfRangeException($"The enum value '{nameof(RichToolTipPlacementMode)}.{PlacementMode}' is not supported.");
        }

        double xTransformed = DpiHelper.TransformToDeviceX(this, x);
        double yTransformed = DpiHelper.TransformToDeviceY(this, y);

        _popupPointFromLastRequest = new Point(xTransformed, yTransformed);
        return [new CustomPopupPlacement(_popupPointFromLastRequest, PopupPrimaryAxis.Horizontal)];

        double UseOffsetIfRtl(double rtlOffset)
        {
            return FlowDirection == FlowDirection.LeftToRight ? 0 : rtlOffset;
        }
    }



    #region Capture

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern IntPtr GetCapture();

    private static void OnLostMouseCapture(object? sender, MouseEventArgs e)
    {
        if (sender is not RichToolTip richToolTip || Equals(Mouse.Captured, richToolTip))
        {
            return;
        }

        if (Equals(e.OriginalSource, richToolTip))
        {
            if (Mouse.Captured is null || richToolTip._popup is null)
            {
                if (!(Mouse.Captured as DependencyObject).IsDescendantOf(richToolTip._popup))
                {
                    richToolTip.Close();
                }
            }
        }
        else
        {
            if ((Mouse.Captured as DependencyObject).GetVisualAncestry().Contains(richToolTip._popup?.Child))
            {
                // Take capture if one of our children gave up capture (by closing their drop down)
                if (!richToolTip.IsPopupOpen || Mouse.Captured is not null || GetCapture() != IntPtr.Zero) return;

                Mouse.Capture(richToolTip, CaptureMode.SubTree);
                e.Handled = true;
            }
            else
            {
                if (richToolTip.IsPopupOpen)
                {
                    // allow scrolling
                    if (GetCapture() != IntPtr.Zero) return;

                    // Take capture back because click happened outside of control
                    Mouse.Capture(richToolTip, CaptureMode.SubTree);
                    e.Handled = true;
                }
                else
                {
                    richToolTip.Close();
                }
            }
        }
    }

    private static void OnMouseButtonDown(object sender, MouseButtonEventArgs e)
    {
        var richToolTip = (RichToolTip)sender;

        if (!richToolTip.IsKeyboardFocusWithin)
        {
            richToolTip.Focus();
        }

        e.Handled = true;

        if (Mouse.Captured == richToolTip && e.OriginalSource == richToolTip)
        {
            richToolTip.Close();
        }
    }
    #endregion

    private void ToggleButtonOnPreviewMouseLeftButtonUp(object? sender, MouseButtonEventArgs mouseButtonEventArgs)
    {
        if (IsPopupOpen) return;

        if (_popup is not null)
        {
            Open();
        }

        //if (_popup is not null)
        //{
        //    SetValue(IsPopupOpenProperty, true);
        //    OnToggleCheckedContentClick();

        //    if (ToggleCheckedContentCommand is not null
        //        && ToggleCheckedContentCommand.CanExecute(ToggleCheckedContentCommandParameter))
        //    {
        //        ToggleCheckedContentCommand.Execute(ToggleCheckedContentCommandParameter);
        //    }
        //}
        //mouseButtonEventArgs.Handled = true;
    }

    private static object CoerceToolTipIsEnabled(DependencyObject dependencyObject, object value)
    {
        var richToolTip = (RichToolTip)dependencyObject;
        return richToolTip.IsPopupOpen ? false : value;
    }
}
