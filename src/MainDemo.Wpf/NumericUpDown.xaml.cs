using System.Globalization;
using System.Threading;
using CommunityToolkit.Mvvm.ComponentModel;

namespace MaterialDesignDemo;

public partial class NumericUpDown : UserControl
{
    public NumericUpDown()
    {
        InitializeComponent();
        this.DataContext = new MyVM();

        var cult = new CultureInfo("en-EN");
        CultureInfo.CurrentUICulture = cult;
    }
}

public partial class MyVM : ObservableObject
{
    [ObservableProperty]
    private decimal _myDecimal;

    [ObservableProperty]
    private int _myInt;

    [ObservableProperty]
    private double _myDouble;
}
