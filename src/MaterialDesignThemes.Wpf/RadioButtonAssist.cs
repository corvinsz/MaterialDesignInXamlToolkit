namespace MaterialDesignThemes.Wpf
{
    /// <summary>
    /// Provides attached properties for configuring radio button appearance.
    /// </summary>
    public class RadioButtonAssist
    {
        private const double DefaultRadioButtonSize = 18.0;

        #region AttachedProperty : RadioButtonSizeProperty
        /// <summary>
        /// Defines the size of the radio button.
        /// </summary>
        public static readonly DependencyProperty RadioButtonSizeProperty =
            DependencyProperty.RegisterAttached(
                "RadioButtonSize",
                typeof(double),
                typeof(RadioButtonAssist),
                new PropertyMetadata(DefaultRadioButtonSize)
            );

        public static double GetRadioButtonSize(RadioButton element) =>
            (double)element.GetValue(RadioButtonSizeProperty);

        public static void SetRadioButtonSize(RadioButton element, double checkBoxSize) =>
            element.SetValue(RadioButtonSizeProperty, checkBoxSize);
        #endregion
    }
}
