using Log;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RESTApi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SignInApp
{
    public delegate void CancelSelectHandler();
    public delegate void ConfirmSelectHandler(Dictionary<long, LessonInfo> lessonList);

    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private int mClassStauts = 1; //1 上课 2 下课
        private SolidColorBrush mGreen = null;
        private string mClassCourse = null;
        private Dictionary<long, LessonInfo> mSelectLessons = new Dictionary<long, LessonInfo>();

        private MainWindowFixInfo mainWindowFixInfo = new MainWindowFixInfo();

        public MainWindow(Org org)
        {
            InitializeComponent();

            int height = (int)SystemParameters.WorkArea.Height;
            int width = (int)SystemParameters.WorkArea.Width;
            this.Width = width;
            this.Height = height;

            CommDef.ScreenMultiple = 1080 / (double)height;
            CommDef.Size8 /= CommDef.ScreenMultiple;
            CommDef.Size14 /= CommDef.ScreenMultiple;
            CommDef.Size16 /= CommDef.ScreenMultiple;
            CommDef.Size18 /= CommDef.ScreenMultiple;
            CommDef.Size20 /= CommDef.ScreenMultiple;
            CommDef.Size22 /= CommDef.ScreenMultiple;
            CommDef.Size24 /= CommDef.ScreenMultiple;
            CommDef.Size26 /= CommDef.ScreenMultiple;
            CommDef.Size27 /= CommDef.ScreenMultiple;
            CommDef.Size30 /= CommDef.ScreenMultiple;
            CommDef.Size35 /= CommDef.ScreenMultiple;
            CommDef.Size40 /= CommDef.ScreenMultiple;
            CommDef.Size48 /= CommDef.ScreenMultiple;
            CommDef.Size50 /= CommDef.ScreenMultiple;
            CommDef.Size52 /= CommDef.ScreenMultiple;
            CommDef.Size55 /= CommDef.ScreenMultiple;
            CommDef.Size60 /= CommDef.ScreenMultiple;
            CommDef.Size80 /= CommDef.ScreenMultiple;
            CommDef.Size86 /= CommDef.ScreenMultiple;
            CommDef.Size88 /= CommDef.ScreenMultiple;
            CommDef.Size90 /= CommDef.ScreenMultiple;
            CommDef.Size100 /= CommDef.ScreenMultiple;
            CommDef.Size120 /= CommDef.ScreenMultiple;
            CommDef.Size140 /= CommDef.ScreenMultiple;
            CommDef.Size180 /= CommDef.ScreenMultiple;
            CommDef.Size200 /= CommDef.ScreenMultiple;
            CommDef.Size213 /= CommDef.ScreenMultiple;
            CommDef.Size240 /= CommDef.ScreenMultiple;
            CommDef.Size277 /= CommDef.ScreenMultiple;
            CommDef.Size380 /= CommDef.ScreenMultiple;

            mainWindowFixInfo.ClassFixSize = 0;
            mainWindowFixInfo.Size14 = CommDef.Size14;
            mainWindowFixInfo.Size18 = CommDef.Size18;
            mainWindowFixInfo.Size30 = CommDef.Size30;
            mainWindowFixInfo.Size20 = CommDef.Size20;
            mainWindowFixInfo.Size22 = CommDef.Size22;
            mainWindowFixInfo.Size24 = CommDef.Size24;
            mainWindowFixInfo.Size26 = CommDef.Size26;
            mainWindowFixInfo.Size27 = CommDef.Size27;
            mainWindowFixInfo.Size40 = CommDef.Size40;
            mainWindowFixInfo.Size50 = CommDef.Size50;
            mainWindowFixInfo.Size60 = CommDef.Size60;
            mainWindowFixInfo.Size80 = CommDef.Size80;
            mainWindowFixInfo.Size86 = CommDef.Size86;
            mainWindowFixInfo.Size88 = CommDef.Size88;
            mainWindowFixInfo.Size100 = CommDef.Size100;
            mainWindowFixInfo.Size120 = CommDef.Size120;
            mainWindowFixInfo.Size180 = CommDef.Size180;
            mainWindowFixInfo.Size200 = CommDef.Size200;
            mainWindowFixInfo.Size213 = CommDef.Size213;
            mainWindowFixInfo.Size240 = CommDef.Size240;
            mainWindowFixInfo.Size380 = CommDef.Size380;
            this.DataContext = mainWindowFixInfo;

            String CurrPath = Directory.GetCurrentDirectory().Trim();
            if (CurrPath.EndsWith("upgrade"))
                System.IO.Directory.SetCurrentDirectory(@"../");

            InfoDetail orginfo = new InfoDetail();
            Bitmap orgBmp = null;
            try {
                orgBmp = (Bitmap)Bitmap.FromFile(".\\Images\\org.png");
            } catch (Exception err) {
                orgBmp = Properties.Resources.org;
            }
            orginfo.InfoImage = BitmapToBitmapImage(orgBmp);
            orginfo.InfoTitle = "培训学校";
            orginfo.InfoText = org.name;
            InfoShowControl infoShow = new InfoShowControl(orginfo);
            this.OrgInfoList.Children.Add(infoShow);
            InfoDetail dateinfo = new InfoDetail();
            Bitmap dateBmp = null;
            try {
                dateBmp = (Bitmap)Bitmap.FromFile(".\\Images\\date.png");
            } catch (Exception err) {
                dateBmp = Properties.Resources.date;
            }
            dateinfo.InfoImage = BitmapToBitmapImage(dateBmp);
            dateinfo.InfoTitle = "日期";
            dateinfo.InfoText = GetCurrDate();
            infoShow = new InfoShowControl(dateinfo);
            this.OrgInfoList.Children.Add(infoShow);

            InfoDetail addinfo = new InfoDetail();
            Bitmap addressBmp = null;
            try {
                addressBmp = (Bitmap)Bitmap.FromFile(".\\Images\\addr.png");
            } catch (Exception err) {
                addressBmp = Properties.Resources.address;
            }
            addinfo.InfoImage = BitmapToBitmapImage(addressBmp);
            addinfo.InfoTitle = "地址";
            addinfo.InfoText = org.address;
            infoShow = new InfoShowControl(addinfo);
            this.OrgInfoList.Children.Add(infoShow);

            mGreen = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0x43, 0xA0, 0x47));
            Thread signUp = new Thread(new ThreadStart(SignUpHandler));
            signUp.IsBackground = true;
            signUp.Start();
        }

        private BitmapImage BitmapToBitmapImage(System.Drawing.Bitmap bitmap)
        {
            BitmapImage bitmapImage = new BitmapImage();
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                bitmap.Save(ms, bitmap.RawFormat);
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = ms;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();
            }
            return bitmapImage;
        }

        private void ClassBeginHandler(object sender, MouseButtonEventArgs e)
        {
            if (mClassStauts == 2)
            {
                mClassStauts = 1;
                BorderClassBegin.Background = mGreen;
                LabelClassBegin.Foreground = new SolidColorBrush(Colors.White);
                BorderClassEnd.Background = new SolidColorBrush(Colors.Transparent); ;
                LabelClassEnd.Foreground = mGreen;
            }
        }

        private void ClassEndHandler(object sender, MouseButtonEventArgs e)
        {
            if (mClassStauts == 1)
            {
                mClassStauts = 2;
                BorderClassBegin.Background = new SolidColorBrush(Colors.Transparent);
                LabelClassBegin.Foreground = mGreen;
                BorderClassEnd.Background = mGreen;
                LabelClassEnd.Foreground = new SolidColorBrush(Colors.White);
            }
        }

        private void ExitSystemClickHandler(object sender, RoutedEventArgs e)
        {
            string outMessage = "";
            RESTClient.Logout(ref outMessage);
            this.Close();
        }

        private void ClearClassClickHandler(object sender, RoutedEventArgs e)
        {
            mSelectLessons.Clear();
            mainWindowFixInfo.ClassFixSize = 0;
            lessons.Children.Clear();
            signUpList.Children.Clear();
            mClassCourse = null;
        }

        private void AddLessonClickHandler(object sender, RoutedEventArgs e)
        {
            broder_back.Visibility = Visibility.Visible;
            _loading.Visibility = Visibility.Visible;

            Thread addLesson = new Thread(new ThreadStart(AddLessonHandler));
            addLesson.IsBackground = true;
            addLesson.Start();
        }

        private void AddLessonHandler()
        {
            string outMessage = "";
            string response = RESTClient.GetClass(null, ref outMessage);
            if (response == null || response == "")
            {
                if (outMessage == "")
                    outMessage = "获取班级信息失败，服务器错误";
                this.Dispatcher.Invoke(() => {
                    broder_back.Visibility = Visibility.Collapsed;
                    _loading.Visibility = Visibility.Collapsed;
                    WarningTipWindow tipDialog = new WarningTipWindow(outMessage);
                    tipDialog.ShowDialog();
                });
                LogHelper.WriteWarnLog(outMessage);
                return;
            }

            try
            {
                JObject jResp = (JObject)JsonConvert.DeserializeObject(response);
                String status = (String)jResp.SelectToken("status", true);
                if (status != "success")
                {
                    string message = (String)jResp.SelectToken("message", true);
                    this.Dispatcher.Invoke(() => {
                        broder_back.Visibility = Visibility.Collapsed;
                        _loading.Visibility = Visibility.Collapsed;
                        WarningTipWindow tipDialog = new WarningTipWindow("获取班级信息失败:" + message);
                        tipDialog.ShowDialog();
                    });
                    LogHelper.WriteWarnLog("获取班级信息失败:" + message);
                    return;
                }

                JArray jArray = (JArray)jResp.SelectToken("data", true);
                if (jArray == null || jArray.Count == 0)
                {
                    this.Dispatcher.Invoke(() => {
                        broder_back.Visibility = Visibility.Collapsed;
                        _loading.Visibility = Visibility.Collapsed;
                        WarningTipWindow tipDialog = new WarningTipWindow("获取班级信息失败，data为空");
                        tipDialog.ShowDialog();
                    });
                    LogHelper.WriteWarnLog("获取班级信息失败: data为空");
                    return;
                }
                
                LogHelper.WriteInfoLog("获取班级信息成功");
                this.Dispatcher.Invoke(() => {
                    broder_back.Visibility = Visibility.Collapsed;
                    _loading.Visibility = Visibility.Collapsed;
                    back.Visibility = Visibility.Visible;
                    ClassWindow classWindow = new ClassWindow(jArray, mSelectLessons);
                    classWindow.CancelSelect += CancelSelect;
                    classWindow.ConfirmSelect += ConfirmSelect;
                    classWindow.ShowDialog();
                });
                return;
            }
            catch (Exception err)
            {
                this.Dispatcher.Invoke(() => {
                    broder_back.Visibility = Visibility.Collapsed;
                    _loading.Visibility = Visibility.Collapsed;
                    WarningTipWindow tipDialog = new WarningTipWindow("获取班级信息失败，" + err.Message);
                    tipDialog.ShowDialog();
                });
                return;
            }
        }
        
        private void CancelSelect()
        {
            back.Visibility = Visibility.Collapsed;
        }

        private void ConfirmSelect(Dictionary<long, LessonInfo> lessonList)
        {
            back.Visibility = Visibility.Collapsed;
            foreach (var item in lessonList)
            {
                long lessonId = item.Key;
                LessonInfo lessonInfo = item.Value;
                
                if (!mSelectLessons.ContainsKey(lessonId))
                {
                    mSelectLessons.Add(lessonId, lessonInfo);
                    SelectedLessonControl selectedLessonControl = new SelectedLessonControl(lessonInfo);
                    lessons.Children.Add(selectedLessonControl);
                }
            }

            if (mSelectLessons.Count >= 2)
                mainWindowFixInfo.ClassFixSize = mainWindowFixInfo.Size86 * 2;
            else if (mSelectLessons.Count >= 1)
                mainWindowFixInfo.ClassFixSize = mainWindowFixInfo.Size86 * 1;

            Dictionary<long, String> classCourse = new Dictionary<long, String>();
            foreach (var item in mSelectLessons)
            {
                long lessonId = item.Key;
                LessonInfo lessonInfo = item.Value;

                if (classCourse.ContainsKey(lessonInfo.ClassId))
                {
                    classCourse[lessonInfo.ClassId] = classCourse[lessonInfo.ClassId] + "," + lessonInfo.Id.ToString();
                }
                else
                {
                    classCourse[lessonInfo.ClassId] = lessonInfo.Id.ToString();
                }
            }

            JArray jArray = new JArray();
            foreach (var item in classCourse)
            {
                JObject jObject = new JObject();
                jObject.Add("classId", item.Key);
                jObject.Add("courseIds", item.Value);
                jArray.Add(jObject);
            }

            if (jArray.Count > 0)
            {
                JObject jObject = new JObject();
                jObject.Add("classCourse", jArray);
                mClassCourse = jObject.ToString();
                QuerySignUp();
            }
        }

        public void SignUpHandler()
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer();
            while (true)
            {
                IDCardReader cardRead = new IDCardReader();
                //初始化
                if (cardRead.Initialize() == false)
                {
                    //LogHelper.WriteWarnLog("身份证读卡器初始化失败");
                    Thread.Sleep(500);
                    continue;
                }

                if (cardRead.Authenticate() == -1)
                {
                    //LogHelper.WriteWarnLog("身份证读卡器注册失败");
                    Thread.Sleep(500);
                    cardRead.CloseComm();
                    continue;
                }

                if (cardRead.ReadContent() == -1)
                {
                    //LogHelper.WriteWarnLog("获取身份证信息失败");
                    Thread.Sleep(500);
                    cardRead.CloseComm();
                    continue;
                }

                if (mClassCourse == null)
                {
                    try
                    {
                        player.SoundLocation = @".\Voice\selectCourse.wav";
                        player.LoadAsync();
                        player.Play();
                    } catch (Exception err)
                    {
                        LogHelper.WriteWarnLog(err.Message);
                    }
                    continue;
                }

                LogHelper.WriteInfoLog("获取身份证信息成功");
                string name = "";
                string IDNum = "";
                //string bmpPath = "";
                cardRead.GetNameGBK(ref name);
                cardRead.GetIDNumGBK(ref IDNum);
                //cardRead.GetBmpPathGBK(ref bmpPath);
                cardRead.CloseComm();
                
                string outMessage = "";
                string response = RESTClient.SignUp(mClassCourse, IDNum, mClassStauts, ref outMessage);
                if (response == null || response == "")
                {
                    if (outMessage == "")
                        outMessage = "签到失败，服务器错误";
                    this.Dispatcher.Invoke(() => {
                        broder_back.Visibility = Visibility.Collapsed;
                        _loading.Visibility = Visibility.Collapsed;
                        //WarningTipWindow tipDialog = new WarningTipWindow(outMessage);
                        //tipDialog.ShowDialog();
                    });
                    LogHelper.WriteWarnLog(outMessage);
                    try {
                        if (mClassStauts == 1)
                            player.SoundLocation = @".\Voice\signUpInFail.wav";
                        else
                            player.SoundLocation = @".\Voice\signUpOutFail.wav";
                        player.LoadAsync();
                        player.Play();
                    } catch (Exception err) {
                        LogHelper.WriteWarnLog(err.Message);
                    }
                    LogHelper.WriteWarnLog(outMessage);
                    continue;
                }

                try
                {
                    JObject jResp = (JObject)JsonConvert.DeserializeObject(response);
                    String status = (String)jResp.SelectToken("status", true);
                    if (status != "success")
                    {
                        string message = (String)jResp.SelectToken("message", true);
                        this.Dispatcher.Invoke(() => {
                            broder_back.Visibility = Visibility.Collapsed;
                            _loading.Visibility = Visibility.Collapsed;
                            //WarningTipWindow tipDialog = new WarningTipWindow("签到失败:" + message);
                            //tipDialog.ShowDialog();
                        });

                        LogHelper.WriteWarnLog(message);
                        try {
                            if (mClassStauts == 1)
                                player.SoundLocation = @".\Voice\signUpInFail.wav";
                            else
                                player.SoundLocation = @".\Voice\signUpOutFail.wav";
                            player.LoadAsync();
                            player.Play();
                        } catch (Exception err) {
                            LogHelper.WriteWarnLog(err.Message);
                        }
                        LogHelper.WriteWarnLog("签到失败:" + message);
                        continue;
                    }

                    LogHelper.WriteInfoLog("签到成功");
                    this.Dispatcher.Invoke(() => {
                        broder_back.Visibility = Visibility.Collapsed;
                        _loading.Visibility = Visibility.Collapsed;
                    });
                    try {
                        if (mClassStauts == 1)
                            player.SoundLocation = @".\Voice\signUpInSucc.wav";
                        else
                            player.SoundLocation = @".\Voice\signUpOutSucc.wav";
                        player.LoadAsync();
                        player.Play();
                    } catch (Exception err) {
                        LogHelper.WriteWarnLog(err.Message);
                    }
                    QuerySignUp();
                    continue;
                }
                catch (Exception err)
                {
                    this.Dispatcher.Invoke(() => {
                        broder_back.Visibility = Visibility.Collapsed;
                        _loading.Visibility = Visibility.Collapsed;
                        //WarningTipWindow tipDialog = new WarningTipWindow("签到失败，" + err.Message);
                        //tipDialog.ShowDialog();
                    });
                    LogHelper.WriteWarnLog(err.Message);
                    try {
                        if (mClassStauts == 1)
                            player.SoundLocation = @".\Voice\signUpInFail.wav";
                        else
                            player.SoundLocation = @".\Voice\signUpOutFail.wav";
                        player.LoadAsync();
                        player.Play();
                    } catch (Exception errSub) {
                        LogHelper.WriteWarnLog(errSub.Message);
                    }
                    continue;
                }
            }
        }

        private void QuerySignUp()
        {
            if (mClassCourse != null)
            {
                string outMessage = "";
                string response = RESTClient.QuerySignUp(mClassCourse, ref outMessage);
                if (response == null || response == "")
                {
                    if (outMessage == "")
                        outMessage = "查询签到失败，服务器错误";
                    this.Dispatcher.Invoke(() => {
                        broder_back.Visibility = Visibility.Collapsed;
                        _loading.Visibility = Visibility.Collapsed;
                        WarningTipWindow tipDialog = new WarningTipWindow(outMessage);
                        tipDialog.ShowDialog();
                    });
                    LogHelper.WriteWarnLog(outMessage);
                    return;
                }

                try
                {
                    JObject jResp = (JObject)JsonConvert.DeserializeObject(response);
                    String status = (String)jResp.SelectToken("status", true);
                    if (status != "success")
                    {
                        string message = (String)jResp.SelectToken("message", true);
                        this.Dispatcher.Invoke(() => {
                            broder_back.Visibility = Visibility.Collapsed;
                            _loading.Visibility = Visibility.Collapsed;
                            WarningTipWindow tipDialog = new WarningTipWindow("查询签到失败:" + message);
                            tipDialog.ShowDialog();
                        });
                        LogHelper.WriteWarnLog("查询签到失败:" + message);
                        return;
                    }
                    JArray jArray = (JArray)jResp.SelectToken("data", true);

                    LogHelper.WriteInfoLog("查询签到成功");
                    this.Dispatcher.Invoke(() => {
                        broder_back.Visibility = Visibility.Collapsed;
                        _loading.Visibility = Visibility.Collapsed;
                        signUpList.Children.Clear();

                        int index = 0;
                        foreach (var item in jArray)
                        {
                            JToken jToken = item;
                            SignUpInfo signUpInfo = new SignUpInfo();
                            try
                            {
                                signUpInfo.Id = (String)jToken.SelectToken("id");
                                signUpInfo.StudentName = (String)jToken.SelectToken("studentName");
                                signUpInfo.CheckTime = (String)jToken.SelectToken("checkTime");
                                if ((String)jToken.SelectToken("inOut") == "1")
                                    signUpInfo.Type = "上课";
                                else
                                    signUpInfo.Type = "下课";
                                signUpInfo.IdCardNum = (String)jToken.SelectToken("idCardNum");
                                if ((String)jToken.SelectToken("sex") == "1")
                                    signUpInfo.Sex = "男";
                                else
                                    signUpInfo.Sex = "女";
                                signUpInfo.CourseName = (String)jToken.SelectToken("courseName");

                                SignUpControl signUpControl = new SignUpControl(index++, signUpInfo);
                                signUpControl.DeletedSignUp += DeletedSignUp;
                                signUpList.Children.Add(signUpControl);
                            }
                            catch (Exception err)
                            {
                                LogHelper.WriteWarnLog(err.Message);
                                continue;
                            }
                        }
                    });
                    return;
                }
                catch (Exception err)
                {
                    this.Dispatcher.Invoke(() => {
                        broder_back.Visibility = Visibility.Collapsed;
                        _loading.Visibility = Visibility.Collapsed;
                        WarningTipWindow tipDialog = new WarningTipWindow("查询签到失败，" + err.Message);
                        tipDialog.ShowDialog();
                    });
                    return;
                }
            }
        }

        private void DeletedSignUp(SignUpInfo signUpInfo)
        {
            SignUpControl oneSignUpItem = null;
            for (int loop = 0; loop < signUpList.Children.Count; loop++)
            {
                if (signUpList.Children[loop].GetType().ToString() == "SignInApp.SignUpControl")
                {
                    oneSignUpItem = signUpList.Children[loop] as SignUpControl;
                    if (oneSignUpItem.mSignUpInfo.Id == signUpInfo.Id)
                    {
                        //删除
                        string outMessage = "";
                        string response = RESTClient.SignOut(signUpInfo.Id, ref outMessage);
                        if (response == null || response == "")
                        {
                            if (outMessage == "")
                                outMessage = "删除签到失败，服务器错误";
                            broder_back.Visibility = Visibility.Collapsed;
                            _loading.Visibility = Visibility.Collapsed;
                            WarningTipWindow tipDialog = new WarningTipWindow(outMessage);
                            tipDialog.ShowDialog();
                            return;
                        }

                        try
                        {
                            JObject jResp = (JObject)JsonConvert.DeserializeObject(response);
                            String status = (String)jResp.SelectToken("status", true);
                            if (status != "success")
                            {
                                string message = (String)jResp.SelectToken("message", true);
                                broder_back.Visibility = Visibility.Collapsed;
                                _loading.Visibility = Visibility.Collapsed;
                                WarningTipWindow tipDialog = new WarningTipWindow("删除签到失败:" + message);
                                tipDialog.ShowDialog();
                                LogHelper.WriteWarnLog("删除签到失败:" + message);
                                return;
                            }

                            LogHelper.WriteInfoLog("查询签到成功");
                            broder_back.Visibility = Visibility.Collapsed;
                            _loading.Visibility = Visibility.Collapsed;
                            signUpList.Children.RemoveAt(loop);
                            return;
                        }
                        catch (Exception err)
                        {
                            broder_back.Visibility = Visibility.Collapsed;
                            _loading.Visibility = Visibility.Collapsed;
                            WarningTipWindow tipDialog = new WarningTipWindow("查询签到失败，" + err.Message);
                            tipDialog.ShowDialog();
                            return;
                        }
                    }
                }
            }
        }

        private String GetCurrDate()
        {
            string weekName = "";
            switch (DateTime.Now.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    weekName = "星期一";
                    break;
                case DayOfWeek.Tuesday:
                    weekName = "星期二";
                    break;
                case DayOfWeek.Wednesday:
                    weekName = "星期三";
                    break;
                case DayOfWeek.Thursday:
                    weekName = "星期四";
                    break;
                case DayOfWeek.Friday:
                    weekName = "星期五";
                    break;
                case DayOfWeek.Saturday:
                    weekName = "星期六";
                    break;
                case DayOfWeek.Sunday:
                    weekName = "星期日";
                    break;
            }
            return DateTime.Today.Year + "年" + DateTime.Today.Month + "月" + DateTime.Today.Day + "日 " + weekName;
        }
    }

    public class MainWindowFixInfo : NotificationBase
    {
        double mClassFixSize;
        public double Size14 { get; set; }
        public double Size18 { get; set; }
        public double Size20 { get; set; }
        public double Size22 { get; set; }
        public double Size24 { get; set; }
        public double Size26 { get; set; }
        public double Size27 { get; set; }
        public double Size30 { get; set; }
        public double Size40 { get; set; }
        public double Size50 { get; set; }
        public double Size60 { get; set; }
        public double Size80 { get; set; }
        public double Size86 { get; set; }
        public double Size88 { get; set; }
        public double Size100 { get; set; }
        public double Size120 { get; set; }
        public double Size180 { get; set; }
        public double Size200 { get; set; }
        public double Size213 { get; set; }
        public double Size240 { get; set; }
        public double Size380 { get; set; }
        public double ClassFixSize                       //减分项
        {
            get
            {
                return mClassFixSize;
            }
            set
            {
                mClassFixSize = value;
                RaisePropertyChanged("ClassFixSize");
            }
        }
    }

    public class Org
    {
        public long id { get; set; }           //培训机构ID
        public string name { get; set; }       //培训机构名称
        public string address { get; set; }    //培训机构地址
    }

    public class NotificationBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    //通用
    public static class CommDef
    {
        public static double ScreenMultiple = 0;
        public static double Size8 = 8;
        public static double Size14 = 14;
        public static double Size16 = 16;
        public static double Size18 = 18;
        public static double Size20 = 20;
        public static double Size22 = 22;
        public static double Size24 = 24;
        public static double Size26 = 26;
        public static double Size27 = 27;
        public static double Size30 = 30;
        public static double Size35 = 35;
        public static double Size40 = 40;
        public static double Size48 = 48;
        public static double Size50 = 50;
        public static double Size55 = 55;
        public static double Size52 = 52;
        public static double Size60 = 60;
        public static double Size80 = 80;
        public static double Size90 = 90;
        public static double Size86 = 86;
        public static double Size88 = 88;
        public static double Size100 = 100;
        public static double Size120 = 120;
        public static double Size140 = 140;
        public static double Size180 = 180;
        public static double Size200 = 200;
        public static double Size213 = 213;
        public static double Size240 = 240; 
        public static double Size277 = 277;
        public static double Size380 = 380;

        public static string ApplicationDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\HZCQT_SignIn\\";
        public static string UserPath = ApplicationDataPath;
    }
}
