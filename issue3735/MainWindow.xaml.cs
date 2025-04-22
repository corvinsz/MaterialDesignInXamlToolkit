using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MaterialDesignThemes.Wpf;

namespace issue3735;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
///
public partial class MainWindow : Window
{
    public RootDialog RootDialog { get; } = new();

    public MainWindow()
    {
        InitializeComponent();

        Application.Current.Dispatcher.InvokeAsync(async () =>
        {
            await Delay();
            DialogHost.Show(RootDialog, "MainDialog");
            await Delay();
            RootDialog.OpenInnerDialog();
            await Delay();

            //RootDialog.CloseInnerDialog();
            DialogHost.Close("MainDialog");

            await Delay();
            DialogHost.Show(RootDialog, "MainDialog");
        });

        static Task Delay() => Task.Delay(2000);
    }
}
