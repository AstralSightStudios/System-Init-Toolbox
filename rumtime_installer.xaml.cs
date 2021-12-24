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
    //之前本来想做net framework .net java vc++都分别独占一个xaml的 最后发现太麻烦了，还是直接弄一个然后调用好了
    //这就是会出现文件名是runtime_installer，但class却是net_framework的情况的原因。。。
    /// <summary>
    /// net_framework.xaml 的交互逻辑
    /// </summary>
    public partial class net_framework
    {
        public net_framework()
        {
            InitializeComponent();
        }
    }
}
