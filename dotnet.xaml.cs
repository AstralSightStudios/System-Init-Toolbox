using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows;

namespace System_Init_Toolbox
{
    /// <summary>
    /// dotnet.xaml 的交互逻辑
    /// </summary>
    public partial class dotnet
    {
        private bool downloading = false;
        private Stream wb;
        WebResponse response;
        WebRequest request;
        public dotnet()
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
        public static bool DownloadFile(string URL, FileStream fs, System.Windows.Controls.ProgressBar progressBar1, System.Windows.Controls.Label label1, dotnet dotnet)
        {
            try
            {
                WebHeaderCollection wb_nb = new WebHeaderCollection();
                wb_nb.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.110 Safari/537.36 Edg/96.0.1054.62");
                dotnet.request = (HttpWebRequest)WebRequest.Create(URL);
                dotnet.request.Headers = wb_nb;
                dotnet.response = (HttpWebResponse)dotnet.request.GetResponse();
                long totalLength = dotnet.response.ContentLength;
                progressBar1.Maximum = (int)totalLength;
                dotnet.wb = dotnet.response.GetResponseStream();
                //把stream wb改为全局变量 方便进行.close操作
                long currentLength = 0;
                byte[] by = new byte[1024];
                int osize = dotnet.wb.Read(by, 0, by.Length);
                while (osize > 0)
                {
                    DispatcherHelper.DoEvents();
                    currentLength = osize + currentLength;
                    fs.Write(by, 0, osize);
                    progressBar1.Value = (int)currentLength;
                    label1.Content = string.Format("{0} / {1}", BytesToString(currentLength), BytesToString(totalLength));
                    osize = dotnet.wb.Read(by, 0, by.Length);
                }
                fs.Close();
                dotnet.wb.Close();
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
            FileStream down_31_fs = new FileStream("netcore31.exe", FileMode.OpenOrCreate);
            status_label.Content = "正在下载.NET Core 3.1";
            DownloadFile("https://download.visualstudio.microsoft.com/download/pr/1c14e24b-7f31-42dc-ba3c-83295a2d6f7e/41b93591162dfe556cc160ae44fbe75e/windowsdesktop-runtime-3.1.22-win-x64.exe", down_31_fs, progressBar1, label1, this);
            wb.Close();
            down_31_fs.Close();
            status_label.Content = "正在下载.NET 5.0";
            FileStream down_50_fs = new FileStream("net50.exe", FileMode.OpenOrCreate);
            DownloadFile("https://download.visualstudio.microsoft.com/download/pr/2bfb80f2-b8f2-44b0-90c1-d3c8c1c8eac8/409dd3d3367feeeda048f4ff34b32e82/windowsdesktop-runtime-5.0.13-win-x64.exe", down_50_fs, progressBar1, label1, this);
            wb.Close();
            down_50_fs.Close();
            status_label.Content = "正在下载.NET 6.0";
            FileStream down_60_fs = new FileStream("net60.exe", FileMode.OpenOrCreate);
            DownloadFile("https://download.visualstudio.microsoft.com/download/pr/df4372ca-82c8-4bfa-acf9-c49e27279e7e/6bddefd26964017ff520dc1443029e04/dotnet-runtime-6.0.1-win-x64.exe", down_60_fs, progressBar1, label1, this);
            wb.Close();
            down_60_fs.Close();
            down_and_install_button.IsEnabled = false;
            progressBar1.IsIndeterminate = true;
            status_label.Content = "正在安装.NET Core 3.1";
            Delay(3000);
            Process dotnet_core31_Process = new Process();
            ProcessStartInfo startInfo_core31 = new ProcessStartInfo("./netcore31.exe", "/q /norestart /ChainingPackage FullX64Bootstrapper");
            dotnet_core31_Process.StartInfo = startInfo_core31;
            dotnet_core31_Process.StartInfo.CreateNoWindow = ToggleSwitch_NoWindowInstall.IsOn;
            dotnet_core31_Process.Start();
            dotnet_core31_Process.WaitForExit();
            status_label.Content = "正在安装.NET 5.0";
            Delay(3000);
            progressBar1.IsIndeterminate = true;
            label1.Content = " ";
            Process dotnet50_Process = new Process();
            ProcessStartInfo startInfo_50 = new ProcessStartInfo("./net50.exe", "/q /norestart /ChainingPackage FullX64Bootstrapper");
            dotnet50_Process.StartInfo = startInfo_50;
            dotnet50_Process.StartInfo.CreateNoWindow = ToggleSwitch_NoWindowInstall.IsOn;
            dotnet50_Process.Start();
            dotnet50_Process.WaitForExit();
            status_label.Content = "正在安装.NET 6.0";
            Delay(3000);
            Process dotnet60_Process = new Process();
            ProcessStartInfo startInfo_60 = new ProcessStartInfo("./net60.exe", "/q /norestart /ChainingPackage FullX64Bootstrapper");
            dotnet60_Process.StartInfo = startInfo_60;
            dotnet60_Process.StartInfo.CreateNoWindow = ToggleSwitch_NoWindowInstall.IsOn;
            dotnet60_Process.Start();
            dotnet60_Process.WaitForExit();
            progressBar1.IsIndeterminate = true;
            if (ToggleSwitch_AfterInstallRemove_exe.IsOn)
            {
                status_label.Content = "清理残留中...";
                Delay(3000);
                File.Delete("./netcore31.exe");
                File.Delete("./net50.exe");
                File.Delete("./net60.exe");
            }
            progressBar1.IsIndeterminate = false;
            progressBar1.Value = 100;
            status_label.Content = "全部安装完成！";
            downloading = false;
        }
    }
}
