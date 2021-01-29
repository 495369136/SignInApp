using Newtonsoft.Json.Linq;
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
    public delegate void SelectClassHandler(ClassInfo classInfo);
    /// <summary>
    /// ClassRowControl.xaml 的交互逻辑
    /// </summary>
    public partial class ClassRowControl : UserControl
    {
        public event SelectClassHandler SelectClass;

        public ClassInfo mClassInfo = null;
        private SolidColorBrush mOrgBackground = null;

        public ClassRowControl(int index, ClassInfo classInfo)
        {
            InitializeComponent();

            this.Width = 805;
            mClassInfo = classInfo;
            this.DataContext = classInfo;

            if (index == 0)
            {
                ImageRight.Visibility = Visibility.Visible;
                mOrgBackground = new SolidColorBrush(Color.FromRgb(0xF8, 0xF9, 0xFC));
            }
            else if (index % 2 == 0)
                mOrgBackground = new SolidColorBrush(Color.FromRgb(0xF8, 0xF9, 0xFC));
            else
                mOrgBackground = new SolidColorBrush(Color.FromRgb(0xFF, 0xFF, 0xFF));
            this.Background = mOrgBackground;
        }

        private void MouseLeftButtonDownHandler(object sender, MouseButtonEventArgs e)
        {
            ImageRight.Visibility = Visibility.Visible;
            if (null != SelectClass)
                SelectClass(mClassInfo);
        }

        private void MouseUpHandler(object sender, MouseEventArgs e)
        {
            SolidColorBrush newBackground = new SolidColorBrush(Color.FromRgb(0xE4, 0xEE, 0xF8));
            this.Background = newBackground;
        }

        private void MouseLeaveHandler(object sender, MouseEventArgs e)
        {
            this.Background = mOrgBackground;
        }
    }

    public class ClassInfo
    {
        public long Id { get; set; }                    //培训班级ID
        public String ClassName { get; set; }           //培训班级名称
        public String StartTime { get; set; }           //培训开始时间
        public String EndTime { get; set; }             //培训结束时间
        public String ExamTypeName { get; set; }        //培训科目
        public String StudentNums { get; set; }         //培训班级人数
    }
}
