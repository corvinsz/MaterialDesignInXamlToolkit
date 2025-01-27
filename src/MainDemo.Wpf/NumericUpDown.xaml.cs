using CommunityToolkit.Mvvm.ComponentModel;

namespace MaterialDesignDemo;

public partial class NumericUpDown : UserControl
{
    public NumericUpDown()
    {
        InitializeComponent();
        this.DataContext = new NumericUpDownViewModel();
    }
}

internal class NumericUpDownViewModel : ObservableObject
{
    private int _myInt;
    public int MyInt
    {
        get => _myInt;
        set => SetProperty(ref _myInt, value);
    }

    private double _myDouble;
    public double MyDouble
    {
        get => _myDouble;
        set => SetProperty(ref _myDouble, value);
    }
}
