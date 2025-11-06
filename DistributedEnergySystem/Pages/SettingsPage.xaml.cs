using System.Windows.Controls;
using System.Windows;  // 添加Windows命名空间以访问TextWrapping
using DistributedEnergySystem.Services;  // 添加Services命名空间引用
using DistributedEnergySystem.Pages.Settings;  // 添加设置模块命名空间

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
                case "InterfaceSettings":
                    ContentTitle.Text = "界面设置";
                    ContentDescription.Text = "配置系统显示和交互界面";
                    break;
                case "TimeSettings":
                    ContentTitle.Text = "时间设置";
                    ContentDescription.Text = "配置系统时间和时区参数";
                    break;
                case "AudioSettings":
                    ContentTitle.Text = "音频设置";
                    ContentDescription.Text = "配置系统音频和报警设置";
                    break;
                case "DisplaySettings":
                    ContentTitle.Text = "显示设置";
                    ContentDescription.Text = "配置屏幕背光和显示参数";
                    break;
                case "SystemSettings":
                    ContentTitle.Text = "系统设置";
                    ContentDescription.Text = "配置系统维护和性能参数";
                    break;
                case "SecuritySettings":
                    ContentTitle.Text = "安全设置";
                    ContentDescription.Text = "配置系统安全和访问控制";
                    break;
                case "ConnectionSettings":
                    ContentTitle.Text = "连接设置";
                    ContentDescription.Text = "配置网络和通信连接";
                    break;
                case "SystemInfo":
                    ContentTitle.Text = "系统信息";
                    ContentDescription.Text = "查看设备和系统信息";
                    break;
            }
        }

        /// <summary>
        /// 显示对应的设置内容
        /// </summary>
        private void ShowSettingsContent(string settingType)
        {
            // 根据选择加载对应的设置模块
            UserControl settingsControl = null;

            switch (settingType)
            {
                case "InterfaceSettings":
                    settingsControl = new InterfaceSettings();
                    break;
                case "TimeSettings":
                    settingsControl = new TimeSettings();
                    break;
                case "AudioSettings":
                    settingsControl = new AudioSettings();
                    break;
                case "DisplaySettings":
                    settingsControl = new DisplaySettings();
                    break;
                case "SystemSettings":
                    settingsControl = new SystemSettings();
                    break;
                case "SecuritySettings":
                    settingsControl = new SecuritySettings();
                    break;
                case "ConnectionSettings":
                    settingsControl = new ConnectionSettings();
                    break;
                case "SystemInfo":
                    settingsControl = new SystemInfo();
                    break;
                default:
                    // 默认显示提示信息
                    var defaultText = new TextBlock
                    {
                        Text = "请选择左侧菜单项查看详细设置内容。",
                        FontSize = 14,
                        Foreground = System.Windows.Media.Brushes.LightGray,
                        TextWrapping = TextWrapping.Wrap,
                        Margin = new System.Windows.Thickness(20),
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center
                    };

                    // 将TextBlock包装在UserControl中
                    settingsControl = new UserControl
                    {
                        Content = defaultText
                    };
                    break;
            }

            // 将设置控件加载到Frame中
            ContentFrame.Content = settingsControl;
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