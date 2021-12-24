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
        private static Stream wb;
        public net_framework()
        {
            InitializeComponent();
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
        public static bool DownloadFile(string URL, FileStream fs, System.Windows.Controls.ProgressBar progressBar1, System.Windows.Controls.Label label1, driver_download_window driver_download_window)
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

        }
    }
}
