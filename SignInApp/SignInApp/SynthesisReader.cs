using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SignInApp
{
    /// <summary>
    /// 神思：http://www.sdses.com/
    /// SS628(100)X
    /// </summary>

    public class IDCardReader
    {
        /// <summary>
        /// 自动检测身份证读卡器并初始化
        /// </summary>
        /// <returns>
        ///     true：成功
        ///     false：失败
        /// </returns>
        int InnerPort = 0;
        int para1 = 8811;
        //string para2 = "";
        byte[] para2 = { 0x02, 0x27, 0x00, 0x00 };
        public bool Initialize()
        {
            byte CMD = 0x41;
            //int para1 = 8811;
            //int para2 = 9986;
            for (int i = 1001; i <= 1016; i++)
            {
                if (UCommand1(ref CMD, ref i, ref para1, para2) == 62171)
                {
                    InnerPort = i;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 关闭读卡器
        /// </summary>
        /// <returns>
        ///     0：成功
        ///     -1：失败
        /// </returns>
        public int CloseComm()
        {
            byte CMD = 0x42;
            UCommand1(ref CMD, ref InnerPort, ref para1, para2);

            return 0;
        }

        /// <summary>
        /// 验证卡
        /// </summary>
        /// <returns>
        ///     0：验卡成功
        ///     -1：验卡失败
        /// </returns>
        public int Authenticate()
        {
            byte CMD = 0x43;
            //int para1 = 8811;
            //int para2 = 9986;
            int nRet = UCommand1(ref CMD, ref InnerPort, ref para1, para2);
            Console.WriteLine(nRet);
            if (nRet == 62171 || nRet == 62172 || nRet == 62173)
                return 0;
            else
                return -1;
        }

        /// <summary>
        /// 读卡基本信息
        /// </summary>
        /// <returns>
        ///     0：读卡成功，卡片不带指纹数据
        ///     1：读卡成功，卡片带指纹数据
        ///     -1：读卡失败
        /// </returns>
        public int ReadContent()
        {
            /// </summary>
            byte CMD = 0x44;
            //int para1 = 8811;
            //int para2 = 9986;
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\HZCQT";
            int nRet = UCommand1(ref CMD, ref InnerPort, ref para1, Encoding.GetEncoding("GBK").GetBytes(path));
            //Console.WriteLine(nRet);
            if (nRet == 62171) return 0; else if (nRet == 62172) return 1; else return -1;
        }

        public int GetNameGBK(ref string str_name)
        {
            byte[] name = new byte[250];
            int nRet = GetName(name);
            this.trimSpace(name);
            str_name = System.Text.Encoding.GetEncoding("GBK").GetString(name).Replace("\x00", "");
            return nRet;
        }

        public int GetSexGBGBK(ref string str_sex)
        {
            byte[] sex = new byte[250];
            int nRet = GetSexGB(sex);
            this.trimSpace(sex);
            str_sex = System.Text.Encoding.GetEncoding("GBK").GetString(sex).Replace("\x00", "");
            return nRet;
        }

        public int GetFolkGBGBK(ref string str_folk)
        {
            byte[] folk = new byte[250];
            int nRet = GetFolkGB(folk);
            this.trimSpace(folk);
            str_folk = System.Text.Encoding.GetEncoding("GBK").GetString(folk).Replace("\x00", "");
            return nRet;
        }

        public int GetBirthGBK(ref string str_birth)
        {
            byte[] birth = new byte[250];
            int nRet = GetBirth(birth);
            this.trimSpace(birth);
            str_birth = System.Text.Encoding.GetEncoding("GBK").GetString(birth).Replace("\x00", "");
            return nRet;
        }

        public int GetAddrGBK(ref string str_addr)
        {
            byte[] addr = new byte[250];
            int nRet = GetAddr(addr);
            this.trimSpace(addr);
            str_addr = System.Text.Encoding.GetEncoding("GBK").GetString(addr).Replace("\x00", "");
            return nRet;
        }

        public int GetIDNumGBK(ref string str_IDNum)
        {
            byte[] IDNum = new byte[250];
            int nRet = GetIDNum(IDNum);
            this.trimSpace(IDNum);
            str_IDNum = System.Text.Encoding.GetEncoding("GBK").GetString(IDNum).Replace("\x00", "");
            return nRet;
        }

        public int GetDepGBK(ref string str_dep)
        {
            byte[] dep = new byte[250];
            int nRet = GetDep(dep);
            this.trimSpace(dep);
            str_dep = System.Text.Encoding.GetEncoding("GBK").GetString(dep).Replace("\x00", "");
            return nRet;
        }

        public int GetBeginGBK(ref string str_begin)
        {
            byte[] begin = new byte[250];
            int nRet = GetBegin(begin);
            this.trimSpace(begin);
            str_begin = System.Text.Encoding.GetEncoding("GBK").GetString(begin).Replace("\x00", "");
            return nRet;
        }

        public int GetEndGBK(ref string str_end)
        {
            byte[] end = new byte[250];
            int nRet = GetEnd(end);
            this.trimSpace(end);
            str_end = System.Text.Encoding.GetEncoding("GBK").GetString(end).Replace("\x00", "");
            return nRet;
        }

        private void trimSpace(byte[] input)
        {
            int len = input.Length;
            if (len == 0)
                return;

            for (int i = len - 1; i >= 0; i--)
            {
                if (input[i] == 0x00
                    || input[i] == 0x20
                    || input[i] == 0x09
                    || input[i] == 0x0d
                    || input[i] == 0x0a)
                {
                    input[i] = 0;
                }
                else
                    return;
            }
        }

        [DllImport("RdCard.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int UCommand1(ref byte pCmd, ref int parg0, ref int parg1, byte[] parg2);
        [DllImport("RdCard.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int GetName(byte[] name);//读取名称
        [DllImport("RdCard.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int GetSexGB(byte[] buf); //读取性别
        [DllImport("RdCard.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int GetFolkGB(byte[] buf); //读取民族
        [DllImport("RdCard.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int GetBirth(byte[] buf); //读取出生
        [DllImport("RdCard.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int GetAddr(byte[] buf); //读取住址
        [DllImport("RdCard.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int GetIDNum(byte[] buf); //读取公民身份号码
        [DllImport("RdCard.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int GetDep(byte[] buf); //读取签发机关
        [DllImport("RdCard.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int GetBegin(byte[] buf); //读取有效期起
        [DllImport("RdCard.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int GetEnd(byte[] buf); //读取有效期止
    }
}
