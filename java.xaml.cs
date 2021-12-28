using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows;
using ModernWpf.Controls;

namespace System_Init_Toolbox
{
    /// <summary>
    /// java.xaml 的交互逻辑
    /// </summary>
    public partial class java
    {
        private bool downloading = false;
        private Stream wb;
        WebResponse response;
        WebRequest request;
        public java()
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
        public static bool DownloadFile(string URL, FileStream fs, System.Windows.Controls.ProgressBar progressBar1, System.Windows.Controls.Label label1, java java)
        {
            try
            {
                WebHeaderCollection wb_nb = new WebHeaderCollection();
                wb_nb.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.110 Safari/537.36 Edg/96.0.1054.62");
                java.request = (HttpWebRequest)WebRequest.Create(URL);
                java.request.Headers = wb_nb;
                java.response = (HttpWebResponse)java.request.GetResponse();
                long totalLength = java.response.ContentLength;
                progressBar1.Maximum = (int)totalLength;
                java.wb = java.response.GetResponseStream();
                //把stream wb改为全局变量 方便进行.close操作
                long currentLength = 0;
                byte[] by = new byte[1024];
                int osize = java.wb.Read(by, 0, by.Length);
                while (osize > 0)
                {
                    DispatcherHelper.DoEvents();
                    currentLength = osize + currentLength;
                    fs.Write(by, 0, osize);
                    progressBar1.Value = (int)currentLength;
                    label1.Content = string.Format("{0} / {1}", BytesToString(currentLength), BytesToString(totalLength));
                    osize = java.wb.Read(by, 0, by.Length);
                }
                fs.Close();
                java.wb.Close();
                return (currentLength == totalLength);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
                return false;
            }
        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if(java8_checkbox.IsChecked == true)
            {
                downloading = true;
                status_label.Content = "正在下载Java 8";
                FileStream j8 = new FileStream("java8.exe", FileMode.OpenOrCreate);
                DownloadFile("https://javadl.oracle.com/webapps/download/AutoDL?BundleId=245479_4d5417147a92418ea8b615e228bb6935",j8,progressBar1,label1,this);
                status_label.Content = "正在安装Java 8";
                Delay(3000);
                progressBar1.IsIndeterminate = true;
                Process j8_Process = new Process();
                ProcessStartInfo startInfo_8 = new ProcessStartInfo("./java8.exe");
                j8_Process.StartInfo = startInfo_8;
                j8_Process.StartInfo.CreateNoWindow = true;
                j8_Process.Start();
                j8_Process.WaitForExit();
                downloading = false;
            }
            if(jdk17_checkbox.IsChecked == true)
            {
                downloading = true;
                status_label.Content = "正在下载JDK 17";
                FileStream j17 = new FileStream("jdk17.exe", FileMode.OpenOrCreate);
                DownloadFile("https://download.oracle.com/java/17/latest/jdk-17_windows-x64_bin.exe", j17, progressBar1, label1, this);
                status_label.Content = "正在安装JDK 17";
                Delay(3000);
                progressBar1.IsIndeterminate = true;
                Process j17_Process = new Process();
                ProcessStartInfo startInfo_17 = new ProcessStartInfo("./jdk17.exe");
                j17_Process.StartInfo = startInfo_17;
                j17_Process.StartInfo.CreateNoWindow = true;
                j17_Process.Start();
                j17_Process.WaitForExit();
                downloading = false;
            }
            if(java8_checkbox.IsChecked == false || jdk17_checkbox.IsChecked == false)
            {
                ContentDialog null_tips = new ContentDialog
                {
                    Title = "请先选择要安装的Java",
                    Content = "请先选择要安装的Java再点击安装按钮以安装哦！",
                    CloseButtonText = "OK"
                };
                ContentDialogResult result = await null_tips.ShowAsync();
            }
            progressBar1.Value = 100;
            progressBar1.IsIndeterminate = false;
            status_label.Content = "全部安装完成";
        }
    }
}
