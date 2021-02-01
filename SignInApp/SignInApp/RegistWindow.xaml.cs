using System;
using System.IO;
using System.Windows;

using WinForm = System.Windows.Forms;

namespace SignInApp
{
    /// <summary>
    /// RegistWindow.xaml 的交互逻辑
    /// </summary>
    public partial class RegistWindow : Window
    {
        public RegistWindow()
        {
            InitializeComponent();
            make_key_btn.Click += MakeRegistFileClick;
            regist_key_btn.Click += RegistCehckClick;
            this.Topmost = true;
            this.Top = 250;
            this.Left = 500;
        }

        private void RegistKeyMakeClick(object sender, EventArgs e)
        {
            string localFileName = string.Concat(
                                   RegistFileHelper.ApplicationDataPath,
                                   RegistFileHelper.ComputerInfofile);

            WinForm.OpenFileDialog openFileDialog = new WinForm.OpenFileDialog();
            if (openFileDialog.ShowDialog() == WinForm.DialogResult.OK)
            {
                string fileName = openFileDialog.FileName;
                computer_info_file.Text = fileName;

                if (fileName != localFileName)
                {
                    if (File.Exists(localFileName) == false)
                        File.Create(localFileName).Close(); ;
                    File.Copy(fileName, localFileName, true);
                }
                string computer = RegistFileHelper.ReadComputerInfoFile();
                EncryptionHelper help = new EncryptionHelper(EncryptionKeyEnum.KeyB);
                string md5String = help.GetMD5String(computer);
                string registInfo = help.EncryptString(md5String);
                RegistFileHelper.WriteRegistFile(registInfo);
            }
            else
            {
                return;
            }
        }

        private void MakeRegistFileClick(object sender, EventArgs e)
        {
            string localFileName = string.Concat(
                                   RegistFileHelper.ApplicationDataPath,
                                   RegistFileHelper.ComputerInfofile);

            WinForm.FolderBrowserDialog dialog = new WinForm.FolderBrowserDialog();
            if (dialog.ShowDialog() == WinForm.DialogResult.OK)
            {
                string fileName = dialog.SelectedPath + System.IO.Path.DirectorySeparatorChar + RegistFileHelper.ComputerInfofile;
                computer_info_file.Text = fileName;
                string computerInfo = ComputerInfo.GetComputerInfo();
                string encryptComputer = new EncryptionHelper().EncryptString(computerInfo);
                RegistFileHelper.WriteComputerInfoFile(encryptComputer);
                if (fileName != localFileName)
                {
                    if (File.Exists(fileName) == false)
                        File.Create(fileName).Close(); ;
                    File.Copy(localFileName, fileName, true);
                }
            }
            else
            {
                return;
            }
        }

        private void RegistCehckClick(object sender, EventArgs e)
        {
            if (RegistFileHelper.ExistComputerInfofile() == false)
            {
                System.Windows.MessageBox.Show("注册失败，请先生成本地key文件再向厂商申请...");
                return;
            }

            string computer = RegistFileHelper.ReadComputerInfoFile();
            EncryptionHelper help = new EncryptionHelper(EncryptionKeyEnum.KeyB);
            string md5String = help.GetMD5String(computer);
            string registInfo = help.EncryptString(md5String);

            if (RegistFileHelper.ExistRegistInfofile() == true)
            {
                string inputRegist = RegistFileHelper.ReadRegistFile();
                if (registInfo == inputRegist)
                {
                    LoginWindow LoginWindowPage = new LoginWindow();
                    LoginWindowPage.Show();
                    this.Close();
                    return;
                }
            }

            WinForm.OpenFileDialog openFileDialog = new WinForm.OpenFileDialog();
            if (openFileDialog.ShowDialog() == WinForm.DialogResult.OK)
            {
                string fileName = openFileDialog.FileName;
                license_info_file.Text = fileName;
                string localFileName = string.Concat(
                                       RegistFileHelper.ApplicationDataPath,
                                       RegistFileHelper.RegistInfofile);
                if (fileName != localFileName)
                {
                    if (File.Exists(localFileName) == false)
                        File.Create(localFileName).Close();
                    File.Copy(fileName, localFileName, true);
                }

                string inputRegist = RegistFileHelper.ReadRegistFile();
                if (registInfo == inputRegist)
                {
                    LoginWindow LoginWindowPage = new LoginWindow();
                    LoginWindowPage.Show();
                    this.Close();
                    return;
                }
                else
                {
                    System.Windows.MessageBox.Show("注册失败,请验证License...");
                    return;
                }
            }
            else
            {
                return;
            }
        }
    }
}
