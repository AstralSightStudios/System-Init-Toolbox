using System.IO;
using ModernWpf.Controls;
using System.Windows.Controls;
using System;

namespace System_Init_Toolbox
{
    public static partial class Configer
    {
        public async static void real_firstrun(TextBox wsa_ip_textbox, TextBox wsa_port_textbox, Slider a_sdr, Slider r_sdr, Slider g_sdr, Slider b_sdr)
        {
            if (!Directory.Exists("./configs"))
            {
                ContentDialog firstrun_dialog = new ContentDialog
                {
                    Title = "欢迎",
                    Content = "这看起来是您第一次运行System Init Toolbox呢！本软件是开源软件，使用GNU General Public License v3.0协议，并且完全免费。" +
                    "若您是在任何商家或个人通过任何方式付费获取到本软件，请立即要求退款并喷几句然后拉黑，严重者建议直接开盒上门。\n" +
                    "我相信您可能对这个软件还不算太熟悉，建议你熟悉熟悉再使用~ 您可以参考程序的wiki（https://github.com/Stargazing-Studio/System-Init-Toolbox/wiki），也可以去各大网站寻找本软件的教程，总能找到您看得懂的。\n" +
                    "本软件仅支持Windows10（1507）及更高版本，Windows 7或8的用户或许也可以试试，但不能保证您的体验。对于xp和vista用户来说...什么？你居然打开了这个软件？我超，微软成慈善家了？！（\n" +
                    "最后，祝您使用愉快，也欢迎您对本软件的发展做出贡献（如提交issues，pull requests）哦！",
                    CloseButtonText = "OK"
                };
                ContentDialogResult result = await firstrun_dialog.ShowAsync();
                Directory.CreateDirectory("./configs");
                File.Create("configs/RESET_CONFIGS").Dispose();
                System.IO.StreamWriter reset_configs_write = new System.IO.StreamWriter("configs/RESET_CONFIGS", false);
                reset_configs_write.Write("如果您的配置文件损坏，可以尝试删除此文件再打开程序，或许能将其修复，但您将丢失所有的配置。");
                reset_configs_write.Close();
                File.Create("configs/wsa_ip.txt").Dispose();
                File.WriteAllText("configs/wsa_ip.txt", "127.0.0.1");
                File.Create("configs/wsa_port.txt").Dispose();
                File.WriteAllText("configs/wsa_port.txt", "58526");
                System.IO.StreamWriter bg_color_a_write = new System.IO.StreamWriter("configs/bg_color_a.txt", false);
                bg_color_a_write.Write("255");
                bg_color_a_write.Close();
                System.IO.StreamWriter bg_color_r_write = new System.IO.StreamWriter("configs/bg_color_r.txt", false);
                bg_color_r_write.Write("255");
                bg_color_r_write.Close();
                System.IO.StreamWriter bg_color_g_write = new System.IO.StreamWriter("configs/bg_color_g.txt", false);
                bg_color_g_write.Write("255");
                bg_color_g_write.Close();
                System.IO.StreamWriter bg_color_b_write = new System.IO.StreamWriter("configs/bg_color_b.txt", false);
                bg_color_b_write.Write("255");
                bg_color_b_write.Close();
                System.IO.StreamWriter bg_image_path_write = new System.IO.StreamWriter("configs/bg_image_path.txt", false);
                bg_image_path_write.Write("NULL");
                bg_image_path_write.Close();
                File.Create("configs/dotnet-quiet").Dispose();
                File.Create("configs/dotnet-clean_residue").Dispose();
                File.Create("configs/driver-clean_residue").Dispose();
                File.Create("configs/app-clean_residue").Dispose();
                File.Create("configs/java-clean_residue").Dispose();
                File.Create("configs/net_framework-quiet").Dispose();
                File.Create("configs/net_framework-clean_residue").Dispose();
                File.Create("configs/visual_cpp-clean_residue").Dispose();
                DateTime current2 = DateTime.Now;
                while (current2.AddMilliseconds(500) > DateTime.Now)
                {
                    System.Windows.Forms.Application.DoEvents();
                }
                try
                {
                    wsa_ip_textbox.Text = System.IO.File.ReadAllText("configs/wsa_ip.txt");
                    wsa_port_textbox.Text = System.IO.File.ReadAllText("configs/wsa_port.txt");
                    a_sdr.Value = (byte)int.Parse(System.IO.File.ReadAllText("configs/bg_color_a.txt"));
                    r_sdr.Value = (byte)int.Parse(System.IO.File.ReadAllText("configs/bg_color_r.txt"));
                    g_sdr.Value = (byte)int.Parse(System.IO.File.ReadAllText("configs/bg_color_g.txt"));
                    b_sdr.Value = (byte)int.Parse(System.IO.File.ReadAllText("configs/bg_color_b.txt"));
                }
                catch
                {
                    //什么也不做
                }
            }
        }
        public async static void whether_reset_configs()
        {
            //配置文件修复
            if (Directory.Exists("configs"))
            {
                if (!File.Exists("configs/RESET_CONFIGS"))
                {
                    try
                    {
                        File.Delete("configs/wsa_ip.txt");
                        File.Delete("configs/wsa_port.txt");
                        File.Delete("configs/dotnet-quiet");
                        File.Delete("configs/dotnet-clean_residue");
                        File.Delete("configs/driver-clean_residue");
                        File.Delete("configs/app-clean_residue");
                        File.Delete("configs/java-clean_residue");
                        File.Delete("configs/net_framework-quiet");
                        File.Delete("configs/net_framework-clean_residue");
                        File.Delete("configs/visual_cpp-clean_residue");
                        File.Delete("configs/bg_color_a.txt");
                        File.Delete("configs/bg_color_r.txt");
                        File.Delete("configs/bg_color_g.txt");
                        File.Delete("configs/bg_color_b.txt");
                        File.Delete("configs/bg_image_path.txt");
                        File.Create("configs/RESET_CONFIGS").Dispose();
                        System.IO.StreamWriter reset_configs_write = new System.IO.StreamWriter("configs/RESET_CONFIGS", false);
                        reset_configs_write.Write("如果您的配置文件损坏，可以尝试删除此文件再打开程序，或许能将其修复，但您将丢失所有的配置。");
                        reset_configs_write.Close();
                        File.Create("configs/wsa_ip.txt").Dispose();
                        System.IO.StreamWriter wsa_ip_write = new System.IO.StreamWriter("configs/wsa_ip.txt", false);
                        wsa_ip_write.Write("127.0.0.1");
                        wsa_ip_write.Close();
                        File.Create("configs/wsa_port.txt").Dispose();
                        System.IO.StreamWriter wsa_port_write = new System.IO.StreamWriter("configs/wsa_port.txt", false);
                        wsa_port_write.Write("58526");
                        wsa_port_write.Close();
                        System.IO.StreamWriter bg_color_a_write = new System.IO.StreamWriter("configs/bg_color_a.txt", false);
                        bg_color_a_write.Write("255");
                        bg_color_a_write.Close();
                        System.IO.StreamWriter bg_color_r_write = new System.IO.StreamWriter("configs/bg_color_r.txt", false);
                        bg_color_r_write.Write("255");
                        bg_color_r_write.Close();
                        System.IO.StreamWriter bg_color_g_write = new System.IO.StreamWriter("configs/bg_color_g.txt", false);
                        bg_color_g_write.Write("255");
                        bg_color_g_write.Close();
                        System.IO.StreamWriter bg_color_b_write = new System.IO.StreamWriter("configs/bg_color_b.txt", false);
                        bg_color_b_write.Write("255");
                        bg_color_b_write.Close();
                        System.IO.StreamWriter bg_image_path_write = new System.IO.StreamWriter("configs/bg_image_path.txt", false);
                        bg_image_path_write.Write("NULL");
                        bg_image_path_write.Close();
                        File.Create("configs/dotnet-quiet").Dispose();
                        File.Create("configs/dotnet-clean_residue").Dispose();
                        File.Create("configs/driver-clean_residue").Dispose();
                        File.Create("configs/java-clean_residue").Dispose();
                        File.Create("configs/net_framework-quiet").Dispose();
                        File.Create("configs/net_framework-clean_residue").Dispose();
                        File.Create("configs/visual_cpp-clean_residue").Dispose();
                        File.Create("configs/app-clean_residue").Dispose();
                    }
                    catch
                    {
                        //即使报错也什么都不做，继续执行
                    }
                    System.Windows.MessageBox.Show("配置文件重置完毕。\n程序将自动关闭，请您手动重新打开此程序。", "操作执行完毕");
                    //完全退出此程序
                    System.Diagnostics.Process.GetCurrentProcess().Kill();
                }
            }
        }
    }
}
