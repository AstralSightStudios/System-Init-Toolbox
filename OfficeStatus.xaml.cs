using System;
using System.Diagnostics;

namespace System_Init_Toolbox
{
    /// <summary>
    /// OfficeStatus.xaml 的交互逻辑
    /// </summary>
    public partial class OfficeStatus
    {
        private string XmlFileName;
        public static void Delay(int mm)
        {
            DateTime current = DateTime.Now;
            while (current.AddMilliseconds(mm) > DateTime.Now)
            {
                System.Windows.Forms.Application.DoEvents();
            }
            return;
        }
        public OfficeStatus(string XmlFileName)
        {
            InitializeComponent();
            this.XmlFileName = XmlFileName;
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            tipslabel.Opacity = 100;
            tipslabel_2.Opacity = 100;
            tipslabel_3.Opacity = 100;
            dpy_button.IsEnabled = false;
            pb.IsIndeterminate = true;
            Process down_process = new Process();
            ProcessStartInfo down_start_info = new ProcessStartInfo("./OfficeDeployTool/setup.exe", "/download " + "\"" + XmlFileName + "\"");
            down_process.StartInfo = down_start_info;
            down_process.StartInfo.CreateNoWindow = true;
            StatusLabel.Content = "正在下载Office...";
            Delay(3000);
            down_process.Start();
            down_process.WaitForExit();
            Process install_process = new Process();
            ProcessStartInfo install_start_info = new ProcessStartInfo("./OfficeDeployTool/setup.exe", "/configure " + "\"" + XmlFileName + "\"");
            StatusLabel.Content = "正在安装Office...";
            Delay(3000);
            install_process.StartInfo = install_start_info;
            install_process.StartInfo.CreateNoWindow = true;
            install_process.Start();
            install_process.WaitForExit();
            StatusLabel.Content = "Office安装完毕！";
            pb.IsIndeterminate = false;
            pb.Value = 100;
        }
    }
}
