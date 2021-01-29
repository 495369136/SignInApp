using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RESTApi
{
    public class RESTClient
    {
        public static string REST_HOST = "http://10.63.100.129:2000";

        public static string URL_LOGIN = "/api/v2/session/login_password.xhtml";
        public static string URL_CLASS = "/api/v2/clazz/noEnterExam.xhtml";
        public static string URL_LESSON = "/api/v2/clazz/query_course.xhtml";
        public static string URL_SIGN_UP = "/api/v2/clazz/sign_up.xhtml";
        public static string URL_QUREY_SIGN_UP = "/api/v2/clazz/query_sign_up.xhtml";
        public static string URL_SIGN_OUT = "/api/v2/clazz/sign_out.xhtml";
        public static string URL_LOGOUT = "/api/v2/session/logout.xhtml";
        
        public static string APP_ID = "25659291566342146";
        public static string APP_SECRET = "1dce22b43da5f39d076c446a4217c579af5f7591";

        public static String SESSION_ID = "";

        public static string GetURL(string url)
        {
            return REST_HOST + url;
        }

        public static long GetTimeStamp()
        {
            //long ticks = DateTime.Now.Ticks - DateTime.Parse("01/01/1970 00:00:00").Ticks;
            //return ticks / 10000;
            //return 1534225102203;
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); // 当地时区
            long timeStamp = (long)(DateTime.Now.ToLocalTime() - startTime).TotalMilliseconds; // 相差毫秒数
            return timeStamp;
        }

        public static string GetMD5(string myString)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = System.Text.Encoding.UTF8.GetBytes(myString);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;
            byte2String = BitConverter.ToString(targetData).Replace("-", "");
            /*for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x");
            }
            */
            return byte2String;
        }
        public static string MD5Encrypt(string strText)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(strText));
            return System.Text.Encoding.UTF8.GetString(result);
        }
        private static void GenerateToken(out string token, out string timestamp)
        {
            token = null;
            timestamp = GetTimeStamp().ToString();
            string strts = timestamp.ToString().ToUpper();
            token = GetMD5(GetMD5(strts) + APP_SECRET.ToUpper());
            token = token.ToUpper();
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="Account"></param>
        /// <param name="Password">准考证号</param>
        /// <returns></returns>
        public static string Login(string Account, string Password, ref string outMessage, string test = "FALSE")
        {
            string response = null;
            string url = GetURL(URL_LOGIN);
            url += "?appid=" + APP_ID + "&appsecret=" + APP_SECRET + "&account=" + Account + "&pwd=" + Password + "&test=" + test;

            try
            {
                HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
                request.Method = "GET";
                request.ProtocolVersion = new Version(1, 1);
                HttpWebResponse httpResponse = request.GetResponse() as HttpWebResponse;
                if (httpResponse == null)
                    return null;

                response = GetResponse(httpResponse);
                httpResponse.Close();
                return response;
            }
            catch (Exception err)
            {
                outMessage = err.Message;
                return null;
            }
        }

        /// <summary>
        /// 根据班级名称获取班级信息
        /// </summary>
        /// <param name="className">班级名称</param>
        /// <returns></returns>
        public static string GetClass(string className, ref string outMessage)
        {
            string response = null;
            string url = GetURL(URL_CLASS);
            try
            {
                HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.Headers.Add("Cookie", "JSESSIONID = " + SESSION_ID);
                request.ProtocolVersion = new Version(1, 1);

                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendFormat("&{0}={1}", "className", className);
                byte[] buffer = Encoding.UTF8.GetBytes(stringBuilder.ToString().Trim('&'));
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(buffer, 0, buffer.Length);
                requestStream.Close();

                HttpWebResponse httpResponse = request.GetResponse() as HttpWebResponse;
                if (httpResponse == null)
                    return null;

                response = GetResponse(httpResponse);
                httpResponse.Close();
                return response;
            }
            catch (Exception err)
            {
                outMessage = err.Message;
                return null;
            }
        }

        /// <summary>
        /// 获取班级课程列表
        /// </summary>
        /// <param name="className">班级ID</param>
        /// <returns></returns>
        public static string GetLesson(long classId, ref string outMessage)
        {
            string response = null;
            string url = GetURL(URL_LESSON);
            try
            {
                HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.Headers.Add("Cookie", "JSESSIONID = " + SESSION_ID);
                request.ProtocolVersion = new Version(1, 1);

                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendFormat("&{0}={1}", "classId", classId);
                byte[] buffer = Encoding.UTF8.GetBytes(stringBuilder.ToString().Trim('&'));
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(buffer, 0, buffer.Length);
                requestStream.Close();

                HttpWebResponse httpResponse = request.GetResponse() as HttpWebResponse;
                if (httpResponse == null)
                    return null;

                response = GetResponse(httpResponse);
                httpResponse.Close();
                return response;
            }
            catch (Exception err)
            {
                outMessage = err.Message;
                return null;
            }
        }

        /// <summary>
        /// 签到
        /// </summary>
        /// <param name="classCourse">班级课程信息</param>
        /// <returns></returns>
        public static string SignUp(string classCourse, string idCard, int type, ref string outMessage)
        {
            string response = null;
            string url = GetURL(URL_SIGN_UP);
            try
            {
                HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.Headers.Add("Cookie", "JSESSIONID = " + SESSION_ID);
                request.ProtocolVersion = new Version(1, 1);

                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendFormat("&{0}={1}", "classCourse", classCourse);
                stringBuilder.AppendFormat("&{0}={1}", "idCard", idCard);
                stringBuilder.AppendFormat("&{0}={1}", "inOUt", type);
                byte[] buffer = Encoding.UTF8.GetBytes(stringBuilder.ToString().Trim('&'));
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(buffer, 0, buffer.Length);
                requestStream.Close();

                HttpWebResponse httpResponse = request.GetResponse() as HttpWebResponse;
                if (httpResponse == null)
                    return null;

                response = GetResponse(httpResponse);
                httpResponse.Close();
                return response;
            }
            catch (Exception err)
            {
                outMessage = err.Message;
                return null;
            }
        }

        /// <summary>
        /// 查询签到
        /// </summary>
        /// <param name="classCourse">班级课程信息</param>
        /// <returns></returns>
        public static string QuerySignUp(String classCourse, ref string outMessage)
        {
            string response = null;
            string url = GetURL(URL_QUREY_SIGN_UP);
            try
            {
                HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.Headers.Add("Cookie", "JSESSIONID = " + SESSION_ID);
                request.ProtocolVersion = new Version(1, 1);

                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendFormat("&{0}={1}", "classCourse", classCourse);
                byte[] buffer = Encoding.UTF8.GetBytes(stringBuilder.ToString().Trim('&'));
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(buffer, 0, buffer.Length);
                requestStream.Close();

                HttpWebResponse httpResponse = request.GetResponse() as HttpWebResponse;
                if (httpResponse == null)
                    return null;

                response = GetResponse(httpResponse);
                httpResponse.Close();
                return response;
            }
            catch (Exception err)
            {
                outMessage = err.Message;
                return null;
            }
        }


        /// <summary>
        /// 删除签到
        /// </summary>
        /// <param name="id">签到ID</param>
        /// <returns></returns>
        public static string SignOut(string id, ref string outMessage)
        {
            string response = null;
            string url = GetURL(URL_SIGN_OUT);
            try
            {
                HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.Headers.Add("Cookie", "JSESSIONID = " + SESSION_ID);
                request.ProtocolVersion = new Version(1, 1);

                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendFormat("&{0}={1}", "id", id);
                byte[] buffer = Encoding.UTF8.GetBytes(stringBuilder.ToString().Trim('&'));
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(buffer, 0, buffer.Length);
                requestStream.Close();

                HttpWebResponse httpResponse = request.GetResponse() as HttpWebResponse;
                if (httpResponse == null)
                    return null;

                response = GetResponse(httpResponse);
                httpResponse.Close();
                return response;
            }
            catch (Exception err)
            {
                outMessage = err.Message;
                return null;
            }
        }
        
        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        public static string Logout(ref string outMessage, string test = "FALSE")
        {
            string response = null;
            string url = GetURL(URL_LOGOUT);
            url += "?test=" + test;
            try
            {
                HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
                request.Method = "GET";
                request.Headers.Add("Cookie", "JSESSIONID = " + SESSION_ID);
                request.ProtocolVersion = new Version(1, 1);
                HttpWebResponse httpResponse = request.GetResponse() as HttpWebResponse;
                if (httpResponse == null)
                    return null;

                response = GetResponse(httpResponse);
                httpResponse.Close();
                return response;
            }
            catch (Exception err)
            {
                outMessage = err.Message;
                return null;
            }
        }

        private static string GetResponse(HttpWebResponse response)
        {
            string content = new StreamReader(response.GetResponseStream(), Encoding.UTF8).ReadToEnd();


            return content;
            /*
            string content = null;
            if (response.ContentLength <= 0)
            {
                return content;
            }
            using (Stream stream = response.GetResponseStream())
            {
                int totalLength = (int)response.ContentLength;
                int numBytesRead = 0;
                byte[] bytes = new byte[totalLength];
                //通过一个循环读取流中的数据，读取完毕，跳出循环
                while (numBytesRead < totalLength)
                {
                    int num = stream.Read(bytes, numBytesRead, 1024);  //每次希望读取1024字节
                    if (num == 0)   //说明流中数据读取完毕
                        break;
                    numBytesRead += num;
                }
                content = Encoding.UTF8.GetString(bytes);
            }

            return content;
            */
        }
    }
}
