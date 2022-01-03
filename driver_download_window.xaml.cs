using ModernWpf.Controls;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows;

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
		System.Windows.Forms.SaveFileDialog dialog;
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
				Environment.Exit(0);
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
				WebHeaderCollection wb_nb = new WebHeaderCollection();
				// +referer
				wb_nb.Add(HttpRequestHeader.Referer, "https://www.amd.com/");
				WebRequest aas = WebRequest.Create(http_file_url);
				aas.Headers = wb_nb;
				response = aas.GetResponse();
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
				WebHeaderCollection wb_nb = new WebHeaderCollection();
				// +referer
				wb_nb.Add(HttpRequestHeader.Referer, "https://www.amd.com/");
				driver_download_window.request = (HttpWebRequest)WebRequest.Create(URL);
				driver_download_window.request.Headers = wb_nb;
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
				MessageBox.Show(exception.ToString());
				return false;
			}
		}

		public async void Button_Click(object sender, RoutedEventArgs e)
		{
			downbutton.IsEnabled = false;
			downloading = true;
			dialog = new System.Windows.Forms.SaveFileDialog();//选择文件夹
			dialog.FileName = "Graphics Driver.exe";
            // c o m p r e s s e d files
			dialog.Filter = "executable files (*.exe)|*.exe";
			FileStream fs;
			//bool b = DownloadFile(global_uri, fs, progressBar1, label1, this);
			if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				//dbg_label.Content = dialog.FileName;
				if ((fs = (FileStream)dialog.OpenFile()) != null)
				{
					if (HttpFileExist(global_uri))
					{
						bool b = DownloadFile(global_uri, fs, progressBar1, label1, this);
						if (b)
						{
							run_setup_bt.IsEnabled = true;
							downloading = false;
						}
						else
						{
							run_setup_bt.IsEnabled = false;
						}
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
		public void run_setup_func()
		{
			dbg_label.Content = "等待显卡驱动安装程序结束......";
			Process process = new Process();//创建进程对象    
			ProcessStartInfo startInfo = new ProcessStartInfo(dialog.FileName); // 括号里是(程序名,参数)
			process.StartInfo = startInfo;
			process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
			process.StartInfo.CreateNoWindow = true;
			process.Start();
			process.WaitForExit();
			dbg_label.Content = "安装程序结束，安装完毕！";
			if (ToggleSwitch_AfterInstallRemove_exe.IsOn)
			{
				dialog.OpenFile();
			}
		}
		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			run_setup_func();
		}
	}
}
