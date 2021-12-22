using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace System_Init_Toolbox
{
    /// <summary>
    /// driver_download_window.xaml 的交互逻辑
    /// </summary>
    public partial class driver_download_window
    {
        private string global_uri = "";
        public driver_download_window(string uri, string name = "Driver Name", string version = "Driver Version")
        {
            InitializeComponent();
            this.global_uri = uri;
            DriverNameLabel.Content = name;
            DriverVersionLabel.Content = version;
        }
    }
}
