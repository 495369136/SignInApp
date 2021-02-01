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
    /// SelectedLessonControl.xaml 的交互逻辑
    /// </summary>
    public partial class SelectedLessonControl : UserControl
    {
        public SelectedLessonControl(LessonInfo lessonInfo)
        {
            InitializeComponent();
            
            int width = (int)SystemParameters.WorkArea.Width;
            this.Width = width - CommDef.Size380;

            lessonInfo.Size8 = CommDef.Size8;
            lessonInfo.Size18 = CommDef.Size18;
            lessonInfo.Size24 = CommDef.Size24;
            lessonInfo.Size35 = CommDef.Size35;
            lessonInfo.Size48 = CommDef.Size48;
            this.DataContext = lessonInfo;
            //this.Width = 1535;
        }
    }
}
