using System;
using System.Windows;
using System.Windows.Controls;

namespace SignInApp
{
    public delegate void DeletedSignUpHandler(SignUpInfo signUpInfo);
    
    /// <summary>
    /// SignUpControl.xaml 的交互逻辑
    /// </summary>
    public partial class SignUpControl : UserControl
    {
        public event DeletedSignUpHandler DeletedSignUp;

        public SignUpInfo mSignUpInfo = null;

        public SignUpControl(int index, SignUpInfo signUpInfo)
        {
            InitializeComponent();
            mSignUpInfo = signUpInfo;
            this.Width = 1512;
            this.DataContext = signUpInfo;
        }

        private void DeleteClickHandler(object sender, RoutedEventArgs e)
        {
            if (null != DeletedSignUp)
                DeletedSignUp(mSignUpInfo);
        }
    }

    public class SignUpInfo
    {
        public String Id { get; set; }                    //课程ID
        public String StudentName { get; set; }         //姓名
        public String IdCardNum { get; set; }           //身份证
        public String Type { get; set; }                //类型
        public String Sex { get; set; }                 //性别
        public String CheckTime { get; set; }           //签到时间
        public String CourseName { get; set; }          //课程名称
    }
}
