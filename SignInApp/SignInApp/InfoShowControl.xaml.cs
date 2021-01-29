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

namespace SignInApp
{
    /// <summary>
    /// InfoShowControl.xaml 的交互逻辑
    /// </summary>
    public partial class InfoShowControl : UserControl
    {
        public InfoShowControl(InfoDetail infoDetail)
        {
            InitializeComponent();
            this.DataContext = infoDetail;
        }
    }

    public class InfoDetail : NotificationBase
    {
        public BitmapImage InfoImage { get; set; }  //图像
        public string InfoTitle { get; set; }       //title
        public string InfoText { get; set; }        //详细
    }
}
