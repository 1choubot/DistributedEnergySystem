using System.Windows.Controls;

namespace DistributedEnergySystem.Pages
{
    /// <summary>
    /// HelpPage.xaml 的交互逻辑
    /// </summary>
    public partial class HelpPage : BasePage
    {
        public HelpPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 帮助菜单选择改变事件（预留，暂不实现具体逻辑）
        /// </summary>
        private void HelpMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // 预留：后期可根据选中的菜单项切换不同的帮助内容
            // 当前只显示快速入门内容
        }
    }
}