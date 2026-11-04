using System;
using System.Windows.Media;
using MaterialDesignColors.ColorManipulation;

namespace MaterialDesignThemes.Wpf;

[TemplatePart(Name = HueSliderPartName, Type = typeof(Slider))]
[TemplatePart(Name = AlphaSliderPartName, Type = typeof(Slider))]
[TemplatePart(Name = SaturationBrightnessPickerPartName, Type = typeof(Canvas))]
[TemplatePart(Name = SaturationBrightnessPickerThumbPartName, Type = typeof(Thumb))]
public class ColorPicker : Control
{
    public const string HueSliderPartName = "PART_HueSlider";
    public const string AlphaSliderPartName = "PART_AlphaSlider";
    public const string InputPartName = "PART_Input";
    public const string SaturationBrightnessPickerPartName = "PART_SaturationBrightnessPicker";
    public const string SaturationBrightnessPickerThumbPartName = "PART_SaturationBrightnessPickerThumb";

    static ColorPicker()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorPicker), new FrameworkPropertyMetadata(typeof(ColorPicker)));
    }

    private Thumb? _saturationBrightnessThumb;
    private Canvas? _saturationBrightnessCanvas;
    private Slider? _hueSlider;
    private TextBox? _input;

    public static readonly DependencyProperty ColorProperty = DependencyProperty.Register(nameof(Color), typeof(Color), typeof(ColorPicker),
        new FrameworkPropertyMetadata(default(Color), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, ColorPropertyChangedCallback));

    public Color Color
    {
        get => (Color)GetValue(ColorProperty);
        set => SetValue(ColorProperty, value);
    }

    private static void ColorPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var colorPicker = (ColorPicker)d;

        if (!colorPicker._inCallback)
        {
            colorPicker._inCallback = true;
            var color = (Color)e.NewValue;
            colorPicker.SetCurrentValue(HsbProperty, color.ToHsb());
            colorPicker.SetCurrentValue(AlphaProperty, (double)color.A);
            colorPicker._inCallback = false;
        }

        colorPicker.UpdateInputText();

        var args = new RoutedPropertyChangedEventArgs<Color>(
            (Color)e.OldValue,
            (Color)e.NewValue)
        { RoutedEvent = ColorChangedEvent };
        colorPicker.RaiseEvent(args);
    }

    public static readonly RoutedEvent ColorChangedEvent =
        EventManager.RegisterRoutedEvent(
            nameof(Color),
            RoutingStrategy.Bubble,
            typeof(RoutedPropertyChangedEventHandler<Color>),
            typeof(ColorPicker));

    public event RoutedPropertyChangedEventHandler<Color> ColorChanged
    {
        add => AddHandler(ColorChangedEvent, value);
        remove => RemoveHandler(ColorChangedEvent, value);
    }

    internal static readonly DependencyProperty HsbProperty = DependencyProperty.Register(nameof(Hsb), typeof(Hsb), typeof(ColorPicker),
        new FrameworkPropertyMetadata(default(Hsb), FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, HsbPropertyChangedCallback));

    internal Hsb Hsb
    {
        get => (Hsb)GetValue(HsbProperty);
        set => SetValue(HsbProperty, value);
    }

    private bool _inCallback;
    private static void HsbPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var colorPicker = (ColorPicker)d;
        if (colorPicker._inCallback)
        {
            return;
        }

        colorPicker._inCallback = true;

        var color = default(Color);
        if (e.NewValue is Hsb hsb)
        {
            color = hsb.ToColor();
            color.A = ToByte(colorPicker.Alpha);
        }
        colorPicker.SetCurrentValue(ColorProperty, color);

        colorPicker._inCallback = false;
    }

    public static readonly DependencyProperty AlphaProperty = DependencyProperty.Register(
        nameof(Alpha), typeof(double), typeof(ColorPicker),
        new FrameworkPropertyMetadata(255d, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, AlphaPropertyChangedCallback));

    public double Alpha
    {
        get => (double)GetValue(AlphaProperty);
        set => SetValue(AlphaProperty, value);
    }

    private static void AlphaPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var colorPicker = (ColorPicker)d;
        if (colorPicker._inCallback)
        {
            return;
        }

        colorPicker._inCallback = true;

        var color = colorPicker.Hsb.ToColor();
        color.A = ToByte(e.NewValue);
        colorPicker.SetCurrentValue(ColorProperty, color);

        colorPicker._inCallback = false;
    }

    public static readonly DependencyProperty HueSliderPositionProperty = DependencyProperty.Register(
        nameof(HueSliderPosition), typeof(Dock), typeof(ColorPicker), new PropertyMetadata(Dock.Bottom));

    public Dock HueSliderPosition
    {
        get => (Dock)GetValue(HueSliderPositionProperty);
        set => SetValue(HueSliderPositionProperty, value);
    }

    public static readonly DependencyProperty AlphaSliderPositionProperty = DependencyProperty.Register(
        nameof(AlphaSliderPosition), typeof(Dock), typeof(ColorPicker), new PropertyMetadata(Dock.Bottom));

    public Dock AlphaSliderPosition
    {
        get => (Dock)GetValue(AlphaSliderPositionProperty);
        set => SetValue(AlphaSliderPositionProperty, value);
    }

    public static readonly DependencyProperty ShowAlphaSliderProperty = DependencyProperty.Register(
        nameof(ShowAlphaSlider), typeof(bool), typeof(ColorPicker), new PropertyMetadata(true));

    public bool ShowAlphaSlider
    {
        get => (bool)GetValue(ShowAlphaSliderProperty);
        set => SetValue(ShowAlphaSliderProperty, value);
    }

    public static readonly DependencyProperty ShowPreviewProperty = DependencyProperty.Register(
        nameof(ShowPreview), typeof(bool), typeof(ColorPicker), new PropertyMetadata(true));

    public bool ShowPreview
    {
        get => (bool)GetValue(ShowPreviewProperty);
        set => SetValue(ShowPreviewProperty, value);
    }

    public static readonly DependencyProperty ShowInputProperty = DependencyProperty.Register(
        nameof(ShowInput), typeof(bool), typeof(ColorPicker), new PropertyMetadata(true));

    public bool ShowInput
    {
        get => (bool)GetValue(ShowInputProperty);
        set => SetValue(ShowInputProperty, value);
    }

    public override void OnApplyTemplate()
    {
        if (_saturationBrightnessCanvas != null)
        {
            _saturationBrightnessCanvas.MouseDown -= SaturationBrightnessCanvasMouseDown;
            _saturationBrightnessCanvas.MouseMove -= SaturationBrightnessCanvasMouseMove;
            _saturationBrightnessCanvas.MouseUp -= SaturationBrightnessCanvasMouseUp;
        }
        _saturationBrightnessCanvas = GetTemplateChild(SaturationBrightnessPickerPartName) as Canvas;
        if (_saturationBrightnessCanvas != null)
        {
            _saturationBrightnessCanvas.MouseDown += SaturationBrightnessCanvasMouseDown;
            _saturationBrightnessCanvas.MouseMove += SaturationBrightnessCanvasMouseMove;
            _saturationBrightnessCanvas.MouseUp += SaturationBrightnessCanvasMouseUp;
        }

        if (_saturationBrightnessThumb != null) _saturationBrightnessThumb.DragDelta -= SaturationBrightnessThumbDragDelta;
        _saturationBrightnessThumb = GetTemplateChild(SaturationBrightnessPickerThumbPartName) as Thumb;
        if (_saturationBrightnessThumb != null) _saturationBrightnessThumb.DragDelta += SaturationBrightnessThumbDragDelta;

        if (_hueSlider != null)
        {
            _hueSlider.ValueChanged -= HueSliderOnValueChanged;
        }
        _hueSlider = GetTemplateChild(HueSliderPartName) as Slider;
        if (_hueSlider != null)
        {
            _hueSlider.ValueChanged += HueSliderOnValueChanged;
        }

        if (_input != null)
        {
            _input.LostFocus -= InputOnLostFocus;
        }
        _input = GetTemplateChild(InputPartName) as TextBox;
        if (_input != null)
        {
            _input.LostFocus += InputOnLostFocus;
            UpdateInputText();
        }

        base.OnApplyTemplate();
    }

    private void HueSliderOnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        if (Hsb is Hsb hsb)
        {
            Hsb = new Hsb(e.NewValue, hsb.Saturation, hsb.Brightness);
        }
    }

    private void SaturationBrightnessCanvasMouseDown(object sender, MouseButtonEventArgs e)
    {
        _saturationBrightnessThumb?.CaptureMouse();
    }

    private void SaturationBrightnessCanvasMouseMove(object sender, MouseEventArgs e)
    {
        if (Mouse.Captured is null || Mouse.Captured != _saturationBrightnessThumb)
            return;

        if (e.LeftButton == MouseButtonState.Pressed)
        {
            var position = e.GetPosition(_saturationBrightnessCanvas);
            ApplyThumbPosition(position.X, position.Y);
        }
    }

    private void SaturationBrightnessCanvasMouseUp(object sender, MouseButtonEventArgs e)
    {
        _saturationBrightnessThumb?.ReleaseMouseCapture();
    }

    protected override Size ArrangeOverride(Size arrangeBounds)
    {
        var result = base.ArrangeOverride(arrangeBounds);
        SetThumbLeft();
        SetThumbTop();
        return result;
    }

    private void SaturationBrightnessThumbDragDelta(object sender, DragDeltaEventArgs e)
    {
        var position = Mouse.GetPosition(_saturationBrightnessCanvas);
        ApplyThumbPosition(position.X, position.Y);
    }

    private void ApplyThumbPosition(double left, double top)
    {
        if (left < 0) left = 0;
        if (top < 0) top = 0;

        if (left > _saturationBrightnessCanvas?.ActualWidth)
        {
            left = _saturationBrightnessCanvas.ActualWidth;
        }
        if (top > _saturationBrightnessCanvas?.ActualHeight)
        {
            top = _saturationBrightnessCanvas.ActualHeight;
        }

        double saturation = (1 / (_saturationBrightnessCanvas?.ActualWidth / left)) ?? 0;
        double brightness = (1 - (top / _saturationBrightnessCanvas?.ActualHeight)) ?? 0;

        SetCurrentValue(HsbProperty, new Hsb(Hsb.Hue, saturation, brightness));
    }

    private void SetThumbLeft()
    {
        if (_saturationBrightnessCanvas is null) return;
        var left = (_saturationBrightnessCanvas.ActualWidth) / (1 / Hsb.Saturation);
        Canvas.SetLeft(_saturationBrightnessThumb, left);
    }

    private void SetThumbTop()
    {
        if (_saturationBrightnessCanvas is null) return;
        var top = ((1 - Hsb.Brightness) * _saturationBrightnessCanvas.ActualHeight);
        Canvas.SetTop(_saturationBrightnessThumb, top);
    }

    private void InputOnLostFocus(object sender, RoutedEventArgs e)
    {
        if (_input is null)
        {
            return;
        }

        Color oldColor = Color;

        try
        {
            object? convertedColor = ColorConverter.ConvertFromString(_input.Text);
            if (convertedColor is Color converted)
            {
                SetCurrentValue(ColorProperty, converted);
                return;
            }
        }
        catch
        {
        }

        _input.Text = oldColor.ToString();
    }

    private void UpdateInputText()
    {
        if (_input is null || _input.IsKeyboardFocusWithin)
        {
            return;
        }

        string colorAsString = Color.ToString();
        if (!string.Equals(_input.Text, colorAsString, StringComparison.OrdinalIgnoreCase))
        {
            _input.Text = colorAsString;
        }
    }

    private static byte ToByte(object? value)
    {
        if (value is not double alpha)
        {
            return 0;
        }

        if (alpha < byte.MinValue) alpha = byte.MinValue;
        if (alpha > byte.MaxValue) alpha = byte.MaxValue;

        return (byte)alpha;
    }
}
