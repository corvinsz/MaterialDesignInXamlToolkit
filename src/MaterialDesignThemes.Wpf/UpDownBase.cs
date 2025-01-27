using System.ComponentModel;
using System.Globalization;

namespace MaterialDesignThemes.Wpf;


#if NET8_0_OR_GREATER
using System.Numerics;
using System.Text.RegularExpressions;

public abstract class UpDownBase<T> : UpDownBase
    where T : INumber<T>, IMinMaxValue<T>
{
    private static readonly Type SelfType = typeof(UpDownBase<T>);

    private static UpDownBase<T> ToUpDownBase(DependencyObject dependencyObject) => (UpDownBase<T>)dependencyObject;

    private static T MinValue => T.MinValue;
    private static T MaxValue => T.MaxValue;
    private static T Zero => T.Zero;
    private static T One => T.One;
    private static T Max(T value1, T value2) => T.Max(value1, value2);
    private static T Clamp(T value, T min, T max) => T.Clamp(value, min, max);
    private static T Add(T value1, T value2) => value1 + value2;
    private static T Subtract(T value1, T value2) => value1 - value2;
    private static bool TryParse(string text, IFormatProvider? formatProvider, out T? value)
        => T.TryParse(text, formatProvider, out value);
    private static int Compare(T value1, T value2) => value1.CompareTo(value2);
#else
public class UpDownBase<T, TArithmetic> : UpDownBase
    where TArithmetic : IArithmetic<T>, new()
{
    private static readonly Type SelfType = typeof(UpDownBase<T, TArithmetic>);
    private static readonly TArithmetic _arithmetic = new();

    private static UpDownBase<T, TArithmetic> ToUpDownBase(DependencyObject dependencyObject) => (UpDownBase<T, TArithmetic>)dependencyObject;

    private static T MinValue => _arithmetic.MinValue();
    private static T MaxValue => _arithmetic.MaxValue();
    private static T Zero => _arithmetic.Zero();
    private static T One => _arithmetic.One();
    private static T Max(T value1, T value2) => _arithmetic.Max(value1, value2);
    private static T Clamp(T value, T min, T max) => _arithmetic.Max(_arithmetic.Min(value, max), min);
    private static T Add(T value1, T value2) => _arithmetic.Add(value1, value2);
    private static T Subtract(T value1, T value2) => _arithmetic.Subtract(value1, value2);
    private static bool TryParse(string text, IFormatProvider? formatProvider, out T? value)
        => _arithmetic.TryParse(text, formatProvider, out value);
    private static int Compare(T value1, T value2) => _arithmetic.Compare(value1, value2);
#endif

    #region DependencyProperties

    #region DependencyProperty : MinimumProperty

    public virtual T Minimum
    {
        get => (T)GetValue(MinimumProperty);
        set => SetValue(MinimumProperty, value);
    }
    public static readonly DependencyProperty MinimumProperty =
        DependencyProperty.Register(nameof(Minimum), typeof(T), SelfType, new PropertyMetadata(MinValue, OnMinimumChanged));

    private static void OnMinimumChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var ctrl = ToUpDownBase(d);
        ctrl.CoerceValue(MaximumProperty);
        ctrl.CoerceValue(ValueProperty);
    }

    #endregion DependencyProperty : MinimumProperty

    #region DependencyProperty : MaximumProperty

    public T Maximum
    {
        get => (T)GetValue(MaximumProperty);
        set => SetValue(MaximumProperty, value);
    }

    public static readonly DependencyProperty MaximumProperty =
        DependencyProperty.Register(nameof(Maximum), typeof(T), SelfType, new PropertyMetadata(MaxValue, OnMaximumChanged, CoerceMaximum));

    private static void OnMaximumChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var upDownBase = ToUpDownBase(d);
        upDownBase.CoerceValue(ValueProperty);
    }

    private static object? CoerceMaximum(DependencyObject d, object? value)
    {
        if (value is T numericValue)
        {
            var upDownBase = ToUpDownBase(d);
            return Max(upDownBase.Minimum, numericValue);
        }
        return value;
    }

    #endregion DependencyProperty : MaximumProperty

    #region DependencyProperty : ValueProperty
    public T Value
    {
        get => (T)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(nameof(Value),
                                        typeof(T),
                                        SelfType,
                                        new FrameworkPropertyMetadata(default(T), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnNumericValueChanged, CoerceNumericValue));

    private static void OnNumericValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var upDownBase = ToUpDownBase(d);
        var args = new RoutedPropertyChangedEventArgs<T>((T)e.OldValue, (T)e.NewValue)
        {
            RoutedEvent = ValueChangedEvent
        };
        upDownBase.RaiseEvent(args);

        if (upDownBase._textBoxField is { } textBox)
        {
            upDownBase.UpdateText();
            //textBox.Text = e.NewValue.ToString();
        }

        if (upDownBase._increaseButton is { } increaseButton)
        {
            increaseButton.IsEnabled = Compare(upDownBase.Value, upDownBase.Maximum) < 0;
        }

        if (upDownBase._decreaseButton is { } decreaseButton)
        {
            decreaseButton.IsEnabled = Compare(upDownBase.Value, upDownBase.Minimum) > 0;
        }
    }

    private static object? CoerceNumericValue(DependencyObject d, object? value)
    {
        if (value is T numericValue)
        {
            var upDownBase = ToUpDownBase(d);
            numericValue = Clamp(numericValue, upDownBase.Minimum, upDownBase.Maximum);
            return numericValue;
        }
        return value;
    }
    #endregion ValueProperty

    #region DependencyProperty : ValueStep
    /// <summary>
    /// The step of value for each increase or decrease
    /// </summary>
    public T ValueStep
    {
        get => (T)GetValue(ValueStepProperty);
        set => SetValue(ValueStepProperty, value);
    }

    public static readonly DependencyProperty ValueStepProperty =
        DependencyProperty.Register(nameof(ValueStep), typeof(T), SelfType, new PropertyMetadata(One));
    #endregion

    #region DependencyProperty : AllowChangeOnScroll

    public bool AllowChangeOnScroll
    {
        get => (bool)GetValue(AllowChangeOnScrollProperty);
        set => SetValue(AllowChangeOnScrollProperty, value);
    }

    public static readonly DependencyProperty AllowChangeOnScrollProperty =
        DependencyProperty.Register(nameof(AllowChangeOnScroll), typeof(bool), SelfType, new PropertyMetadata(false));

    #endregion


    public string StringFormat
    {
        get => (string)GetValue(StringFormatProperty);
        set => SetValue(StringFormatProperty, value);
    }

    public static readonly DependencyProperty StringFormatProperty
        = DependencyProperty.Register(nameof(StringFormat),
                                      typeof(string),
                                      SelfType,
                                      new FrameworkPropertyMetadata(string.Empty, OnStringFormatPropertyChanged, CoerceStringFormat));

    private static void OnStringFormatPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var upDownBase = ToUpDownBase(d);
        if (e.OldValue != e.NewValue && upDownBase is not null)
        {
            if (upDownBase._textBoxField is not null)
            {
                upDownBase.UpdateText();
                //upDownBase._textBoxField.Text = FormattedValueString(newValue, this.StringFormat, CultureInfo.CurrentUICulture);
            }

            //if (e.NewValue is string format && !string.IsNullOrEmpty(format) && RegexStringFormatHexadecimal.IsMatch(format))
            //{
            //    numericUpDown.SetCurrentValue(ParsingNumberStyleProperty, NumberStyles.HexNumber);
            //    numericUpDown.SetCurrentValue(NumericInputModeProperty, numericUpDown.NumericInputMode | NumericInput.Decimal);
            //}
        }
    }

    private static object CoerceStringFormat(DependencyObject d, object? baseValue)
    {
        return baseValue ?? string.Empty;
    }

    private void UpdateText()
    {
        if (_textBoxField is not null)
        {
            _textBoxField.Text = GetFormattedValueString(Value, StringFormat);
        }
    }

    private static readonly System.Text.RegularExpressions.Regex _regexStringFormat =
        new(@"\{0\s*(:(?<format>.*))?\}", System.Text.RegularExpressions.RegexOptions.Compiled);
    private string? GetFormattedValueString(T newValue, string format, CultureInfo culture = null)
    {
        culture ??= CultureInfo.CurrentCulture;
        format = format.Replace("{}", string.Empty);
        if (!string.IsNullOrWhiteSpace(format))
        {
            var match = _regexStringFormat.Match(format);
            if (match.Success)
            {
                // we have a format template such as "{0:N0}"
                return string.Format(culture, format, newValue);
            }

            // we have a format such as "N0"
            //return newValue.ToString(format, culture);
        }

        return newValue.ToString();
        //return newValue.ToString(culture);
    }

    private void OnTextChanged(object sender, TextChangedEventArgs e)
    {
        var text = ((TextBox)sender).Text;
        this.ChangeValueFromTextInput(text);
    }

    private void ChangeValueFromTextInput(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            this.SetCurrentValue(ValueProperty, null);
            return;
        }

        if (this.ValidateText(text, out var convertedValue))
        {
            convertedValue = GetFormattedValueString(convertedValue, this.StringFormat, CultureInfo.CurrentUICulture);
            this.SetValueTo(convertedValue);
        }
        else if (this.DefaultValue.HasValue)
        {
            this.SetValueTo(this.DefaultValue.Value);
            this.InternalSetText(this.Value);
        }
        else
        {
            this.SetCurrentValue(ValueProperty, null);
        }
        this.OnValueChanged(this.Value, this.Value);

        this.manualChange = false;
    }

    private bool ValidateText(string text, out T convertedValue)
    {
        convertedValue = T.Zero;

        if (text == CultureInfo.CurrentUICulture.NumberFormat.PositiveSign
            || text == CultureInfo.CurrentUICulture.NumberFormat.NegativeSign)
        {
            return true;
        }

        if (text.Count(c => c == CultureInfo.CurrentUICulture.NumberFormat.PositiveSign[0]) > 2
            || text.Count(c => c == CultureInfo.CurrentUICulture.NumberFormat.NegativeSign[0]) > 2
           // || text.Count(c => c == CultureInfo.CurrentUICulture.NumberFormat.NumberGroupSeparator[0]) > 1
           )
        {
            return false;
        }

        var number = this.TryGetNumberFromText(text);

        // If we are only accepting numbers then attempt to parse as an integer.
        if (this.GetType() == typeof(NumericUpDown))
        {
            return this.ConvertNumber(number, out convertedValue);
        }

        if (number == CultureInfo.CurrentUICulture.NumberFormat.NumberDecimalSeparator
            || number == CultureInfo.CurrentUICulture.NumberFormat.CurrencyDecimalSeparator
            || number == CultureInfo.CurrentUICulture.NumberFormat.PercentDecimalSeparator
            || number == (CultureInfo.CurrentUICulture.NumberFormat.NegativeSign + CultureInfo.CurrentUICulture.NumberFormat.NumberDecimalSeparator)
            || number == (CultureInfo.CurrentUICulture.NumberFormat.PositiveSign + CultureInfo.CurrentUICulture.NumberFormat.NumberDecimalSeparator)
           )
        {
            return true;
        }

        if (!double.TryParse(number, this.ParsingNumberStyle, CultureInfo.CurrentUICulture, out convertedValue))
        {
            return false;
        }

        return true;
    }

    protected abstract void ConvertToConcreteType(string text);


    private const string RawRegexNumberString = @"[-+]?(?<![0-9][<DecimalSeparator><GroupSeparator>])[<DecimalSeparator><GroupSeparator>]?[0-9]+(?:[<DecimalSeparator><GroupSeparator>\s][0-9]+)*[<DecimalSeparator><GroupSeparator>]?[0-9]?(?:[eE][-+]?[0-9]+)?(?!\.[0-9])";
    private static System.Text.RegularExpressions.Regex? _regexNumber = null;
    private string TryGetNumberFromText(string text)
    {
        if (_regexNumber is null)
        {
            _regexNumber = new System.Text.RegularExpressions.Regex(RawRegexNumberString.Replace("<DecimalSeparator>", CultureInfo.CurrentUICulture.NumberFormat.NumberDecimalSeparator)
                                                                                        .Replace("<GroupSeparator>", CultureInfo.CurrentUICulture.NumberFormat.NumberGroupSeparator),
                                         System.Text.RegularExpressions.RegexOptions.Compiled);
        }

        var matches = _regexNumber.Matches(text);
        return matches.Count > 0 ? matches[0].Value : text;
    }

    private bool ConvertNumber(string text, out double convertedValue)
    {
        if (text.Any(c => c == CultureInfo.CurrentUICulture.NumberFormat.NumberDecimalSeparator[0]
                       || c == CultureInfo.CurrentUICulture.NumberFormat.PercentDecimalSeparator[0]
                       || c == CultureInfo.CurrentUICulture.NumberFormat.CurrencyDecimalSeparator[0]))
        {
            convertedValue = 0d;
            return false;
        }

        if (!long.TryParse(text, NumberStyles.Any, CultureInfo.CurrentUICulture, out var convertedInt))
        {
            convertedValue = convertedInt;
            return false;
        }

        convertedValue = convertedInt;
        return true;
    }

    #endregion DependencyProperties

    #region Event : ValueChangedEvent
    [Category("Behavior")]
    public static readonly RoutedEvent ValueChangedEvent = EventManager.RegisterRoutedEvent(nameof(ValueChanged), RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<T>), SelfType);

    public event RoutedPropertyChangedEventHandler<T> ValueChanged
    {
        add => AddHandler(ValueChangedEvent, value);
        remove => RemoveHandler(ValueChangedEvent, value);
    }
    #endregion Event : ValueChangedEvent

    public override void OnApplyTemplate()
    {
        if (_increaseButton != null)
            _increaseButton.Click -= IncreaseButtonOnClick;

        if (_decreaseButton != null)
            _decreaseButton.Click -= DecreaseButtonOnClick;
        if (_textBoxField != null)
            _textBoxField.TextChanged -= OnTextBoxFocusLost;

        base.OnApplyTemplate();

        if (_increaseButton != null)
            _increaseButton.Click += IncreaseButtonOnClick;

        if (_decreaseButton != null)
            _decreaseButton.Click += DecreaseButtonOnClick;

        if (_textBoxField != null)
        {
            _textBoxField.LostFocus += OnTextBoxFocusLost;
            this.UpdateText();
        }

    }

    private void OnTextBoxFocusLost(object sender, EventArgs e)
    {
        if (_textBoxField is { } textBoxField)
        {
            _textBoxField.TextChanged -= this.OnTextChanged;
            this.InternalSetText(this.Value);
            _textBoxField.TextChanged += this.OnTextChanged;
            UpdateText();
            //if (TryParse(textBoxField.Text, CultureInfo.CurrentUICulture, out T? value))
            //{
            //    SetCurrentValue(ValueProperty, value);
            //}
            //else
            //{
            //    textBoxField.Text = Value?.ToString();
            //}
        }
    }

    private void IncreaseButtonOnClick(object sender, RoutedEventArgs e) => OnIncrease();

    private void DecreaseButtonOnClick(object sender, RoutedEventArgs e) => OnDecrease();

    private void OnIncrease() => SetCurrentValue(ValueProperty, Clamp(Add(Value, ValueStep), Minimum, Maximum));

    private void OnDecrease() => SetCurrentValue(ValueProperty, Clamp(Subtract(Value, ValueStep), Minimum, Maximum));

    protected override void OnPreviewKeyDown(KeyEventArgs e)
    {
        if (e.Key == Key.Up)
        {
            OnIncrease();
            e.Handled = true;
        }
        else if (e.Key == Key.Down)
        {
            OnDecrease();
            e.Handled = true;
        }
        base.OnPreviewKeyDown(e);
    }

    protected override void OnPreviewMouseWheel(MouseWheelEventArgs e)
    {
        if (IsKeyboardFocusWithin && AllowChangeOnScroll)
        {
            if (e.Delta > 0)
            {
                OnIncrease();
            }
            else if (e.Delta < 0)
            {
                OnDecrease();
            }
            e.Handled = true;
        }
        base.OnPreviewMouseWheel(e);
    }
}

[TemplatePart(Name = IncreaseButtonPartName, Type = typeof(RepeatButton))]
[TemplatePart(Name = DecreaseButtonPartName, Type = typeof(RepeatButton))]
[TemplatePart(Name = TextBoxPartName, Type = typeof(TextBox))]
public abstract class UpDownBase : Control
{
    public const string IncreaseButtonPartName = "PART_IncreaseButton";
    public const string DecreaseButtonPartName = "PART_DecreaseButton";
    public const string TextBoxPartName = "PART_TextBox";

    protected TextBox? _textBoxField;
    protected RepeatButton? _decreaseButton;
    protected RepeatButton? _increaseButton;

    static UpDownBase()
    {
        EventManager.RegisterClassHandler(typeof(UpDownBase), GotFocusEvent, new RoutedEventHandler(OnGotFocus));
    }

    // Based on work in MahApps
    // https://github.com/MahApps/MahApps.Metro/blob/f7ba30586e9670f07c2f7b6553d129a9e32fc673/src/MahApps.Metro/Controls/NumericUpDown.cs#L966
    private static void OnGotFocus(object sender, RoutedEventArgs e)
    {
        // When NumericUpDown gets logical focus, select the text inside us.
        // If we're an editable NumericUpDown, forward focus to the TextBox element
        if (!e.Handled)
        {
            var numericUpDown = (UpDownBase)sender;
            if (numericUpDown.Focusable && e.OriginalSource == numericUpDown)
            {
                // MoveFocus takes a TraversalRequest as its argument.
                var focusDirection = Keyboard.Modifiers.HasFlag(ModifierKeys.Shift)
                    ? FocusNavigationDirection.Previous
                    : FocusNavigationDirection.Next;

                var request = new TraversalRequest(focusDirection);
                // Gets the element with keyboard focus.
                // And change the keyboard focus.
                if (Keyboard.FocusedElement is UIElement elementWithFocus)
                {
                    elementWithFocus.MoveFocus(request);
                }
                else
                {
                    numericUpDown.Focus();
                }

                e.Handled = true;
            }
        }
    }

    public override void OnApplyTemplate()
    {
        _increaseButton = GetTemplateChild(IncreaseButtonPartName) as RepeatButton;
        _decreaseButton = GetTemplateChild(DecreaseButtonPartName) as RepeatButton;
        _textBoxField = GetTemplateChild(TextBoxPartName) as TextBox;

        base.OnApplyTemplate();
    }

    public void SelectAll() => _textBoxField?.SelectAll();

    public object? IncreaseContent
    {
        get => GetValue(IncreaseContentProperty);
        set => SetValue(IncreaseContentProperty, value);
    }

    // Using a DependencyProperty as the backing store for IncreaseContent.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty IncreaseContentProperty =
        DependencyProperty.Register(nameof(IncreaseContent), typeof(object), typeof(UpDownBase), new PropertyMetadata(null));

    public object? DecreaseContent
    {
        get => GetValue(DecreaseContentProperty);
        set => SetValue(DecreaseContentProperty, value);
    }

    // Using a DependencyProperty as the backing store for DecreaseContent.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty DecreaseContentProperty =
        DependencyProperty.Register(nameof(DecreaseContent), typeof(object), typeof(UpDownBase), new PropertyMetadata(null));


}

#if !NET8_0_OR_GREATER
public interface IArithmetic<T>
{
    T Add(T value1, T value2);

    T Subtract(T value1, T value2);

    int Compare(T value1, T value2);

    T MinValue();

    T MaxValue();

    T Zero();

    T One();

    T Max(T value1, T value2);

    T Min(T value1, T value2);

    bool TryParse(string text, IFormatProvider? formatProvider, out T? value);
}
#endif
