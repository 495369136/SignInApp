using Log;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RESTApi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

        public MainWindow(Org org)
        {
            InitializeComponent();

            int height = (int)SystemParameters.WorkArea.Height;
            int width = (int)SystemParameters.WorkArea.Width;
            this.Width = width;
            this.Height = height;

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
                    player.SoundLocation = @".\Voice\selectCourse.wav";
                    player.LoadAsync();
                    player.Play();
                    continue;
                }

                LogHelper.WriteInfoLog("获取身份证信息成功");
                string name = "";
                string IDNum = "";
                cardRead.GetNameGBK(ref name);
                cardRead.GetIDNumGBK(ref IDNum);
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
                    if (mClassStauts == 1)
                        player.SoundLocation = @".\Voice\signUpInFail.wav";
                    else
                        player.SoundLocation = @".\Voice\signUpOutFail.wav";
                    player.LoadAsync();
                    player.Play();
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
                        if (mClassStauts == 1)
                            player.SoundLocation = @".\Voice\signUpInFail.wav";
                        else
                            player.SoundLocation = @".\Voice\signUpOutFail.wav";
                        player.LoadAsync();
                        player.Play();
                        LogHelper.WriteWarnLog("签到失败:" + message);
                        continue;
                    }

                    LogHelper.WriteInfoLog("签到成功");
                    this.Dispatcher.Invoke(() => {
                        broder_back.Visibility = Visibility.Collapsed;
                        _loading.Visibility = Visibility.Collapsed;
                    });
                    if (mClassStauts == 1)
                        player.SoundLocation = @".\Voice\signUpInSucc.wav";
                    else
                        player.SoundLocation = @".\Voice\signUpOutSucc.wav";
                    player.LoadAsync();
                    player.Play();
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
                    if (mClassStauts == 1)
                        player.SoundLocation = @".\Voice\signUpInFail.wav";
                    else
                        player.SoundLocation = @".\Voice\signUpOutFail.wav";
                    player.LoadAsync();
                    player.Play();
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
}
