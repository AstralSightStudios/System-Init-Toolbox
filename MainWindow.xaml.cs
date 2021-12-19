using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace System_Init_Toolbox
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //获取系统信息并显示
            OperatingSystem os = Environment.OSVersion;
            Version ver = os.Version;
            string get_conputer_name = System.Environment.GetEnvironmentVariable("ComputerName");
            if(get_conputer_name != null)
            {
                //Label_Computer_Name.Content = "电脑名称：" + get_conputer_name;
            }
            else
            {
                //Label_Computer_Name.Content = "电脑名称：无法读取";
            }
            Label_System_Version.Content = "系统名称：" + Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion").GetValue("ProductName");
            Label_System_BuildLabEx.Content = "系统信息：" + Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion").GetValue("BuildLabEx");
            Label_System_Build.Content = "系统Build号：" + Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion").GetValue("CurrentBuild");
            Label_System_Edition_ID.Content = "版本ID：" + Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion").GetValue("EditionID");
            Label_System_Product_ID.Content = "产品ID：" + Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion").GetValue("ProductID");
            Label_System_DigitalProduct_Id.Content = "系统根目录：" + Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion").GetValue("SystemRoot");
        }
    }
}
