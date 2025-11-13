using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Linq;
using DistributedEnergySystem.Views;  // 引入Views命名空间
using DistributedEnergySystem.Services;  // 引入Services命名空间
using DistributedEnergySystem.Pages;  // 引入Pages命名空间

namespace DistributedEnergySystem
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// 这个窗口作为主容器，负责页面导航
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// 构造函数：窗口初始化时调用
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            // 初始化导航服务
            InitializeNavigation();
        }

        /// <summary>
        /// 初始化导航服务
        /// </summary>
        private void InitializeNavigation()
        {
            // 初始化导航服务并传入主Frame
            Services.NavigationService.Instance.Initialize(MainContentFrame);

            // 设置初始Tag以确保主页按钮处于激活状态
            this.Tag = "Home";

            // 导航到主页
            Services.NavigationService.Instance.NavigateTo("Home");
        }

        /// <summary>
        /// 窗口加载完成事件
        /// </summary>
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // 可以在这里添加窗口加载完成后的初始化逻辑
        }

        /// <summary>
        /// 窗口关闭事件
        /// </summary>
        protected override void OnClosed(EventArgs e)
        {
            // 清理资源
            Services.NavigationService.Instance.ClearHistory();
            base.OnClosed(e);
        }

        /// <summary>
        /// 底部导航按钮点击事件
        /// </summary>
        private void NavButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is string pageKey)
            {
                // 更新Window的Tag以切换活动状态
                this.Tag = pageKey;

                // 查找所有导航按钮并重置样式
                var navButtons = new Button[] { HomeButton, DataAnalysisButton, SettingsButton, RunRecordsButton, HelpButton };

                foreach (var navButton in navButtons)
                {
                    if (navButton != null)
                    {
                        navButton.Style = (Style)FindResource("NavButtonStyle");
                    }
                }

                // 为当前按钮设置活动样式
                button.Style = (Style)FindResource("ActiveNavButtonStyle");

                // 导航到目标页面
                Services.NavigationService.Instance.NavigateTo(pageKey);
            }
        }

        /// <summary>
        /// 状态栏图标点击事件 - 报警声音
        /// </summary>
        private void AlarmSoundButton_Click(object sender, RoutedEventArgs e)
        {
            // 跳转到设置页面的音频设置
            NavigateToSettings("AudioSettings");
        }

        /// <summary>
        /// 状态栏图标点击事件 - 系统通知
        /// </summary>
        private void NotificationButton_Click(object sender, RoutedEventArgs e)
        {
            // 跳转到运行记录页面
            NavigateToPage("RunRecords");
        }

        /// <summary>
        /// 状态栏图标点击事件 - 网络连接
        /// </summary>
        private void NetworkButton_Click(object sender, RoutedEventArgs e)
        {
            // 跳转到设置页面的连接设置
            NavigateToSettings("ConnectionSettings");
        }

        /// <summary>
        /// 状态栏图标点击事件 - 蓝牙设置
        /// </summary>
        private void BluetoothButton_Click(object sender, RoutedEventArgs e)
        {
            // 跳转到设置页面的连接设置
            NavigateToSettings("ConnectionSettings");
        }

        /// <summary>
        /// 状态栏图标点击事件 - 4G连接
        /// </summary>
        private void FourGButton_Click(object sender, RoutedEventArgs e)
        {
            // 跳转到设置页面的连接设置
            NavigateToSettings("ConnectionSettings");
        }

        /// <summary>
        /// 导航到设置页面并选中指定子模块
        /// </summary>
        private void NavigateToSettings(string settingType)
        {
            // 更新导航按钮状态
            UpdateNavigationButtonState("Settings");

            // 导航到设置页面并传递参数
            Services.NavigationService.Instance.NavigateTo("Settings", settingType);
        }

        /// <summary>
        /// 导航到指定页面
        /// </summary>
        private void NavigateToPage(string pageKey)
        {
            // 更新导航按钮状态
            UpdateNavigationButtonState(pageKey);

            // 导航到目标页面
            Services.NavigationService.Instance.NavigateTo(pageKey);
        }

        /// <summary>
        /// 更新导航按钮状态
        /// </summary>
        private void UpdateNavigationButtonState(string activePageKey)
        {
            // 更新Window的Tag
            this.Tag = activePageKey;

            // 查找所有导航按钮并重置样式
            var navButtons = new Button[] { HomeButton, DataAnalysisButton, SettingsButton, RunRecordsButton, HelpButton };

            foreach (var navButton in navButtons)
            {
                if (navButton != null)
                {
                    if (navButton.Tag?.ToString() == activePageKey)
                    {
                        navButton.Style = (Style)FindResource("ActiveNavButtonStyle");
                    }
                    else
                    {
                        navButton.Style = (Style)FindResource("NavButtonStyle");
                    }
                }
            }
        }
    }
}