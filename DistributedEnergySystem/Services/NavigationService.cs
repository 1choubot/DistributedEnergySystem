using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using DistributedEnergySystem.Pages;
using DistributedEnergySystem.Views;  // 添加Views命名空间引用

namespace DistributedEnergySystem.Services
{
    /// <summary>
    /// 页面导航服务
    /// </summary>
    public class NavigationService
    {
        private static NavigationService _instance;
        private Frame _mainFrame;  // 移除readonly修饰符，允许在Initialize方法中设置
        private readonly Dictionary<string, Type> _pages;
        private Stack<string> _navigationHistory;

        public static NavigationService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new NavigationService();
                return _instance;
            }
        }

        private NavigationService()
        {
            _pages = new Dictionary<string, Type>();
            _navigationHistory = new Stack<string>();
        }

        /// <summary>
        /// 初始化导航服务
        /// </summary>
        /// <param name="mainFrame">主容器Frame</param>
        public void Initialize(Frame mainFrame)
        {
            _mainFrame = mainFrame ?? throw new ArgumentNullException(nameof(mainFrame));

            // 注册所有页面
            RegisterPages();
        }

        /// <summary>
        /// 注册所有页面
        /// </summary>
        private void RegisterPages()
        {
            // 注册系统总览页面
            _pages["Home"] = typeof(HomePage);

            // 注册系统设置页面
            _pages["Settings"] = typeof(Pages.SettingsPage);  // 明确指定Pages命名空间

            // 注册数据分析页面
            _pages["DataAnalysis"] = typeof(DataAnalysisPage);

            // 注册运行记录页面
            _pages["RunRecords"] = typeof(RunRecordsPage);

            // 注册帮助中心页面
            _pages["Help"] = typeof(HelpPage);
        }

        /// <summary>
        /// 导航到指定页面
        /// </summary>
        /// <param name="pageKey">页面键</param>
        /// <param name="parameter">导航参数</param>
        public bool NavigateTo(string pageKey, object parameter = null)
        {
            if (!_pages.ContainsKey(pageKey))
            {
                System.Diagnostics.Debug.WriteLine($"页面 {pageKey} 未注册");
                return false;
            }

            try
            {
                var pageType = _pages[pageKey];
                var page = Activator.CreateInstance(pageType) as Page;

                if (page != null)
                {
                    // 记录导航历史
                    if (_mainFrame.Content != null)
                    {
                        _navigationHistory.Push(pageKey);
                    }

                    // 执行导航
                    _mainFrame.Navigate(page, parameter);
                    return true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"导航到页面 {pageKey} 失败: {ex.Message}");
            }

            return false;
        }

        /// <summary>
        /// 返回上一页
        /// </summary>
        public bool GoBack()
        {
            if (_navigationHistory.Count > 0 && _mainFrame.CanGoBack)
            {
                _mainFrame.GoBack();
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获取当前页面键
        /// </summary>
        public string GetCurrentPageKey()
        {
            // 这里可以根据需要实现更复杂的逻辑
            if (_mainFrame?.Content is Page page)
            {
                return page.GetType().Name.Replace("Page", "");
            }
            return string.Empty;
        }

        /// <summary>
        /// 清空导航历史
        /// </summary>
        public void ClearHistory()
        {
            _navigationHistory.Clear();
            // Frame的NavigationService.ClearHistory()方法不存在，移除此调用
        }
    }
}