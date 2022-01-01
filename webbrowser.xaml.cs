using System;
using System.Windows;

namespace System_Init_Toolbox
{
    /// <summary>
    /// webbrowser.xaml 的交互逻辑
    /// </summary>
    public partial class webbrowser : Window
    {
        public webbrowser(string webbrowser_openuri)
        {
            InitializeComponent();
            webview_control.Source = new Uri(webbrowser_openuri);
        }
    }
}
