using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
/// Interaction logic for RootDialog.xaml
/// </summary>
public partial class RootDialog : UserControl
{
    public RootDialog()
    {
        InitializeComponent();
    }

    internal void CloseInnerDialog() => DialogHost.Close("ucDialogHost");

    internal void OpenInnerDialog()
    {
        var tb = new TextBlock
        {
            Text = "Inner dialog content",
            Margin = new Thickness(16)
        };
        DialogHost.Show(tb, "ucDialogHost");
    }
}
