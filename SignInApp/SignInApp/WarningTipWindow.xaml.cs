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
using System.Windows.Shapes;

namespace SignInApp
{
    /// <summary>
    /// WarningTipWindow.xaml 的交互逻辑
    /// </summary>
    public partial class WarningTipWindow : Window
    {
        public WarningTipWindow(string tipStr)
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            tip_value.Text = tipStr;
        }

        private void CloseClickHandler(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
