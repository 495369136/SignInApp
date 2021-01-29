using Log;
using System;
using System.Windows;
using System.Windows.Input;
using Newtonsoft.Json.Linq;
using RESTApi;
using System.Threading;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace SignInApp
{
    /// <summary>
    /// ClassWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ClassWindow : Window
    {
        public event CancelSelectHandler CancelSelect;
        public event ConfirmSelectHandler ConfirmSelect;

        private long mSelectClassId = 0;
        private Dictionary<long, LessonInfo> mLessonList = new Dictionary<long, LessonInfo>();
        private Dictionary<long, LessonInfo> mSelectLessons = null;

        public ClassWindow(JArray classList, Dictionary<long, LessonInfo> selectLessons)
        {
            InitializeComponent();

            mSelectLessons = selectLessons;
            int index = 0;
            foreach (var item in classList)
            {
                JToken jToken = item;
                ClassInfo classInfo = new ClassInfo();
                try {
                    classInfo.Id = (long)jToken.SelectToken("id");
                    classInfo.ClassName = (String)jToken.SelectToken("className");
                    classInfo.StartTime = (String)jToken.SelectToken("startTime");
                    classInfo.EndTime = (String)jToken.SelectToken("endTime");
                    classInfo.ExamTypeName = (String)jToken.SelectToken("examTypeName");
                    classInfo.StudentNums = (String)jToken.SelectToken("studentNums");
                    ClassRowControl classRowControl = new ClassRowControl(index++, classInfo);
                    classRowControl.SelectClass += SelectClass;
                    ClassList.Children.Add(classRowControl);
                } catch (Exception err) {
                    LogHelper.WriteWarnLog(err.Message);
                    continue;
                }

                if (index == 1)
                    SelectClass(classInfo);
            }
        }

        private void SelectClass(ClassInfo classInfo)
        {
            if (mSelectClassId != classInfo.Id)
            {
                mSelectClassId = classInfo.Id;
                //加载班级信息
                lesson_broder_back.Visibility = Visibility.Visible;
                _lesson_loading.Visibility = Visibility.Visible;
                ClassRowControl OneClassItem = null;
                for (int loop = 0; loop < ClassList.Children.Count; loop++)
                {
                    if (ClassList.Children[loop].GetType().ToString() == "SignInApp.ClassRowControl")
                    {
                        OneClassItem = ClassList.Children[loop] as ClassRowControl;
                        if (OneClassItem.mClassInfo.Id != classInfo.Id)
                            OneClassItem.ImageRight.Visibility = Visibility.Collapsed;
                    }
                }

                LessonList.Children.Clear();
                Thread queryLesson = new Thread(() => QueryLessonHandler(classInfo));
                queryLesson.IsBackground = true;
                queryLesson.Start();
            }
        }
        
        private void QueryLessonHandler(ClassInfo classInfo)
        {
            string outMessage = "";
            string response = RESTClient.GetLesson(classInfo.Id, ref outMessage);
            if (response == null || response == "")
            {
                if (outMessage == "")
                    outMessage = "获取课程信息失败，服务器错误";
                this.Dispatcher.Invoke(() => {
                    lesson_broder_back.Visibility = Visibility.Collapsed;
                    _lesson_loading.Visibility = Visibility.Collapsed;
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
                        lesson_broder_back.Visibility = Visibility.Collapsed;
                        _lesson_loading.Visibility = Visibility.Collapsed;
                        WarningTipWindow tipDialog = new WarningTipWindow("获取课程信息失败:" + message);
                        tipDialog.ShowDialog();
                    });
                    LogHelper.WriteWarnLog("获取课程信息失败:" + message);
                    return;
                }

                JArray jArray = (JArray)jResp.SelectToken("data", true);
                if (jArray == null || jArray.Count == 0)
                {
                    this.Dispatcher.Invoke(() => {
                        lesson_broder_back.Visibility = Visibility.Collapsed;
                        _lesson_loading.Visibility = Visibility.Collapsed;
                    });
                    LogHelper.WriteWarnLog("获取课程信息失败: data为空");
                    return;
                }

                LogHelper.WriteInfoLog("获取课程信息成功");
                this.Dispatcher.Invoke(() => {
                    lesson_broder_back.Visibility = Visibility.Collapsed;
                    _lesson_loading.Visibility = Visibility.Collapsed;

                    int index = 0;
                    foreach (var item in jArray)
                    {
                        JToken jToken = item;
                        LessonInfo lessonInfo = new LessonInfo();
                        try
                        {
                            lessonInfo.Id = (long)jToken.SelectToken("id");
                            lessonInfo.Name = (String)jToken.SelectToken("name");
                            lessonInfo.Teacher = (String)jToken.SelectToken("teacher");
                            lessonInfo.Number = (String)jToken.SelectToken("number");
                            lessonInfo.ClassId = classInfo.Id;
                            lessonInfo.ClassName = "班级：" + classInfo.ClassName;
                            if (mLessonList.ContainsKey(lessonInfo.Id)
                               || mSelectLessons.ContainsKey(lessonInfo.Id)) {
                                lessonInfo.IsChecked = true;
                            } else {
                                lessonInfo.IsChecked = false;
                            }

                            LessonRowControl lessonRowControl = new LessonRowControl(index++, lessonInfo);
                            lessonRowControl.SelectLesson += SelectLesson;
                            LessonList.Children.Add(lessonRowControl);
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
                    lesson_broder_back.Visibility = Visibility.Collapsed;
                    _lesson_loading.Visibility = Visibility.Collapsed;
                    WarningTipWindow tipDialog = new WarningTipWindow("获取课程信息失败，" + err.Message);
                    tipDialog.ShowDialog();
                });
                return;
            }
        }

        private void SelectLesson(long lessonId, bool isSelected, LessonInfo lessonInfo)
        {
            if (isSelected) {
                mLessonList.Add(lessonId, lessonInfo);
            } else {
                mLessonList.Remove(lessonId);
            }
        }

        private void CloseClickHandler(object sender, RoutedEventArgs e)
        {
            if (null != CancelSelect)
                CancelSelect();
            this.Close();
        }

        private void SelectLessonClickHandler(object sender, RoutedEventArgs e)
        {
            if (null != ConfirmSelect)
                ConfirmSelect(mLessonList);
            this.Close();
        }

        private void SearchClickHandler(object sender, RoutedEventArgs e)
        {
            broder_back.Visibility = Visibility.Visible;
            _loading.Visibility = Visibility.Visible;
            ClassList.Children.Clear();
            LessonList.Children.Clear();

            string className = this.classSrachCriteria.Text;
            Thread addLesson = new Thread(() => AddLessonHandler(className));
            addLesson.IsBackground = true;
            addLesson.Start();
        }

        private void SerachKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                broder_back.Visibility = Visibility.Visible;
                _loading.Visibility = Visibility.Visible;
                ClassList.Children.Clear();
                LessonList.Children.Clear();

                string className = this.classSrachCriteria.Text;
                Thread addLesson = new Thread(() => AddLessonHandler(className));
                addLesson.IsBackground = true;
                addLesson.Start();
            }
        }

        private void AddLessonHandler(string className)
        {
            string outMessage = "";
            string response = RESTClient.GetClass(className, ref outMessage);
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
                    });
                    LogHelper.WriteWarnLog("获取班级信息失败: data为空");
                    return;
                }

                LogHelper.WriteInfoLog("获取班级信息成功");
                this.Dispatcher.Invoke(() => {
                    broder_back.Visibility = Visibility.Collapsed;
                    _loading.Visibility = Visibility.Collapsed;

                    int index = 0;
                    foreach (var item in jArray)
                    {
                        JToken jToken = item;
                        ClassInfo classInfo = new ClassInfo();
                        try
                        {
                            classInfo.Id = (long)jToken.SelectToken("id");
                            classInfo.ClassName = (String)jToken.SelectToken("className");
                            classInfo.StartTime = (String)jToken.SelectToken("startTime");
                            classInfo.EndTime = (String)jToken.SelectToken("endTime");
                            classInfo.ExamTypeName = (String)jToken.SelectToken("examTypeName");
                            classInfo.StudentNums = (String)jToken.SelectToken("studentNums");
                            ClassRowControl classRowControl = new ClassRowControl(index++, classInfo);
                            classRowControl.SelectClass += SelectClass;
                            ClassList.Children.Add(classRowControl);
                        }
                        catch (Exception err)
                        {
                            LogHelper.WriteWarnLog(err.Message);
                            continue;
                        }

                        if (index == 1)
                            SelectClass(classInfo);
                    }
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
    }
}
