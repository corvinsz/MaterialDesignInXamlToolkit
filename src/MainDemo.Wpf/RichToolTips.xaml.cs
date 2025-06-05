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

namespace MaterialDesignDemo
{
    /// <summary>
    /// Interaction logic for RichToolTips.xaml
    /// </summary>
    public partial class RichToolTips : UserControl
    {
        public RichToolTips()
        {
            InitializeComponent();

            foreach (var item in Enum.GetValues(typeof(MaterialDesignThemes.Wpf.Elevation)))
            {
                ElevationComboBox.Items.Add(item);
            }

            foreach (var item in Enum.GetValues(typeof(MaterialDesignThemes.Wpf.RichToolTipPlacementMode)))
            {
                RichToolTipPlacementMode.Items.Add(item);
            }

            foreach (var item in Enum.GetValues(typeof(PopupAnimation)))
            {
                PopupAnimation.Items.Add(item);
            }
        }
    }
}
