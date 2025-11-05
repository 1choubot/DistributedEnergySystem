using System;
using System.Windows.Controls;

namespace DistributedEnergySystem.Views
{
    /// <summary>
    /// SettingsPage.xaml 的交互逻辑
    ///
    /// partial class: 部分类，允许将一个类的定义分散到多个文件中
    /// 在WPF中，XAML文件会自动生成一个部分类，后台代码文件是另一个部分
    /// 编译时会将它们合并成一个完整的类
    /// </summary>
    public partial class SettingsPage : Page
    {
        /// <summary>
        /// 构造函数：类初始化时调用的方法
        /// InitializeComponent(): WPF自动生成的方法，用于初始化XAML中定义的控件
        /// </summary>
        public SettingsPage()
        {
            InitializeComponent();
        }
    }
}