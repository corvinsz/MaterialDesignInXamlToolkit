using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using MaterialDesignColors.ColorManipulation;

namespace MaterialDesignThemes.Wpf.Converters;

public class HsbToMediaColorConverter : IMultiValueConverter
{
    public static readonly HsbToMediaColorConverter Instance = new();

    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values is not [double hue, double saturation, double brightness]) return Binding.DoNothing;

        return new Hsb(hue, saturation, brightness).ToColor();
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
