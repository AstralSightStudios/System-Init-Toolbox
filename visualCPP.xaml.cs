using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows;

namespace System_Init_Toolbox
{
    /// <summary>
    /// visualCPP.xaml 的交互逻辑
    /// </summary>
    public partial class visualCPP
    {
        private bool downloading = false;
        private Stream wb;
        WebResponse response;
        WebRequest request;
        public visualCPP()
        {
            InitializeComponent();
            this.Closing += _download_window_closing;
        }
        public void _download_window_closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (downloading)
            {
                //若窗口关闭即停止下载
                wb.Close();
                response.Close();
                request.Abort();
                Environment.Exit(0);
            }
        }
        public static void Delay(int mm)
        {
            DateTime current = DateTime.Now;
            while (current.AddMilliseconds(mm) > DateTime.Now)
            {
                System.Windows.Forms.Application.DoEvents();
            }
            return;
        }
        private static string BytesToString(decimal Bytes)
        {
            decimal Kb = Bytes / 1024;
            decimal Mb = Kb / 1024;
            decimal Gb = Mb / 1024;
            if (Gb > 1000)
            {
                return string.Format("{0:0.0} TB", Gb / 1024);
            }
            else if (Mb > 1000)
            {
                return string.Format("{0:0.0} GB", Gb);
            }
            else if (Kb > 1000)
            {
                return string.Format("{0:0.0} MB", Mb);
            }
            else
            {
                return string.Format("{0:0} KB", Kb);
            }
        }
        public static bool DownloadFile(string URL, FileStream fs, System.Windows.Controls.ProgressBar progressBar1, System.Windows.Controls.Label label1, visualCPP visualCPP)
        {
            try
            {
                WebHeaderCollection wb_nb = new WebHeaderCollection();
                wb_nb.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.110 Safari/537.36 Edg/96.0.1054.62");
                visualCPP.request = (HttpWebRequest)WebRequest.Create(URL);
                visualCPP.request.Headers = wb_nb;
                visualCPP.response = (HttpWebResponse)visualCPP.request.GetResponse();
                long totalLength = visualCPP.response.ContentLength;
                progressBar1.Maximum = (int)totalLength;
                visualCPP.wb = visualCPP.response.GetResponseStream();
                //把stream wb改为全局变量 方便进行.close操作
                long currentLength = 0;
                byte[] by = new byte[1024];
                int osize = visualCPP.wb.Read(by, 0, by.Length);
                while (osize > 0)
                {
                    DispatcherHelper.DoEvents();
                    currentLength = osize + currentLength;
                    fs.Write(by, 0, osize);
                    progressBar1.Value = (int)currentLength;
                    label1.Content = string.Format("{0} / {1}", BytesToString(currentLength), BytesToString(totalLength));
                    osize = visualCPP.wb.Read(by, 0, by.Length);
                }
                fs.Close();
                visualCPP.wb.Close();
                return (currentLength == totalLength);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
                return false;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            down_and_install_button.IsEnabled = false;
            downloading = true;
            status_label.Content = "正在下载VC++ 2005 x86";
            progressBar1.IsIndeterminate = true;
            Delay(3000);
            FileStream fs_2005_x86 = new FileStream("vc2005-x86.exe",FileMode.OpenOrCreate);
            DownloadFile("https://gitee.com/search__stars/uris_-system_-init_-toolbox/attach_files/931939/download/vcredist_x86.EXE",fs_2005_x86,progressBar1,label1,this);
            fs_2005_x86.Close();
            status_label.Content = "正在下载VC++ 2005 x64";
            progressBar1.IsIndeterminate = true;
            Delay(3000);
            FileStream fs_2005_x64 = new FileStream("vc2005-x64.exe", FileMode.OpenOrCreate);
            DownloadFile("https://gitee.com/search__stars/uris_-system_-init_-toolbox/attach_files/931940/download/vcredist_x64.EXE", fs_2005_x64, progressBar1, label1, this);
            fs_2005_x64.Close();
            status_label.Content = "正在下载VC++ 2008 x86";
            progressBar1.IsIndeterminate = true;
            Delay(3000);
            FileStream fs_2008_x86 = new FileStream("vc2008-x86.exe", FileMode.OpenOrCreate);
            DownloadFile("https://gitee.com/search__stars/uris_-system_-init_-toolbox/attach_files/931941/download/vcredist_x86.exe", fs_2008_x86, progressBar1, label1, this);
            fs_2008_x86.Close();
            status_label.Content = "正在下载VC++ 2008 x64";
            progressBar1.IsIndeterminate = true;
            Delay(3000);
            FileStream fs_2008_x64 = new FileStream("vc2008-x64.exe", FileMode.OpenOrCreate);
            DownloadFile("https://gitee.com/search__stars/uris_-system_-init_-toolbox/attach_files/931932/download/vcredist_x64.exe", fs_2008_x64, progressBar1, label1, this);
            fs_2008_x64.Close();
            status_label.Content = "正在下载VC++ 2010 x86";
            progressBar1.IsIndeterminate = true;
            Delay(3000);
            FileStream fs_2010_x86 = new FileStream("vc2010-x86.exe", FileMode.OpenOrCreate);
            DownloadFile("https://gitee.com/search__stars/uris_-system_-init_-toolbox/attach_files/931946/download/vcredist_x86.exe", fs_2010_x86, progressBar1, label1, this);
            fs_2010_x86.Close();
            status_label.Content = "正在下载VC++ 2010 x64";
            progressBar1.IsIndeterminate = true;
            Delay(3000);
            FileStream fs_2010_x64 = new FileStream("vc2010-x64.exe", FileMode.OpenOrCreate);
            DownloadFile("https://gitee.com/search__stars/uris_-system_-init_-toolbox/attach_files/931935/download/vcredist_x64.exe", fs_2010_x64, progressBar1, label1, this);
            fs_2010_x64.Close();
            status_label.Content = "正在下载VC++ 2012 x86";
            progressBar1.IsIndeterminate = true;
            Delay(3000);
            FileStream fs_2012_x86 = new FileStream("vc2012-x86.exe", FileMode.OpenOrCreate);
            DownloadFile("https://gitee.com/search__stars/uris_-system_-init_-toolbox/attach_files/931934/download/vcredist_x86.exe", fs_2012_x86, progressBar1, label1, this);
            fs_2012_x86.Close();
            status_label.Content = "正在下载VC++ 2012 x64";
            progressBar1.IsIndeterminate = true;
            Delay(3000);
            FileStream fs_2012_x64 = new FileStream("vc2012-x64.exe", FileMode.OpenOrCreate);
            DownloadFile("https://gitee.com/search__stars/uris_-system_-init_-toolbox/attach_files/931938/download/vcredist_x64.exe", fs_2012_x64, progressBar1, label1, this);
            fs_2012_x64.Close();
            status_label.Content = "正在下载VC++ 2013 x86";
            progressBar1.IsIndeterminate = true;
            Delay(3000);
            FileStream fs_2013_x86 = new FileStream("vc2013-x86.exe", FileMode.OpenOrCreate);
            DownloadFile("https://gitee.com/search__stars/uris_-system_-init_-toolbox/attach_files/931936/download/vcredist_x86.exe", fs_2013_x86, progressBar1, label1, this);
            fs_2013_x86.Close();
            status_label.Content = "正在下载VC++ 2013 x64";
            progressBar1.IsIndeterminate = true;
            Delay(3000);
            FileStream fs_2013_x64 = new FileStream("vc2013-x64.exe", FileMode.OpenOrCreate);
            DownloadFile("https://gitee.com/search__stars/uris_-system_-init_-toolbox/attach_files/931937/download/vcredist_x64.exe", fs_2013_x64, progressBar1, label1, this);
            fs_2013_x64.Close();
            status_label.Content = "正在下载VC++ 2015-2022 x86";
            progressBar1.IsIndeterminate = true;
            Delay(3000);
            FileStream fs_2015_x86 = new FileStream("vc2015-x86.exe", FileMode.OpenOrCreate);
            DownloadFile("https://gitee.com/search__stars/uris_-system_-init_-toolbox/attach_files/931943/download/VC_redist.x86.exe", fs_2015_x86, progressBar1, label1, this);
            fs_2015_x86.Close();
            status_label.Content = "正在下载VC++ 2015-2022 x64";
            progressBar1.IsIndeterminate = true;
            Delay(3000);
            FileStream fs_2015_x64 = new FileStream("vc2015-x64.exe", FileMode.OpenOrCreate);
            DownloadFile("https://gitee.com/search__stars/uris_-system_-init_-toolbox/attach_files/931944/download/VC_redist.x64.exe", fs_2015_x64, progressBar1, label1, this);
            fs_2015_x64.Close();
            status_label.Content = "正在下载VC++ 2015-2022 arm64";
            progressBar1.IsIndeterminate = true;
            Delay(3000);
            FileStream fs_2015_arm64 = new FileStream("vc2015-arm64.exe", FileMode.OpenOrCreate);
            DownloadFile("https://gitee.com/search__stars/uris_-system_-init_-toolbox/attach_files/931942/download/VC_redist.arm64.exe", fs_2015_arm64, progressBar1, label1, this);
            fs_2015_arm64.Close();
            progressBar1.IsIndeterminate = true;
            status_label.Content = "正在安装VC++ 2005 x86";
            Delay(3000);
            Process vc2005_Process = new Process();
            ProcessStartInfo startInfo_vc2005 = new ProcessStartInfo("vc2005-x86.exe", "/q");
            vc2005_Process.StartInfo = startInfo_vc2005;
            vc2005_Process.StartInfo.CreateNoWindow = ToggleSwitch_NoWindowInstall.IsOn;
            vc2005_Process.Start();
            vc2005_Process.WaitForExit();
            status_label.Content = "正在安装VC++ 2005 x64";
            Delay(3000);
            Process vc2005_Process64 = new Process();
            ProcessStartInfo startInfo_vc2005_64 = new ProcessStartInfo("vc2005-x64.exe", "/q");
            vc2005_Process64.StartInfo = startInfo_vc2005_64;
            vc2005_Process64.Start();
            vc2005_Process64.WaitForExit();
            status_label.Content = "正在安装VC++ 2008 x86";
            Delay(3000);
            progressBar1.IsIndeterminate = true;
            label1.Content = " ";
            Process vc2008_Process = new Process();
            ProcessStartInfo startInfo_vc2008 = new ProcessStartInfo("vc2008-x86.exe", "/q");
            vc2008_Process.StartInfo = startInfo_vc2008;
            vc2008_Process.StartInfo.CreateNoWindow = ToggleSwitch_NoWindowInstall.IsOn;
            vc2008_Process.Start();
            vc2008_Process.WaitForExit();
            status_label.Content = "正在安装VC++ 2008 x64";
            Delay(3000);
            Process vc2008_Process64 = new Process();
            ProcessStartInfo startInfo_vc2008_64 = new ProcessStartInfo("vc2008-x64.exe", "/q");
            vc2008_Process64.StartInfo = startInfo_vc2008_64;
            vc2008_Process64.Start();
            vc2008_Process64.WaitForExit();
            status_label.Content = "正在安装VC++2010 x86";
            Delay(3000);
            progressBar1.IsIndeterminate = true;
            label1.Content = " ";
            Process vc2010_Process = new Process();
            ProcessStartInfo startInfo_vc2010 = new ProcessStartInfo("vc2010-x86.exe", "Setup /q");
            vc2010_Process.StartInfo = startInfo_vc2010;
            vc2010_Process.StartInfo.CreateNoWindow = ToggleSwitch_NoWindowInstall.IsOn;
            vc2010_Process.Start();
            vc2010_Process.WaitForExit();
            status_label.Content = "正在安装VC++ 2010 x64";
            Delay(3000);
            Process vc2010_Process64 = new Process();
            ProcessStartInfo startInfo_vc2010_64 = new ProcessStartInfo("vc2010-x64.exe", "Setup /q");
            vc2010_Process64.StartInfo = startInfo_vc2010_64;
            vc2010_Process64.Start();
            vc2010_Process64.WaitForExit();
            status_label.Content = "正在安装VC++2012 Update4 x86";
            Delay(3000);
            progressBar1.IsIndeterminate = true;
            label1.Content = " ";
            Process vc2012_Process = new Process();
            ProcessStartInfo startInfo_vc2012 = new ProcessStartInfo("vc2012-x86.exe", "/install /quiet");
            vc2012_Process.StartInfo = startInfo_vc2012;
            vc2012_Process.StartInfo.CreateNoWindow = ToggleSwitch_NoWindowInstall.IsOn;
            vc2012_Process.Start();
            vc2012_Process.WaitForExit();
            status_label.Content = "正在安装VC++ 2012 Update4 x64";
            Delay(3000);
            Process vc2012_Process64 = new Process();
            ProcessStartInfo startInfo_vc2012_64 = new ProcessStartInfo("vc2012-x64.exe", "/install /quiet");
            vc2012_Process64.StartInfo = startInfo_vc2012_64;
            vc2012_Process64.Start();
            vc2012_Process64.WaitForExit();
            status_label.Content = "正在安装VC++2013 x86";
            Delay(3000);
            progressBar1.IsIndeterminate = true;
            label1.Content = " ";
            Process vc2013_Process = new Process();
            ProcessStartInfo startInfo_vc2013 = new ProcessStartInfo("vc2013-x86.exe", "/install /quiet");
            vc2013_Process.StartInfo = startInfo_vc2013;
            vc2013_Process.StartInfo.CreateNoWindow = ToggleSwitch_NoWindowInstall.IsOn;
            vc2013_Process.Start();
            vc2013_Process.WaitForExit();
            status_label.Content = "正在安装VC++ 2013 x64";
            Delay(3000);
            Process vc2013_Process64 = new Process();
            ProcessStartInfo startInfo_vc2013_64 = new ProcessStartInfo("vc2013-x64.exe", "/install /quiet");
            vc2013_Process64.StartInfo = startInfo_vc2013_64;
            vc2013_Process64.Start();
            vc2013_Process64.WaitForExit();
            status_label.Content = "正在安装VC++2015-2017-2019-2022 x86";
            Delay(3000);
            progressBar1.IsIndeterminate = true;
            label1.Content = " ";
            Process vc2015_Process = new Process();
            ProcessStartInfo startInfo_vc2015 = new ProcessStartInfo("vc2015-x86.exe", "/install /quiet");
            vc2015_Process.StartInfo = startInfo_vc2015;
            vc2015_Process.StartInfo.CreateNoWindow = ToggleSwitch_NoWindowInstall.IsOn;
            vc2015_Process.Start();
            vc2015_Process.WaitForExit();
            status_label.Content = "正在安装VC++2015-2017-2019-2022 x64";
            Delay(3000);
            Process vc2015_Process64 = new Process();
            ProcessStartInfo startInfo_vc2015_64 = new ProcessStartInfo("vc2015-x64.exe", "/install /quiet");
            vc2015_Process64.StartInfo = startInfo_vc2015_64;
            vc2015_Process64.Start();
            vc2015_Process64.WaitForExit();
            status_label.Content = "正在安装VC++2015-2017-2019-2022 arm64";
            Delay(3000);
            Process vc2015_Processarm64 = new Process();
            ProcessStartInfo startInfo_vc2015_arm64 = new ProcessStartInfo("vc2015-arm64.exe", "/install /quiet");
            vc2015_Processarm64.StartInfo = startInfo_vc2015_arm64;
            vc2015_Processarm64.Start();
            vc2015_Processarm64.WaitForExit();
            progressBar1.IsIndeterminate = true;
            if (ToggleSwitch_AfterInstallRemove_exe.IsOn)
            {
                status_label.Content = "清理残留中...";
                Delay(3000);
                File.Delete("./vc2005-x86.exe");
                File.Delete("./vc2005-x64.exe");
                File.Delete("./vc2008-x86.exe");
                File.Delete("./vc2008-x64.exe");
                File.Delete("./vc2010-x86.exe");
                File.Delete("./vc2010-x64.exe");
                File.Delete("./vc2012-x86.exe");
                File.Delete("./vc2012-x64.exe");
                File.Delete("./vc2013-x86.exe");
                File.Delete("./vc2013-x64.exe");
                File.Delete("./vc2015-x86.exe");
                File.Delete("./vc2015-x64.exe");
                File.Delete("./vc2015-arm64.exe");
            }
            progressBar1.IsIndeterminate = false;
            progressBar1.Value = 100;
            status_label.Content = "全部安装完成！";
            downloading = false;
        }
    }
}
