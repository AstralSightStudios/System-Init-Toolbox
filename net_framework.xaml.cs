using System;
using System.Windows;
using System.Net;
using System.IO;
using System.Diagnostics;

namespace System_Init_Toolbox
{
    /// <summary>
    /// net_framework.xaml 的交互逻辑
    /// </summary>
    public partial class net_framework
    {
        private bool downloading = false;
        private Stream wb;
        public net_framework()
        {
            InitializeComponent();
        }
        public void _download_window_closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (downloading)
            {
                //若窗口关闭即停止下载
                wb.Close();
            }
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
                WebRequest request = (HttpWebRequest)WebRequest.Create(URL);
                request.Headers = wb_nb;
                WebResponse response = (HttpWebResponse)request.GetResponse();
                long totalLength = response.ContentLength;
                progressBar1.Maximum = (int)totalLength;
                net_framework.wb = response.GetResponseStream();
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
            Process bat35_Process = new Process();
            bat35_Process.StartInfo.FileName = "./BATFiles/dotnet-framework-3.5-on.bat";
            bat35_Process.StartInfo.CreateNoWindow = true;
            bat35_Process.Start();
            progressBar1.IsIndeterminate=false;
            FileStream down_452_fs = new FileStream("netframework452.exe",FileMode.OpenOrCreate);
            status_label.Content = "正在下载.NET Framework 4.5.2";
            downloading = true;
            DownloadFile("https://gitee.com/search__stars/uris_-system_-init_-toolbox/attach_files/924577/download/netframework452.exe", down_452_fs, progressBar1, label1, this);
            wb.Close();
            downloading = false;
            status_label.Content = "正在安装.NET Framework 4.5.2";
            progressBar1.IsIndeterminate = true;
            label1.Content = " ";
            Process dotnet452_Process = new Process();
            ProcessStartInfo startInfo_452 = new ProcessStartInfo("./netframework452.exe", "/q /norestart /ChainingPackage FullX64Bootstrapper");
            dotnet452_Process.StartInfo = startInfo_452;
            dotnet452_Process.StartInfo.CreateNoWindow = true;
            dotnet452_Process.Start();
        }
    }
}
