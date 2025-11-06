using System;
using System.Windows.Controls;
using DistributedEnergySystem.Pages;  // 引入Pages命名空间

namespace DistributedEnergySystem.Views
{
    /// <summary>
    /// HomePage.xaml 的交互逻辑
    /// 主页包含系统总览的所有内容
    /// </summary>
    public partial class HomePage : BasePage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 页面激活时调用
        /// </summary>
        protected override void OnPageActivated()
        {
            base.OnPageActivated();

            // 启动数据更新定时器
            StartDataUpdate();
        }

        /// <summary>
        /// 页面失活时调用
        /// </summary>
        protected override void OnPageDeactivated()
        {
            base.OnPageDeactivated();

            // 停止数据更新定时器
            StopDataUpdate();
        }

        /// <summary>
        /// 启动数据更新
        /// </summary>
        private void StartDataUpdate()
        {
            // 这里可以添加数据更新逻辑
            // 例如启动定时器更新系统参数
        }

        /// <summary>
        /// 停止数据更新
        /// </summary>
        private void StopDataUpdate()
        {
            // 这里可以添加停止数据更新的逻辑
            // 例如停止定时器
        }
    }
}