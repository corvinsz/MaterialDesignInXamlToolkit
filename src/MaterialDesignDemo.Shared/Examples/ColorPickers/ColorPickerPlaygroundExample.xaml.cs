using CommunityToolkit.Mvvm.ComponentModel;
using MaterialDesignThemes.Wpf;

namespace MaterialDesignDemo.Shared.Examples.ColorPickers;

/// <summary>
/// Interaction logic for ColorPickerPlaygroundExample.xaml
/// </summary>
public partial class ColorPickerPlaygroundExample : UserControl
{
    public ColorPickerPlaygroundExample()
    {
        DataContext = new ColorPickerPlaygroundExampleViewModel();
        InitializeComponent();
    }
}
public partial class ColorPickerPlaygroundExampleViewModel : ObservableObject
{
    public ColorPickerPlaygroundExampleViewModel()
    {
        SliderPositions =
        [
            new SliderPosition(PackIconKind.FormatAlignLeft, Dock.Left),
            new SliderPosition(PackIconKind.FormatAlignTop, Dock.Top),
            new SliderPosition(PackIconKind.FormatAlignRight, Dock.Right),
            new SliderPosition(PackIconKind.FormatAlignBottom, Dock.Bottom),
        ];
    }
    public IReadOnlyCollection<SliderPosition> SliderPositions { get; }

}

public class SliderPosition(PackIconKind icon, Dock dock)
{
    public PackIconKind Icon { get; } = icon;
    public Dock Dock { get; } = dock;
}
