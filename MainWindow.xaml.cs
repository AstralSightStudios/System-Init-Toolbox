using Microsoft.Win32;
using ModernWpf.Controls;
using System;
using System.Management;
using System.Windows;
using System.Diagnostics;

namespace System_Init_Toolbox
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow
	{
		//获取分区大小函数
		public static long GetHardDiskSpace(string str_HardDiskName)//string盘符变量
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
		//获取当前使用的网卡ID
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
			catch (Exception)
			{
				return "uMnIk";
			}

		}
		//函数与函数之间传递变量的class
		public static class mainwindow_class
		{
			public static string gpu_installed_display_drivers = "";
		}
		public async void check_updates()
		{
			try
			{
                //目前这玩意是0.2BETA
				string nowver = await Utilities.get("https://gitee.com/search__stars/uris_-system_-init_-toolbox/raw/master/now_version.txt");
				bool a = nowver.Contains("0.3BETA");
				if (!a)
				{
					ContentDialog dialog_update_tips = new ContentDialog
					{
						Title = "有新版本可用",
						Content = "System Init Toolbox现已推出新版本，请前往https://github.com/Stargazing-Studio/System-Init-Toolbox/releases下载最新版本并替换旧版本以获取最佳体验。",
						PrimaryButtonText = "前往下载页面",
						CloseButtonText = "暂不更新"
					};
					ContentDialogResult result = await dialog_update_tips.ShowAsync();
					if (result == ContentDialogResult.Primary)
					{
						System.Diagnostics.Process.Start("explorer.exe", "https://github.com/Stargazing-Studio/System-Init-Toolbox/releases");
					}
				}

			}
			catch (Exception ex)
			{
				ContentDialog dialog_update_error = new ContentDialog
				{
					Title = "检查更新时出现问题",
					Content = "System Init Toolbox在检查更新时出现问题。\n请检查您是否正确连接到网络。若无法联网，您可能无法使用此程序的在线版本，请使用其它方式下载此程序的离线版本。\n错误信息：\n" + ex,
					PrimaryButtonText = "OK",
				};
				ContentDialogResult result = await dialog_update_error.ShowAsync();
			}
		}
		public MainWindow()
		{
			InitializeComponent();
			check_updates();
			//获取系统信息并显示
			OperatingSystem os = Environment.OSVersion;
			Version ver = os.Version;
			//获取电脑名称，没啥用，废案
			string get_conputer_name = System.Environment.GetEnvironmentVariable("ComputerName");
			if (get_conputer_name != null)
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
			double capacity = 0;
			foreach (ManagementObject mo1 in moc1)
			{
				capacity += ((Math.Round(Int64.Parse(mo1.Properties["Capacity"].Value.ToString()) / 1024 / 1024 / 1024.0, 1)));
			}
			//获取显卡信息
			ManagementObjectSearcher objvide = new ManagementObjectSearcher("select * from Win32_VideoController");
			//初始化显卡信息全局变量
			string gpu_name = "";
			string gpu_device_id = "";
			string gpu_adapter_ram = "";
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
			Label_GPU_Name.Content = "图像输出显卡名称（GPU）：" + gpu_name;
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
			//开发选项页面 驱动下载链接get
			ContentDialog debug_output_gitee_link_content = new ContentDialog
			{
				Title = "Debug Message",
				Content = await Utilities.get("https://gitee.com/search__stars/uris_-system_-init_-toolbox/raw/master/nvidia-desktop-lastet.txt"),
				CloseButtonText = "OK"
			};
			ContentDialogResult result = await debug_output_gitee_link_content.ShowAsync();
		}

		private void Button_Click_3(object sender, RoutedEventArgs e)
		{
			//开发选项页面 驱动下载窗口test
			driver_download_window driver_download_window = new driver_download_window("tst", "test test test test test test test", "1145.14");
			driver_download_window.Show();
		}

		private async void Button_Click_4(object sender, RoutedEventArgs e)
		{
			//NVIDIA显卡驱动下载
			//屎山代码，谁能帮忙改改😭😭😭
			ContentDialog nv_combobox_null_tips = new ContentDialog
			{
				Title = "下拉框为空",
				Content = "请先在上面选择NVIDIA驱动平台与版本再点下载按钮哦~",
				CloseButtonText = "OK"
			};
			//debug_nv1.Content = nv_platform.SelectedIndex + "        " + nv_driver_version.SelectedIndex;
			if (nv_platform.Text == "")
			{
				ContentDialogResult result = await nv_combobox_null_tips.ShowAsync();
			}
			else if (nv_driver_version.Text == "")
			{
				ContentDialogResult result = await nv_combobox_null_tips.ShowAsync();
			}
			if (nv_platform.Text == "笔记本" && nv_driver_version.SelectedIndex == 1)
			{
				ContentDialog nv_notebook_gpu_error = new ContentDialog
				{
					Title = "显卡错误",
					Content = "虽然但是移动端好像没有卡在这个日期停更...建议换成2019.4.11批次的下载。",
					CloseButtonText = "OK"
				};
				ContentDialogResult result = await nv_notebook_gpu_error.ShowAsync();
			}
			try
			{
				if (nv_platform.Text == "笔记本" && nv_driver_version.SelectedIndex == 0)
				{
					driver_download_window driver_download_window = new driver_download_window(await Utilities.get("https://gitee.com/search__stars/uris_-system_-init_-toolbox/raw/master/nvidia-notebook-lastet.txt"), "NVIDIA笔记本显卡驱动", "最新版本");
					driver_download_window.Show();
				}
				if (nv_platform.Text == "笔记本" && nv_driver_version.SelectedIndex == 2)
				{
					driver_download_window driver_download_window = new driver_download_window(await Utilities.get("https://gitee.com/search__stars/uris_-system_-init_-toolbox/raw/master/nvidia-notebook-20190411.txt"), "NVIDIA笔记本显卡驱动", "2019.4.11 425.31");
					driver_download_window.Show();
				}
				if (nv_platform.Text == "笔记本" && nv_driver_version.SelectedIndex == 3)
				{
					driver_download_window driver_download_window = new driver_download_window(await Utilities.get("https://gitee.com/search__stars/uris_-system_-init_-toolbox/raw/master/nvidia-notebook-20180327.txt"), "NVIDIA笔记本显卡驱动", "2018.3.27 391.35");
					driver_download_window.Show();
				}
				if (nv_platform.Text == "笔记本" && nv_driver_version.SelectedIndex == 4)
				{
					driver_download_window driver_download_window = new driver_download_window(await Utilities.get("https://gitee.com/search__stars/uris_-system_-init_-toolbox/raw/master/nvidia-notebook-20161214.txt"), "NVIDIA笔记本显卡驱动", "2016.12.14 342.01");
					driver_download_window.Show();
				}
			}
			catch (Exception ex)
			{
				ContentDialog nv_notebook_gpu_error = new ContentDialog
				{
					Title = "Error",
					Content = "在获取驱动下载链接时出现错误。\n错误信息：\n" + ex + "\n发生这样的错误，极有可能是你宽带拉了，重连网络试试看？",
					CloseButtonText = "OK"
				};
				ContentDialogResult result = await nv_notebook_gpu_error.ShowAsync();
			}
			if (nv_driver_version.SelectedIndex == 5)
			{
				ContentDialog nv_notebook_old_gpu = new ContentDialog
				{
					Title = "远古显卡",
					Content = "对于过于远古的显卡，还请自行前往官网下载驱动。\n官网：https://www.nvidia.cn/geforce/drivers",
					CloseButtonText = "OK"
				};
				ContentDialogResult result = await nv_notebook_old_gpu.ShowAsync();
			}
			if (nv_platform.Text == "台式电脑" && nv_driver_version.SelectedIndex == 2)
			{
				ContentDialog nv_notebook_gpu_error = new ContentDialog
				{
					Title = "显卡错误",
					Content = "虽然但是桌面端好像没有卡在这个日期停更...建议换成2018.3.27批次的下载。",
					CloseButtonText = "OK"
				};
				ContentDialogResult result = await nv_notebook_gpu_error.ShowAsync();
			}
			if (nv_platform.Text == "台式电脑" && nv_driver_version.SelectedIndex == 0)
			{
				driver_download_window driver_download_window = new driver_download_window(await Utilities.get("https://gitee.com/search__stars/uris_-system_-init_-toolbox/raw/master/nvidia-desktop-lastet.txt"), "NVIDIA显卡驱动", "最新版本");
				driver_download_window.Show();
			}
			if (nv_platform.Text == "台式电脑" && nv_driver_version.SelectedIndex == 1)
			{
				driver_download_window driver_download_window = new driver_download_window(await Utilities.get("https://gitee.com/search__stars/uris_-system_-init_-toolbox/raw/master/nvidia-desktop-20210920.txt"), "NVIDIA显卡驱动", "2021.9.20 472.12");
				driver_download_window.Show();
			}
			if (nv_platform.Text == "台式电脑" && nv_driver_version.SelectedIndex == 3)
			{
				driver_download_window driver_download_window = new driver_download_window(await Utilities.get("https://gitee.com/search__stars/uris_-system_-init_-toolbox/raw/master/nvidia-desktop-20180327.txt"), "NVIDIA显卡驱动", "2018.3.27 391.35");
				driver_download_window.Show();
			}
			if (nv_platform.Text == "台式电脑" && nv_driver_version.SelectedIndex == 4)
			{
				driver_download_window driver_download_window = new driver_download_window(await Utilities.get("https://gitee.com/search__stars/uris_-system_-init_-toolbox/raw/master/nvidia-desktop-20161214.txt"), "NVIDIA显卡驱动", "2016.12.14 342.01");
				driver_download_window.Show();
			}
		}

		private async void Button_Click_5(object sender, RoutedEventArgs e)
		{
			//AMD驱动下载按钮 用不了
			//amd的驱动下载简单的多
			//但还是屎山代码😭😭😭
			//而且会卡死
			//初步诊断是amd官网防爬虫的问题
			//amd我焯你🐎🐎🐎🐎🐎🐎🐎🐎🐎🐎🐎🐎🐎🐎🐎🐎🐎🐎
			/*
            ContentDialog amd_tips = new ContentDialog
            {
                Title = "AMD驱动下载关闭提示",
                Content = "由于AMD驱动下载链接的问题，此程序目前无法下载AMD显卡驱动，此问题可能会在以后解决，所以目前还请自行移步至官网\"amd.com\"下载。\nSo Amd Fuck You",
                CloseButtonText = "OK"
            };
            ContentDialogResult result = await amd_tips.ShowAsync();
            */
			ContentDialog amd_combobox_null_tips = new ContentDialog
			{
				Title = "下拉框为空",
				Content = "请先在上面选择AMD驱动平台与版本再点下载按钮哦~",
				CloseButtonText = "OK"
			};
			if (amd_platform.Text == "")
			{
				ContentDialogResult result = await amd_combobox_null_tips.ShowAsync();
			}
			else if (amd_driver_version.Text == "")
			{
				ContentDialogResult result = await amd_combobox_null_tips.ShowAsync();
			}
			if (amd_platform.Text == "台式电脑/笔记本" && amd_driver_version.SelectedIndex == 0)
			{
				driver_download_window driver_download_window = new driver_download_window(await Utilities.get("https://gitee.com/search__stars/uris_-system_-init_-toolbox/raw/master/amd-lastet.txt"), "AMD显卡驱动", "最新版本");
				driver_download_window.Show();
			}
			if (amd_platform.Text == "台式电脑/笔记本" && amd_driver_version.SelectedIndex == 1)
			{
				string uri = await Utilities.get("https://gitee.com/search__stars/uris_-system_-init_-toolbox/raw/master/amd-20210621.txt");
				driver_download_window driver_download_window = new driver_download_window(uri, "AMD显卡驱动", "2021.6.21 21.5.2");
				driver_download_window.Show();
			}
			if (amd_platform.Text == "台式电脑/笔记本" && amd_driver_version.SelectedIndex == 2)
			{
				driver_download_window driver_download_window = new driver_download_window(await Utilities.get("https://gitee.com/search__stars/uris_-system_-init_-toolbox/raw/master/amd-20150729.txt"), "AMD显卡驱动", "2021.7.29 15.7.1");
				driver_download_window.Show();
			}
			if (nv_driver_version.SelectedIndex == 3)
			{
				ContentDialog amd_old_gpu = new ContentDialog
				{
					Title = "远古显卡",
					Content = "对于过于远古的显卡，还请自行前往官网下载驱动。\n官网：https://www.amd.com/zh-hans/support",
					CloseButtonText = "OK"
				};
				ContentDialogResult result = await amd_old_gpu.ShowAsync();
			}
		}

		private void Button_Click_6(object sender, RoutedEventArgs e)
		{
			//.NET Framework安装窗口
			net_framework net_framework = new net_framework();
			net_framework.Show();
		}

		private void Button_Click_7(object sender, RoutedEventArgs e)
		{
			//.NET安装窗口
			dotnet dotnet = new dotnet();
			dotnet.Show();
		}

		private void Button_Click_8(object sender, RoutedEventArgs e)
		{
			//VC++安装窗口
			visualCPP visualCPP = new visualCPP();
			visualCPP.Show();
		}

		private void Button_Click_9(object sender, RoutedEventArgs e)
		{
			//抓哇安装窗口
			java java = new java();
			java.Show();
		}

		private void Button_Click_10(object sender, RoutedEventArgs e)
		{
			//xml生成页面打开
			System.Diagnostics.Process.Start("explorer.exe", "https://config.office.com/deploymentsettings");
		}

		private async void Button_Click_11(object sender, RoutedEventArgs e)
		{
			//默认配置文件按钮dialog
			ContentDialog createFileDialog = new ContentDialog
			{
				Title = "确定要使用？",
				Content = "如果您确定，那么程序将会在程序目录下生成一个叫做ProPlus2021Volume.xml的文件，您在部署Office时选择此配置文件即可。\n\n此配置文件包含：\nOffice LTSC 专业增强版 2021 - 批量许可证   最新版本\nWord\nPowerPoint\nExcel\n主要语言：匹配操作系统\n次要语言：English（US）\n次要语言：日语\n自动KMS激活（密钥：FXYTK-NJJ8C-GB6DW-3DYQT-6F7TH）",
				PrimaryButtonText = "确定",
				CloseButtonText = "取消"
			};

			ContentDialogResult result = await createFileDialog.ShowAsync();

			if (result == ContentDialogResult.Primary)//如果用户点确定
			{
				System.IO.File.Copy("OfficeConfigs/ProPlus2021Volume.xml", "ProPlus2021Volume.xml", true);//true=覆盖已存在的同名文件,false则反之
			}
		}

		private async void Button_Click_12(object sender, RoutedEventArgs e)
		{
			if (SelectedXMLFileLabel.Content != "")
			{
				ContentDialog eulaDialog = new ContentDialog
				{
					Title = "EULA",
					Content = "请打开程序根目录的OfficeDeployTool文件夹中的EULA文件（用记事本等软件打开皆可）以阅读EULA再决定是否同意。",
					PrimaryButtonText = "同意",
					CloseButtonText = "不同意"
				};

				ContentDialogResult result = await eulaDialog.ShowAsync();

				if (result == ContentDialogResult.Primary)//如果用户点同意
				{
					/*
                    ContentDialog bugDialog = new ContentDialog
                    {
                        Title = "此功能不可用",
                        Content = "目前，此程序的Office部署功能存在很大的问题，因此已停止使用。如果您有信心帮助我们解决此问题，请随时给我们提交Pull Request或在相关主题的issue中提供一些信息。非常感谢！",
                        PrimaryButtonText = "OK",
                        CloseButtonText = "打开issue页面"
                    };

                    ContentDialogResult result_t = await bugDialog.ShowAsync();
                    if(result_t == ContentDialogResult.None)
                    {
                        System.Diagnostics.Process.Start("explorer.exe", "https://github.com/Stargazing-Studio/System-Init-Toolbox/issues/1");
                    */
					OfficeStatus status = new OfficeStatus(SelectedXMLFileLabel_.Content.ToString());
					status.Show();
				}
			}
			else
			{
				ContentDialog nullselect = new ContentDialog
				{
					Title = "您没有选择文件",
					Content = "您似乎没用选择文件哦~请选择有效的XML配置文件后再部署哦~",
					PrimaryButtonText = "OK",
				};
				ContentDialogResult result = await nullselect.ShowAsync();
			}
		}

		private async void Button_Click_13(object sender, RoutedEventArgs e)
		{
			System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
			openFileDialog.Filter = "compressed files (*.xml)|*.xml";
			if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				SelectedXMLFileLabel.Content = "您已选择：" + openFileDialog.FileName;
				SelectedXMLFileLabel_.Content = openFileDialog.FileName;
			}
			else
			{
				ContentDialog nullselect = new ContentDialog
				{
					Title = "您没有选择文件",
					Content = "您似乎没用选择文件哦~",
					PrimaryButtonText = "OK",
				};
				ContentDialogResult result = await nullselect.ShowAsync();
			}
		}

		private void Button_Click_14(object sender, RoutedEventArgs e)
		{
			webbrowser webbrowser = new webbrowser("https://gitee.com/search__stars/uris_-system_-init_-toolbox/raw/master/donate/sg_skm.jpg");
			webbrowser.Show();
		}

		private void Button_Click_15(object sender, RoutedEventArgs e)
		{
			webbrowser webbrowser = new webbrowser("https://www.paypal.com/paypalme/mcmacheng");
			webbrowser.Show();
		}

		private void Button_Click_16(object sender, RoutedEventArgs e)
		{
			app_download_window app_download_window = new app_download_window("https://media.st.dl.pinyuncloud.com/client/installer/SteamSetup.exe", "Steam", "最新版本");
			app_download_window.Show();
		}

		private void Button_Click_17(object sender, RoutedEventArgs e)
		{
			app_download_window app_download_window = new app_download_window("https://launcher-public-service-prod06.ol.epicgames.com/launcher/api/installer/download/EpicGamesLauncherInstaller.msi", "EpicGames", "最新版本");
			app_download_window.Show();
		}

		private void Button_Click_18(object sender, RoutedEventArgs e)
		{
			app_download_window app_download_window = new app_download_window("https://www.dm.origin.com/download", "Origin", "最新版本");
			app_download_window.Show();
		}

		private void Button_Click_19(object sender, RoutedEventArgs e)
		{
			app_download_window app_download_window = new app_download_window("https://ubi.li/4vxt9", "Ubisoft Connect （由于育碧服务器很拉所以下载可能会很慢）", "最新版本");
			app_download_window.Show();
		}

		private void Button_Click_20(object sender, RoutedEventArgs e)
		{
			app_download_window app_download_window = new app_download_window("https://download.mozilla.org/?product=firefox-latest&os=win64&lang=zh-CN", "Firefox（国际版）", "最新版本");
			app_download_window.Show();
		}

		private void Button_Click_21(object sender, RoutedEventArgs e)
		{
			app_download_window app_download_window = new app_download_window("https://go.microsoft.com/fwlink/?linkid=2108834&Channel=Stable&language=zh-cn", "Microsoft Edge（Chrome内核）", "最新版本");
			app_download_window.Show();
		}

		private void Button_Click_22(object sender, RoutedEventArgs e)
		{
			app_download_window app_download_window = new app_download_window("https://dl.google.com/tag/s/appguid%3D%7B8A69D345-D564-463C-AFF1-A69D9E530F96%7D%26iid%3D%7B7FCA3ED6-C600-7C1A-5A93-773CC9C481B5%7D%26lang%3Dzh-CN%26browser%3D4%26usagestats%3D1%26appname%3DGoogle%2520Chrome%26needsadmin%3Dprefers%26ap%3Dx64-stable-statsdef_1%26installdataindex%3Dempty/update2/installers/ChromeSetup.exe", "Google Chrome", "最新版本");
			app_download_window.Show();
		}

		private void Button_Click_23(object sender, RoutedEventArgs e)
		{
			System.Diagnostics.Process.Start("explorer.exe", "https://github.com/xiaoniu1234/StarVPN/releases");
		}

		private void Button_Click_24(object sender, RoutedEventArgs e)
		{
			System.Diagnostics.Process.Start("explorer.exe", "https://www.jiayouyabeijing.com/cn/");
		}

		private void Button_Click_25(object sender, RoutedEventArgs e)
		{
			app_download_window app_download_window = new app_download_window("https://aka.ms/vs/17/release/vs_community.exe", "Visual Studio Installer", "2022 Release 17.x");
			app_download_window.Show();
		}

		private void Button_Click_26(object sender, RoutedEventArgs e)
		{
			app_download_window app_download_window = new app_download_window("https://vscode.cdn.azure.cn/stable/899d46d82c4c95423fb7e10e68eba52050e30ba3/VSCodeUserSetup-x64-1.63.2.exe", "Visual Studio Code", "Stable Build x64 1.63.2");
			app_download_window.Show();
		}

		private void Button_Click_27(object sender, RoutedEventArgs e)
		{
			app_download_window app_download_window = new app_download_window("https://www.python.org/ftp/python/3.10.1/python-3.10.1-amd64.exe", "Python", "3.10.1");
			app_download_window.Show();
		}

		private void Button_Click_28(object sender, RoutedEventArgs e)
		{
			System.Diagnostics.Process.Start("explorer.exe", "https://www.embarcadero.com/cn/free-tools/dev-cpp");
		}

        private void Button_Click_29(object sender, RoutedEventArgs e)
        {
			app_download_window app_download_window = new app_download_window("https://dsadata.intel.com/installer", "英特尔® 驱动程序和支持助手", "2022 Release 17.x");
			app_download_window.Show();
		}

        private async void Button_Click_30(object sender, RoutedEventArgs e)
        {
			ContentDialog openvmpDialog = new ContentDialog
			{
				Title = "确定要开启？",
				Content = "如果您确定开启，可能就会导致除WSA外的安卓模拟器无法使用。经测试，mumu模拟器、雷电模拟器均会受到影响。并且Vmware等虚拟机也会有无法启动的现象出现，所以还请您慎重考虑。\n当您点击确定后，程序可能会未响应，这是正常现象，请不要关闭程序，耐心等待。开启成功后可能会有弹窗要求您重新启动，按提示操作即可。",
				PrimaryButtonText = "确定",
				CloseButtonText = "取消"
			};

			ContentDialogResult result = await openvmpDialog.ShowAsync();

			if (result == ContentDialogResult.Primary)//如果用户点确定
			{
				Process batvmp_Process = new Process();
				batvmp_Process.StartInfo.FileName = "./BATFiles/virtualmachineplatform-on.bat";
				batvmp_Process.StartInfo.CreateNoWindow = true;
				batvmp_Process.Start();
				batvmp_Process.WaitForExit();
			}
		}

        private async void Button_Click_31(object sender, RoutedEventArgs e)
		{
			ContentDialog installwsaDialog = new ContentDialog
			{
				Title = "提示",
				Content = "在我们解决一些技术问题前，此程序暂时不能为您安装WSA，您需要手动安装。\n请参考：https://blog.csdn.net/qq_43733123/article/details/121194702 \n当然，如果您有什么解决此问题的好方法，也请到此程序的Github Issues页面的相关主题下回复您的想法或解决方法。",
				PrimaryButtonText = "打开手动安装参考教程",
				SecondaryButtonText = "打开关于此问题的issue",
				CloseButtonText = "关闭此对话框"
			};

			ContentDialogResult result = await installwsaDialog.ShowAsync();

			if (result == ContentDialogResult.Primary)
			{
				System.Diagnostics.Process.Start("explorer.exe", "https://blog.csdn.net/qq_43733123/article/details/121194702");
			}
			if (result == ContentDialogResult.Secondary)
            {
				System.Diagnostics.Process.Start("explorer.exe", "https://github.com/Stargazing-Studio/System-Init-Toolbox/issues/7");
			}
			/*
			app_download_window app_download_window = new app_download_window("http://tlu.dl.delivery.mp.microsoft.com/filestreamingservice/files/43d50125-dccc-490b-8c78-3106e736940d?P1=1641246234&P2=404&P3=2&P4=ai6eyQfCfBA5uqsHucQ202z7MEf%2foUe2s0DfjKAGCJmmsqsPHn1de8PQq3D7Wa2hJoQzR%2bQHgPimHjjm6S2Rew%3d%3d", "Windows Subsystem For Android", "1.8.32828",true);
			app_download_window.Show();
			*/
		}

        private async void Button_Click_32(object sender, RoutedEventArgs e)
        {
			System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
			openFileDialog.Filter = "Android APK (*.apk)|*.apk";
			if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				stlabel.Content = "您已选择：" + openFileDialog.FileName;
				wsa_lj.Content = openFileDialog.FileName;
				wsa_install_bt.IsEnabled = true;
			}
			else
			{
				ContentDialog nullselect = new ContentDialog
				{
					Title = "您没有选择文件",
					Content = "您似乎没用选择文件哦~",
					PrimaryButtonText = "OK",
				};
				ContentDialogResult result = await nullselect.ShowAsync();
			}
		}

        private void wsa_install_bt_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}