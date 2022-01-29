# System-Init-Toolbox
【System Init Toolbox · 系统初始化工具箱】一个能让你在安装系统后快速安装运行库等必备程序的软件<br/>
## 注意：这是master分支，也叫dev分支，其内容为实时开发提交，无法保证功能可用性，仅能保证可正常编译运行。若要修改程序请在已发布版本源码的基础上修改，尽可能避免在此分支的基础上修改。<br/>
# 下载、安装与运行
首先，欢迎使用System Init Toolbox。


本程序使用[WPF](https://baike.baidu.com/item/WPF/5299594)（[.NET](https://dotnet.microsoft.com/zh-cn/) Desktop 6.0）开发，并使用Visual Studio 2022作为编辑器。

如果您需要使用本程序，我们推荐您通过Github Release的方式获取本程序（详见下方：从Github Release下载安装程序）

如果您想通过编译的方式运行或修改此程序，详见下方：如何编译并运行此程序

# 从Github Release下载安装程序
这是我们最推荐您使用的方法，且是最稳定最简单最保险的方法。 您只需要点一下此项目页面右边的Releases，或者直接干脆 **[点这里](https://github.com/Stargazing-Studio/System-Init-Toolbox/releases)** 就能直达下载页面，获取程序所有版本的下载链接。

下载速度很慢？**[这个网站](https://d.serctl.com)** 或许能帮你。

下载完成后，双击运行，按照提示安装即可，非常简单、快速。

# 如何编译并运行此程序
上面说过，此程序使用Visual Studio 2022作为编辑器。所以如果要通过编译的方式运行此程序，您先需要下载并安装一个**[Visual Studio](https://visualstudio.microsoft.com/zh-hans/)，并确保已经勾选了“.NET桌面开发”以及“Windows 11 22000.0 SDK”这两个功能选项。

安装成功后，使用**[Github Desktop](https://desktop.github.com/)来clone此仓库至您的电脑。

当然，我们只是推荐您使用Github Desktop。如果您还是只想使用原版Git，可以通过执行以下命令来clone此仓库：

`git clone "https://github.com/Stargazing-Studio/System-Init-Toolbox.git"`

clone完毕后，双击System Init Toolbox.sln应该就可以打开此项目了。

如果您需要修改程序，直接照着您平时开发WPF程序的样子去修改就好了。

需要运行的话，Visual Studio主窗口的上方应该会有一个绿色运行按钮，点击一下即可编译并运行。编译出的程序一般存放在bin文件夹内，您可将其拷贝到其他的电脑上运行，或在遵守本程序的开源协议的前提下，作为自己的程序去发布。当然，这种编译方式编译出来的程序是不带框架的，用户运行时都必须安装.NET 6.0 Desktop Runtime。如果想要编译出带框架的程序...~不对啊不对啊，我怎么开始教你怎么用Visual Studio啦，这种情况难道不应该让你自己去百度吗？哼哼啊啊啊啊啊啊啊啊啊啊啊啊啊~

# 反馈此程序的bug或为此项目做贡献
若您要反馈此程序的Bug，前往该项目的Issues页，新建一个属于你自己的Issue，填入你发现的Bug，然后发布即可。反馈Bug之前，请检查你的程序是否已更新到最新版本，如果不是的话，试试更新。万一更完bug就不在了呢？

为此项目做贡献的方法主要为：

1.为此项目提出Pull Request

2.给开发者提供一些好的想法，包括有哪些可以加入的新功能

3.为这个项目捐助（在程序内有捐助入口）

# 我遇到问题了，需要帮助！
如果你有任何问题，也可以发在Issues里，不影响的。但是在提问之前，还请先了解： **[提问的智慧](https://github.com/ryanhanwu/How-To-Ask-Questions-The-Smart-Way/blob/main/README-zh_CN.md)**
