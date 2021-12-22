using System;
using System.IO;
using System.Net;
using System.Windows;
using ModernWpf.Controls;

namespace System_Init_Toolbox
{
    /// <summary>
    /// driver_download_window.xaml 的交互逻辑
    /// </summary>
    public partial class driver_download_window
    {
        private bool downloading = false;
        private HttpWebRequest request;
        private HttpWebResponse response;
        private Stream wb;
        private string global_uri = "";
        public driver_download_window(string uri, string name = "Driver Name", string version = "Driver Version")
        {
            InitializeComponent();
            this.global_uri = uri;
            DriverNameLabel.Content = name;
            DriverVersionLabel.Content = version;
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
        public bool HttpFileExist(string http_file_url)
        {
            WebResponse response = null;
            bool result = false;//下载结果
            try
            {
                response = WebRequest.Create(http_file_url).GetResponse();
                result = response == null ? false : true;
            }
            catch (Exception)
            {
                result = false;
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }
            return result;
        }
        public static bool DownloadFile(string URL, FileStream fs, System.Windows.Controls.ProgressBar progressBar1, System.Windows.Controls.Label label1, driver_download_window driver_download_window)
        {
            try
            {
                driver_download_window.request = (HttpWebRequest)WebRequest.Create(URL);
                driver_download_window.response = (HttpWebResponse)driver_download_window.request.GetResponse();
                long totalLength = driver_download_window.response.ContentLength;
                progressBar1.Maximum = (int)totalLength;
                driver_download_window.wb = driver_download_window.response.GetResponseStream();
                //把stream wb改为全局变量 方便进行.close操作
                long currentLength = 0;
                byte[] by = new byte[1024];
                int osize = driver_download_window.wb.Read(by, 0, by.Length);
                while (osize > 0)
                {
                    DispatcherHelper.DoEvents();
                    currentLength = osize + currentLength;
                    fs.Write(by, 0, osize);
                    progressBar1.Value = (int)currentLength;
                    label1.Content = string.Format("{0} / {1}", BytesToString(currentLength), BytesToString(totalLength));
                    osize = driver_download_window.wb.Read(by, 0, by.Length);
                }
                fs.Close();
                driver_download_window.wb.Close();
                return (currentLength == totalLength);
            }
            catch (Exception exception)
            {
                // wb速来测试bug
                MessageBox.Show(exception.ToString());
                return false;
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            downbutton.IsEnabled = false;
            downloading = true;
            System.Windows.Forms.SaveFileDialog dialog = new System.Windows.Forms.SaveFileDialog();//选择文件夹
            dialog.FileName = "Graphics Driver.exe";
            dialog.Filter = "compressed files (*.exe)|*.exe";
            FileStream fs;
            ContentDialog dbg_filestream = new ContentDialog
            {
                Title = "OK",
                Content = "FileStream fs已执行",
                CloseButtonText = "OK"
            };
            ContentDialogResult result = await dbg_filestream.ShowAsync();
            //bool b = DownloadFile(global_uri, fs, progressBar1, label1, this);
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ContentDialog dbg_dialog = new ContentDialog
                {
                    Title = "OK",
                    Content = "if判断成功    dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK",
                    CloseButtonText = "OK"
                };
                ContentDialogResult result_two = await dbg_dialog.ShowAsync();
                if ((fs = (FileStream)dialog.OpenFile()) != null)
                {
                    ContentDialog dbg_dialog_no_null = new ContentDialog
                    {
                        Title = "OK",
                        Content = "if判断成功    fs = (FileStream)dialog.OpenFile()) != null\n顺带dialog一下global uri：\n" + global_uri,
                        CloseButtonText = "OK"
                    };
                    ContentDialogResult result_three = await dbg_dialog_no_null.ShowAsync();
                    if (HttpFileExist(global_uri))
                    {
                        ContentDialog StartDownloadTips = new ContentDialog
                        {
                            Title = "Start",
                            Content = "HttpFileExist验证通过，开始下载咯！",
                            CloseButtonText = "OK"
                        };
                        ContentDialogResult result_four = await StartDownloadTips.ShowAsync();
                        bool b = DownloadFile(global_uri, fs, progressBar1, label1, this);
                    }
                    else
                    {
                        ContentDialog online_file_not_found = new ContentDialog
                        {
                            Title = "服务器文件不存在",
                            Content = "服务器不存在驱动文件。难道...AMD和NVIDIA跑路了？！",
                            CloseButtonText = "OK"
                        };
                        ContentDialogResult result_five = await online_file_not_found.ShowAsync();
                    }
                }
            }
        }
    }
}
