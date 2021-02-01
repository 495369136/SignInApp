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
            infoDetail.Size16 = CommDef.Size16;
            infoDetail.Size18 = CommDef.Size18;
            infoDetail.Size20 = CommDef.Size20;
            infoDetail.Size27 = CommDef.Size27;
            infoDetail.Size50 = CommDef.Size50;
            infoDetail.Size55 = CommDef.Size55;
            infoDetail.Size277 = CommDef.Size277;
            this.DataContext = infoDetail;
        }
    }

    public class InfoDetail : NotificationBase
    {
        public double Size16 { get; set; }
        public double Size18 { get; set; }
        public double Size20 { get; set; }
        public double Size27 { get; set; }
        public double Size50 { get; set; }
        public double Size55 { get; set; }
        public double Size277 { get; set; }

        public BitmapImage InfoImage { get; set; }  //图像
        public string InfoTitle { get; set; }       //title
        public string InfoText { get; set; }        //详细
    }
}
