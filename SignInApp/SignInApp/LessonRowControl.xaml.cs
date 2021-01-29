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

            this.Width = 640;
            mLessonInfo = lessonInfo;
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
        public bool IsChecked { get; set; }             //课程ID
        public long Id { get; set; }                    //课程ID
        public long ClassId { get; set; }               //班级名称
        public String Name { get; set; }                //课程名称
        public String Teacher { get; set; }             //教师
        public String Number { get; set; }              //学时
        public String ClassName { get; set; }           //班级名称
    }
}
