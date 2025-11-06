using System.Windows.Controls;
using System.Windows;  // 添加Windows命名空间以访问TextWrapping
using DistributedEnergySystem.Services;  // 添加Services命名空间引用

namespace DistributedEnergySystem.Pages
{
    /// <summary>
    /// SettingsPage.xaml 的交互逻辑
    /// </summary>
    public partial class SettingsPage : BasePage
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 返回按钮点击事件
        /// </summary>
        private void BackButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            // 使用导航服务返回主页
            Services.NavigationService.Instance.NavigateTo("Home");
        }

        /// <summary>
        /// 设置菜单选择改变事件
        /// </summary>
        private void SettingsMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ListBox listBox && listBox.SelectedItem is ListBoxItem selectedItem)
            {
                // 更新内容标题
                UpdateContentTitle(selectedItem.Tag?.ToString());

                // 根据选择显示不同的设置内容
                ShowSettingsContent(selectedItem.Tag?.ToString());
            }
        }

        /// <summary>
        /// 更新内容标题和描述
        /// </summary>
        private void UpdateContentTitle(string settingType)
        {
            switch (settingType)
            {
                case "DeviceConfig":
                    ContentTitle.Text = "设备配置";
                    ContentDescription.Text = "配置和管理系统设备参数";
                    break;
                case "NetworkSettings":
                    ContentTitle.Text = "网络设置";
                    ContentDescription.Text = "配置网络连接和通信参数";
                    break;
                case "UserManagement":
                    ContentTitle.Text = "用户管理";
                    ContentDescription.Text = "管理系统用户权限和账户";
                    break;
                case "SystemParams":
                    ContentTitle.Text = "系统参数";
                    ContentDescription.Text = "调整系统运行参数和阈值";
                    break;
                case "DataBackup":
                    ContentTitle.Text = "数据备份";
                    ContentDescription.Text = "配置数据备份和恢复策略";
                    break;
                case "LogManagement":
                    ContentTitle.Text = "日志管理";
                    ContentDescription.Text = "查看和管理系统运行日志";
                    break;
            }
        }

        /// <summary>
        /// 显示对应的设置内容
        /// </summary>
        private void ShowSettingsContent(string settingType)
        {
            // 这里可以导航到不同的子页面或者在Frame中加载不同的内容
            // 暂时显示简单的文本内容
            var contentText = GetContentText(settingType);
            ContentFrame.Content = new TextBlock
            {
                Text = contentText,
                FontSize = 14,
                Foreground = System.Windows.Media.Brushes.LightGray,
                TextWrapping = TextWrapping.Wrap,
                Margin = new System.Windows.Thickness(20)
            };
        }

        /// <summary>
        /// 获取设置内容文本
        /// </summary>
        private string GetContentText(string settingType)
        {
            switch (settingType)
            {
                case "DeviceConfig":
                    return @"设备配置功能包括：
• 风力发电机组参数配置
• 光伏发电系统设置
• 储能系统参数管理
• 发电机组配置
• 逆变器参数设置
• 设备通信协议配置";

                case "NetworkSettings":
                    return @"网络设置功能包括：
• 以太网配置
• WiFi连接设置
• 蓝牙设备管理
• Modbus通信配置
• 云平台连接设置
• 网络安全配置";

                case "UserManagement":
                    return @"用户管理功能包括：
• 用户账户管理
• 角色权限分配
• 登录密码设置
• 操作日志查看
• 用户活动监控
• 安全策略配置";

                case "SystemParams":
                    return @"系统参数功能包括：
• 运行参数阈值设置
• 报警参数配置
• 数据采集间隔设置
• 系统时间同步
• 语言和时区设置
• 系统性能优化";

                case "DataBackup":
                    return @"数据备份功能包括：
• 自动备份策略设置
• 备份存储位置配置
• 数据恢复操作
• 备份数据查看
• 备份计划管理
• 数据完整性检查";

                case "LogManagement":
                    return @"日志管理功能包括：
• 系统运行日志查看
• 错误日志分析
• 操作日志记录
• 日志导出功能
• 日志搜索和过滤
• 日志存储配置";

                default:
                    return "请选择左侧菜单项查看详细设置内容。";
            }
        }

        /// <summary>
        /// 页面激活时调用
        /// </summary>
        protected override void OnPageActivated()
        {
            base.OnPageActivated();

            // 默认选中第一个菜单项
            if (SettingsMenu.Items.Count > 0)
            {
                SettingsMenu.SelectedIndex = 0;
            }
        }
    }
}