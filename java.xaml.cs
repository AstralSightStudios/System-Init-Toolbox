using System.Windows;
using ModernWpf.Controls;

namespace System_Init_Toolbox
{
    /// <summary>
    /// java.xaml 的交互逻辑
    /// </summary>
    public partial class java
    {
        public java()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if(java8_checkbox.IsChecked == true)
            {

            }
            else if(jdk17_checkbox.IsChecked == true)
            {

            }
            else
            {
                ContentDialog null_tips = new ContentDialog
                {
                    Title = "请先选择要安装的Java",
                    Content = "请先选择要安装的Java再点击安装按钮以安装哦！",
                    CloseButtonText = "OK"
                };
                ContentDialogResult result = await null_tips.ShowAsync();
            }
        }
    }
}
