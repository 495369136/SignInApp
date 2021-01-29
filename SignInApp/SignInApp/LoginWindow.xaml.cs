using Log;
using RESTApi;
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Xml;

namespace SignInApp
{
    /// <summary>
    /// LoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : Window
    {
        public static string ApplicationDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\HZCQT_signIn\\";
        public string CONF_FILE_NAME = ApplicationDataPath + "appSettings.conf";
        public const string CONF_USER_NAME = "userName";
        public const string CONF_PASSWORD = "password";
        public const string CONF_REMAIN_FLAG = "isRemember";

        public LoginWindow()
        {
            InitializeComponent();

            string versionValue = string.Empty;
            try
            {
                XmlDocument doc = new XmlDocument();
                String CurrPath = Directory.GetCurrentDirectory().Trim();
                if (CurrPath.EndsWith("upgrade"))
                    doc.Load(@"config.xml");
                else
                    doc.Load(@"upgrade\config.xml");
                XmlElement root = doc.DocumentElement;

                foreach (XmlElement item in root.ChildNodes)
                {
                    string codeTxt = string.Empty;
                    string nameTxt = string.Empty;
                    if (item.Name == "CurrVersion")
                    {
                        versionValue = item.ChildNodes[0].Value;
                        break;
                    }
                }
            }
            catch (Exception err)
            {
                LogHelper.WriteWarnLog("Get version failed:" + err.Message);
                versionValue = Properties.Settings.Default.app_version;
            }

            competent_organization.Content = Properties.Settings.Default.competent_organization;
            province.Content = Properties.Settings.Default.province;
            app_type.Content = Properties.Settings.Default.app_type;
            app_name.Content = Properties.Settings.Default.app_name + "V" + versionValue;
            support_company.Content = Properties.Settings.Default.support_company;
            support_tel.Content = Properties.Settings.Default.support_tel;
            RESTClient.REST_HOST = Properties.Settings.Default.rest_host;

            if (!Directory.Exists(ApplicationDataPath))
                Directory.CreateDirectory(ApplicationDataPath);

            if (File.Exists(CONF_FILE_NAME) == false)
            {
                FileStream fs = new FileStream(CONF_FILE_NAME, FileMode.CreateNew);
                if (fs != null)
                    fs.Close();
                return;
            }

            string userName = "";
            string password = "";
            string isRemember = "";
            bool ret = GetConfInfo(ref userName, ref password, ref isRemember);
            if (ret)
            {
                if (userName != "")
                {
                    account.Text = userName;
                    this.account.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0x00, 0x00, 0x00));
                }

                if (isRemember == "true")
                {
                    this.password.Password = password;
                    this.remainFlag.IsChecked = true;
                }
            }
            return;
        }

        private bool GetConfInfo(ref string userName, ref string password, ref string isRemember)
        {
            FileStream fs = null;
            StreamReader streamReader = null;
            string oneLine = "";
            string encodingPassword = "";

            try
            {
                fs = new FileStream(CONF_FILE_NAME, FileMode.Open, FileAccess.Read);
                if (fs == null)
                {
                    return false;
                }
                streamReader = new StreamReader(fs);
                while ((oneLine = streamReader.ReadLine()) != null)
                {
                    if (oneLine.StartsWith(CONF_USER_NAME + "="))
                    {
                        userName = oneLine.Substring(CONF_USER_NAME.Length + 1);
                    }
                    else if (oneLine.StartsWith(CONF_PASSWORD + "="))
                    {
                        encodingPassword = oneLine.Substring(CONF_PASSWORD.Length + 1);
                        int tmpLength = encodingPassword.IndexOf('=');
                        if (tmpLength != -1)
                        {
                            string tmp = encodingPassword.Substring(0, tmpLength);
                            char[] arr = tmp.ToCharArray();
                            Array.Reverse(arr);
                            tmp = new string(arr);
                            encodingPassword = tmp + encodingPassword.Substring(tmpLength);
                        }
                        else
                        {
                            char[] arr = encodingPassword.ToCharArray();
                            Array.Reverse(arr);
                            encodingPassword = new string(arr);
                        }
                        password = Encoding.UTF8.GetString(Convert.FromBase64String(encodingPassword));

                    }
                    else if (oneLine.StartsWith(CONF_REMAIN_FLAG + "="))
                    {
                        isRemember = oneLine.Substring(CONF_REMAIN_FLAG.Length + 1);
                    }
                }
            }
            catch
            {
                if (fs != null)
                    fs.Close();
                return false;
            }

            fs.Close();
            return true;
        }

        public bool UpdateConfInfo(string userName, string password, string isRemember)
        {
            FileStream fs = null;
            StreamWriter streamWriter = null;

            try
            {
                fs = new FileStream(CONF_FILE_NAME, FileMode.Truncate, FileAccess.Write);
                if (fs == null)
                {
                    return false;
                }
                streamWriter = new StreamWriter(fs);
                streamWriter.WriteLine(CONF_USER_NAME + "=" + userName);
                string passwordEncoding = Convert.ToBase64String(Encoding.UTF8.GetBytes(password));
                int tmpLength = passwordEncoding.IndexOf('=');
                if (tmpLength == -1)
                {
                    char[] arr = passwordEncoding.ToCharArray();
                    Array.Reverse(arr);
                    string tmp = new string(arr);
                    streamWriter.WriteLine(CONF_PASSWORD + "=" + tmp);
                }
                else
                {
                    string tmp = passwordEncoding.Substring(0, tmpLength);
                    char[] arr = tmp.ToCharArray();
                    Array.Reverse(arr);
                    tmp = new string(arr);
                    streamWriter.WriteLine(CONF_PASSWORD + "=" + tmp + passwordEncoding.Substring(tmpLength));
                }
                streamWriter.WriteLine(CONF_REMAIN_FLAG + "=" + isRemember);
                streamWriter.Close();
                fs.Close();
            }
            catch (Exception e)
            {
                fs.Close();
                return false;
            }
            return true;
        }

        private void LoginHandler()
        {
            string strAccount = "";
            string strPassword = "";

            this.Dispatcher.Invoke(() => {
                strAccount = this.account.Text;
                strPassword = this.password.Password;
            });

            string outMessage = "";
            string response = RESTClient.Login(strAccount, strPassword, ref outMessage, Properties.Settings.Default.Test);
            if (response == null || response == "")
            {
                if (outMessage == "")
                    outMessage = "登录失败，服务器错误";
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
                        WarningTipWindow tipDialog = new WarningTipWindow("登录失败:" + message);
                        tipDialog.ShowDialog();
                    });
                    LogHelper.WriteWarnLog(strAccount + "登录失败:" + message);
                    return;
                }

                JArray jArray = (JArray)jResp.SelectToken("data", true);
                if (jArray == null || jArray.Count == 0)
                {
                    this.Dispatcher.Invoke(() => {
                        broder_back.Visibility = Visibility.Collapsed;
                        _loading.Visibility = Visibility.Collapsed;
                        WarningTipWindow tipDialog = new WarningTipWindow("登录失败，data为空");
                        tipDialog.ShowDialog();
                    });
                    LogHelper.WriteWarnLog(strAccount + "登录失败: data为空");
                    return;
                }

                JToken jToken = null;
                foreach (var item in jArray)
                {
                    jToken = item;
                    break;
                }

                LogHelper.WriteInfoLog(strAccount + "登录成功");
                string sessionid = (String)jToken.SelectToken("sessionid", true);
                RESTClient.SESSION_ID = sessionid;
                Org org = new Org();
                org.id = (long)jToken.SelectToken("org_id", true);
                org.name = (String)jToken.SelectToken("org_name", true);
                org.address = (String)jToken.SelectToken("address", true);
                this.Dispatcher.Invoke(() => {
                    broder_back.Visibility = Visibility.Collapsed;
                    _loading.Visibility = Visibility.Collapsed;
                    MainWindow mainWindow = new MainWindow(org);
                    mainWindow.Show();
                    this.Close();
                });
                return;
            }
            catch (Exception err)
            {
                this.Dispatcher.Invoke(() => {
                    broder_back.Visibility = Visibility.Collapsed;
                    _loading.Visibility = Visibility.Collapsed;
                    WarningTipWindow tipDialog = new WarningTipWindow(strAccount + "登录失败，" + err.Message);
                    tipDialog.ShowDialog();
                });
                return;
            }
        }

        private void LoginClickHandler(object sender, EventArgs e)
        {
            if (this.account.Text == "")
            {
                WarningTipWindow tipDialog = new WarningTipWindow("请输入账号");
                tipDialog.ShowDialog();
                return;
            }

            if (this.password.Password == "")
            {
                WarningTipWindow tipDialog = new WarningTipWindow("请输入密码");
                tipDialog.ShowDialog();
                return;
            }

            if (this.remainFlag.IsChecked == true)
            {
                UpdateConfInfo(this.account.Text, this.password.Password, "true");
            }
            else
            {
                UpdateConfInfo(this.account.Text, this.password.Password, "false");
            }

            LogHelper.WriteInfoLog(this.account.Text);
            broder_back.Visibility = Visibility.Visible;
            _loading.Visibility = Visibility.Visible;
            Thread Login = new Thread(new ThreadStart(LoginHandler));
            Login.IsBackground = true;
            Login.Start();
        }

        private void ExitClickHandler(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
