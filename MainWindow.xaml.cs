using System;
using System.Windows;
using Microsoft.Win32;
using System.Management;
using ModernWpf.Controls;
using System.Net;

namespace System_Init_Toolbox
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        //获取分区大小函数
        public static long GetHardDiskSpace(string str_HardDiskName)
        {
            
            long totalSize = new long();
            str_HardDiskName = str_HardDiskName + ":\\";
            System.IO.DriveInfo[] drives = System.IO.DriveInfo.GetDrives();
            foreach (System.IO.DriveInfo drive in drives)
            {
                if (drive.Name == str_HardDiskName)
                {
                    totalSize = drive.TotalSize / (1024 * 1024 * 1024);
                }
            }
            return totalSize;
        }
        //获取当前使用的网卡MAC地址函数
        public static string GetNetworkAdpaterID()
        {
            try
            {
                string mac = "";
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                    if ((bool)mo["IPEnabled"] == true)
                    {
                        mac += mo["MacAddress"].ToString();
                        break;
                    }
                string[] macs = mac.Split(':');
                mac = "";
                foreach (string m in macs)
                {
                    mac += m;
                }
                moc = null;
                mc = null;
                return mac.Trim();
            }
            catch (Exception e)
            {
                return "uMnIk";
            }

        }
        //函数与函数之间传递变量的class
        public static class mainwindow_class
        {
            public static string gpu_installed_display_drivers="";
        }
        public MainWindow()
        {
            InitializeComponent();
            //获取系统信息并显示
            OperatingSystem os = Environment.OSVersion;
            Version ver = os.Version;
            //获取电脑名称，没啥用，废案
            string get_conputer_name = System.Environment.GetEnvironmentVariable("ComputerName");
            if(get_conputer_name != null)
            {
                //Label_Computer_Name.Content = "电脑名称：" + get_conputer_name;
            }
            else
            {
                //Label_Computer_Name.Content = "电脑名称：无法读取";
            }
            //获取内存大小
            ManagementClass cimobject1 = new ManagementClass("Win32_PhysicalMemory");
            ManagementObjectCollection moc1 = cimobject1.GetInstances();
            double capacity=0;
            foreach (ManagementObject mo1 in moc1)
            {
                capacity += ((Math.Round(Int64.Parse(mo1.Properties["Capacity"].Value.ToString()) / 1024 / 1024 / 1024.0, 1)));
            }
            //获取显卡信息
            ManagementObjectSearcher objvide = new ManagementObjectSearcher("select * from Win32_VideoController");
            //初始化显卡信息全局变量
            string gpu_name="";
            string gpu_device_id="";
            string gpu_adapter_ram="";
            string gpu_driver_version = "";
            string gpu_video_processor = "";
            string gpu_video_memory_type = "";
            foreach (ManagementObject obj in objvide.Get())
            {
                gpu_name = "" + obj["Name"];
                gpu_device_id = "" + obj["DeviceID"];
                gpu_adapter_ram = "" + obj["AdapterRAM"];
                mainwindow_class.gpu_installed_display_drivers = "" + obj["InstalledDisplayDrivers"];
                gpu_driver_version = "" + obj["DriverVersion"];
                gpu_video_processor = "" + obj["VideoProcessor"];
                gpu_video_memory_type = "" + obj["VideoMemoryType"];
            }
            //读取注册表并显示系统/硬件信息（这波啊是把注册表内容当亲爹了属于是
            Label_System_Version.Content = "系统名称：" + Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion").GetValue("ProductName");
            Label_System_BuildLabEx.Content = "系统信息：" + Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion").GetValue("BuildLabEx");
            Label_System_Build.Content = "系统Build号：" + Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion").GetValue("CurrentBuild");
            Label_System_Edition_ID.Content = "版本ID：" + Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion").GetValue("EditionID");
            Label_System_Product_ID.Content = "产品ID：" + Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion").GetValue("ProductID");
            Label_System_Root.Content = "系统根目录：" + Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion").GetValue("SystemRoot");
            Label_System_Machine_ID.Content = "设备ID：" + Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\SQMClient").GetValue("MachineId");
            Label_Hardware_C_Size.Content = "C盘大小：" + GetHardDiskSpace("C") + "GB";
            Label_Hardware_Drive_letter_number.Content = "分区数量（占用盘符数）：" + System.IO.Directory.GetLogicalDrives().Length + "个";
            Label_Hardware_CPU.Content = "中央处理器（CPU）：" + Registry.LocalMachine.OpenSubKey(@"HARDWARE\DESCRIPTION\System\CentralProcessor\0").GetValue("ProcessorNameString");
            Label_Hardware_CPU_Identifier.Content = "中央处理器标识符（CPU Identifier）：" + Registry.LocalMachine.OpenSubKey(@"HARDWARE\DESCRIPTION\System\CentralProcessor\0").GetValue("Identifier");
            Label_Hardware_RAM.Content = "总内存容量（RAM Size）：" + capacity.ToString() + "GB";
            Label_Hardware_Graphics_card.Content = "图像输出显卡（Graphics Card）：" + gpu_name;
            Label_Hardware_NetworkAdpater_ID.Content = "当前使用的网卡的ID：" + GetNetworkAdpaterID();
            Label_GPU_Name.Content  = "图像输出显卡名称（GPU）：" + gpu_name;
            Label_GPU_Device_ID.Content = "图像输出显卡设备ID（GPU Device ID）：" + gpu_device_id;
            double GB_GPU_AP_RAM = double.Parse(gpu_adapter_ram) / (1024 * 1024 * 1024);
            Label_GPU_AP_RAM.Content = "图像输出显卡共享内存（GPU AP RAM）：" + GB_GPU_AP_RAM.ToString() + "GB";
            Label_GPU_driver_version.Content = "图像输出显卡驱动版本（GPU Driver Version）：" + gpu_driver_version;
            Label_GPU_video_processor.Content = "图像输出显卡核心（GPU Core）：" + gpu_video_processor;
            Label_GPU_video_memory_type.Content = "图像输出显卡显存（GPU Video Memory type）" + gpu_video_memory_type + "GB";
            Label_Hardware_BaseBoard_Manufacturer.Content = "主板制造商：" + Registry.LocalMachine.OpenSubKey(@"HARDWARE\DESCRIPTION\System\BIOS").GetValue("BaseBoardManufacturer");
            Label_Hardware_BaseBoard_Product.Content = "主板型号：" + Registry.LocalMachine.OpenSubKey(@"HARDWARE\DESCRIPTION\System\BIOS").GetValue("BaseBoardProduct");
            Label_Hardware_BaseBoard_Version.Content = "主板版本：" + Registry.LocalMachine.OpenSubKey(@"HARDWARE\DESCRIPTION\System\BIOS").GetValue("BaseBoardVersion");
            Label_Hardware_BIOS_Vendor.Content = "BIOS提供商：" + Registry.LocalMachine.OpenSubKey(@"HARDWARE\DESCRIPTION\System\BIOS").GetValue("BIOSVendor");
            Label_Hardware_BIOS_ReleaseDate.Content = "BIOS发布日期：" + Registry.LocalMachine.OpenSubKey(@"HARDWARE\DESCRIPTION\System\BIOS").GetValue("BIOSReleaseDate");
            Label_Hardware_BIOS_Version.Content = "BIOS版本：" + Registry.LocalMachine.OpenSubKey(@"HARDWARE\DESCRIPTION\System\BIOS").GetValue("BIOSVersion");
            Label_Hardware_BIOS_Manufacturer.Content = "BIOS制造商：" + Registry.LocalMachine.OpenSubKey(@"HARDWARE\DESCRIPTION\System\BIOS").GetValue("SystemManufacturer");
        }

        //输出变量gpu_installed_display_drivers
        public async void Button_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog gpu_installed_display_drivers_dialog = new ContentDialog
            {
                Title = "Installed Display Drivers",
                Content = mainwindow_class.gpu_installed_display_drivers,
                CloseButtonText = "OK"
            };

            ContentDialogResult result = await gpu_installed_display_drivers_dialog.ShowAsync();
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //这个下载方式是个废案，按钮留下来占个位置，这个函数不用看了，没意义
            double version = 497.29;
            ContentDialog debug_nvidia_gpu_driver_download_link_dialog = new ContentDialog
            {
                Title = "Debug Message",
                Content = "https://cn.download.nvidia.com/Windows/" + version + "/" + version + "-notebook-win10-win11-64bit-international-dch-whql.exe",
                CloseButtonText = "OK"
            };
            ContentDialogResult result = await debug_nvidia_gpu_driver_download_link_dialog.ShowAsync();
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ContentDialog debug_output_gitee_link_content = new ContentDialog
            {
                Title = "Debug Message",
                Content = await Utilities.get("https://gitee.com/search__stars/uris_-system_-init_-toolbox/raw/master/nvidia-desktop-lastet.txt"),
                CloseButtonText = "OK"
            };
            ContentDialogResult result = await debug_output_gitee_link_content.ShowAsync();
        }
    }
    }