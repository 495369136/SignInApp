using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log
{
    public class LogHelper
    {
        public static readonly log4net.ILog loginfo = log4net.LogManager.GetLogger("loginfo");
        //public static readonly log4net.ILog logerror = log4net.LogManager.GetLogger("logerror");

        public static void WriteDebugLog(string info)
        {
            if (loginfo.IsDebugEnabled)
            {
                loginfo.Debug(info);
            }
        }

        public static void WriteInfoLog(string info)
        {
            if (loginfo.IsInfoEnabled)
            {
                loginfo.Info(info);
            }
        }

        public static void WriteWarnLog(string info)
        {
            if (loginfo.IsWarnEnabled)
            {
                loginfo.Warn(info);
            }
        }

        public static void WriteErrorLog(string info)
        {
            if (loginfo.IsErrorEnabled)
            {
                loginfo.Error(info);
            }
        }

        public static void WriteFatalLog(string info)
        {
            if (loginfo.IsFatalEnabled)
            {
                loginfo.Fatal(info);
            }
        }

        //public static void WriteLog(string info, Exception ex)
        //{
        //    if (logerror.IsErrorEnabled)
        //    {
        //        logerror.Error(info, ex);
        //    }
        //}
    }
}
