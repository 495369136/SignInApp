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
    public delegate void SelectLessonHandler(long lessonId, bool isSelecte, LessonInfo lessonInfo);
    /// <summary>
    /// LessonRowControl.xaml 的交互逻辑
    /// </summary>
    public partial class LessonRowControl : UserControl
    {
        public event SelectLessonHandler SelectLesson;
        private LessonInfo mLessonInfo = null;

        public LessonRowControl(int index, LessonInfo lessonInfo)
        {
            InitializeComponent();

            //this.Width = 640;
            this.Height = CommDef.Size60;
            mLessonInfo = lessonInfo;

            mLessonInfo.Size18 = CommDef.Size18;
            mLessonInfo.Size35 = CommDef.Size35;
            mLessonInfo.Size60 = CommDef.Size60;
            mLessonInfo.Size90 = CommDef.Size90;
            mLessonInfo.Size100 = CommDef.Size100;
            mLessonInfo.Size120 = CommDef.Size120;
            this.DataContext = lessonInfo;
        }

        private void CheckBoxClickHandler(object sender, RoutedEventArgs e)
        {
            if (checkbox.IsChecked == false) {
                SelectLesson(mLessonInfo.Id, false, null);
            } else {
                SelectLesson(mLessonInfo.Id, true, mLessonInfo);
            }
        }
    }

    public class LessonInfo
    {
        public double Size8 { get; set; }
        public double Size18 { get; set; }
        public double Size24 { get; set; }
        public double Size35 { get; set; }
        public double Size48 { get; set; }
        public double Size60 { get; set; }
        public double Size90 { get; set; }
        public double Size100 { get; set; }
        public double Size120 { get; set; }

        public bool IsChecked { get; set; }             //课程ID
        public long Id { get; set; }                    //课程ID
        public long ClassId { get; set; }               //班级名称
        public String Name { get; set; }                //课程名称
        public String Teacher { get; set; }             //教师
        public String Number { get; set; }              //学时
        public String ClassName { get; set; }           //班级名称
    }
}
