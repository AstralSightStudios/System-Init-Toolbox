using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows;

namespace System_Init_Toolbox
{
    /// <summary>
    /// net_framework.xaml 的交互逻辑
    /// </summary>
    public partial class net_framework
    {
        private bool downloading = false;
        private Stream wb;
        WebResponse response;
        WebRequest request;
        public net_framework()
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
        public static bool DownloadFile(string URL, FileStream fs, System.Windows.Controls.ProgressBar progressBar1, System.Windows.Controls.Label label1, net_framework net_framework)
        {
            try
            {
                WebHeaderCollection wb_nb = new WebHeaderCollection();
                wb_nb.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.110 Safari/537.36 Edg/96.0.1054.62");
                net_framework.request = (HttpWebRequest)WebRequest.Create(URL);
                net_framework.request.Headers = wb_nb;
                net_framework.response = (HttpWebResponse)net_framework.request.GetResponse();
                long totalLength = net_framework.response.ContentLength;
                progressBar1.Maximum = (int)totalLength;
                net_framework.wb = net_framework.response.GetResponseStream();
                //把stream wb改为全局变量 方便进行.close操作
                long currentLength = 0;
                byte[] by = new byte[1024];
                int osize = net_framework.wb.Read(by, 0, by.Length);
                while (osize > 0)
                {
                    DispatcherHelper.DoEvents();
                    currentLength = osize + currentLength;
                    fs.Write(by, 0, osize);
                    progressBar1.Value = (int)currentLength;
                    label1.Content = string.Format("{0} / {1}", BytesToString(currentLength), BytesToString(totalLength));
                    osize = net_framework.wb.Read(by, 0, by.Length);
                }
                fs.Close();
                net_framework.wb.Close();
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
            progressBar1.IsIndeterminate = true;
            status_label.Content = "正在安装.NET Framework 3.5";
            Delay(3000);
            Process dotnet35_Process = new Process();
            ProcessStartInfo startInfo_35 = new ProcessStartInfo("./dotnetfx35.exe");
            dotnet35_Process.StartInfo = startInfo_35;
            dotnet35_Process.StartInfo.CreateNoWindow = ToggleSwitch_NoWindowInstall.IsOn;
            dotnet35_Process.Start();
            dotnet35_Process.WaitForExit();
            status_label.Content = "正在安装.NET Framework 4.5.2";
            Delay(3000);
            progressBar1.IsIndeterminate = true;
            label1.Content = " ";
            Process dotnet452_Process = new Process();
            ProcessStartInfo startInfo_452 = new ProcessStartInfo("./netframework452.exe", "/q /norestart /ChainingPackage FullX64Bootstrapper");
            dotnet452_Process.StartInfo = startInfo_452;
            dotnet452_Process.StartInfo.CreateNoWindow = ToggleSwitch_NoWindowInstall.IsOn;
            dotnet452_Process.Start();
            dotnet452_Process.WaitForExit();
            status_label.Content = "正在安装.NET Framework 4.6";
            Delay(3000);
            Process dotnet46_Process = new Process();
            ProcessStartInfo startInfo_46 = new ProcessStartInfo("./netframework46.exe", "/q /norestart /ChainingPackage FullX64Bootstrapper");
            dotnet46_Process.StartInfo = startInfo_46;
            dotnet46_Process.StartInfo.CreateNoWindow = ToggleSwitch_NoWindowInstall.IsOn;
            dotnet46_Process.Start();
            dotnet46_Process.WaitForExit();
            status_label.Content = "正在安装.NET Framework 4.6.1";
            Delay(3000);
            Process dotnet461_Process = new Process();
            ProcessStartInfo startInfo_461 = new ProcessStartInfo("./netframework461.exe", "/q /norestart /ChainingPackage FullX64Bootstrapper");
            dotnet461_Process.StartInfo = startInfo_461;
            dotnet461_Process.StartInfo.CreateNoWindow = ToggleSwitch_NoWindowInstall.IsOn;
            dotnet461_Process.Start();
            dotnet461_Process.WaitForExit();
            status_label.Content = "正在安装.NET Framework 4.6.2";
            Delay(3000);
            Process dotnet462_Process = new Process();
            ProcessStartInfo startInfo_462 = new ProcessStartInfo("./netframework462.exe", "/q /norestart /ChainingPackage FullX64Bootstrapper");
            dotnet462_Process.StartInfo = startInfo_462;
            dotnet462_Process.StartInfo.CreateNoWindow = ToggleSwitch_NoWindowInstall.IsOn;
            dotnet462_Process.Start();
            dotnet462_Process.WaitForExit();
            status_label.Content = "正在安装.NET Framework 4.7";
            Delay(3000);
            Process dotnet47_Process = new Process();
            ProcessStartInfo startInfo_47 = new ProcessStartInfo("./netframework47.exe", "/q /norestart /ChainingPackage FullX64Bootstrapper");
            dotnet47_Process.StartInfo = startInfo_47;
            dotnet47_Process.StartInfo.CreateNoWindow = ToggleSwitch_NoWindowInstall.IsOn;
            dotnet47_Process.Start();
            dotnet47_Process.WaitForExit();
            status_label.Content = "正在安装.NET Framework 4.7.1";
            Delay(3000);
            Process dotnet471_Process = new Process();
            ProcessStartInfo startInfo_471 = new ProcessStartInfo("./netframework471.exe", "/q /norestart /ChainingPackage FullX64Bootstrapper");
            dotnet471_Process.StartInfo = startInfo_471;
            dotnet471_Process.StartInfo.CreateNoWindow = ToggleSwitch_NoWindowInstall.IsOn;
            dotnet471_Process.Start();
            dotnet471_Process.WaitForExit();
            status_label.Content = "正在安装.NET Framework 4.7.2";
            Delay(3000);
            Process dotnet472_Process = new Process();
            ProcessStartInfo startInfo_472 = new ProcessStartInfo("./netframework472.exe", "/q /norestart /ChainingPackage FullX64Bootstrapper");
            dotnet472_Process.StartInfo = startInfo_472;
            dotnet472_Process.StartInfo.CreateNoWindow = ToggleSwitch_NoWindowInstall.IsOn;
            dotnet472_Process.Start();
            dotnet472_Process.WaitForExit();
            status_label.Content = "正在安装.NET Framework 4.8";
            Delay(3000);
            Process dotnet48_Process = new Process();
            ProcessStartInfo startInfo_48 = new ProcessStartInfo("./netframework48.exe", "/q /norestart /ChainingPackage FullX64Bootstrapper");
            dotnet48_Process.StartInfo = startInfo_48;
            dotnet48_Process.StartInfo.CreateNoWindow = ToggleSwitch_NoWindowInstall.IsOn;
            dotnet48_Process.Start();
            dotnet48_Process.WaitForExit();
            if (ToggleSwitch_AfterInstallRemove_exe.IsOn)
            {
                status_label.Content = "清理残留中...";
                Delay(3000);
                File.Delete("./netframework452.exe");
                File.Delete("./netframework46.exe");
                File.Delete("./netframework461.exe");
                File.Delete("./netframework462.exe");
                File.Delete("./netframework47.exe");
                File.Delete("./netframework471.exe");
                File.Delete("./netframework472.exe");
                File.Delete("./netframework48.exe");
            }
            progressBar1.IsIndeterminate = false;
            progressBar1.Value = 100;
            status_label.Content = "全部安装完成！";
            downloading = false;
        }
    }
}
