using System.Windows.Controls;

namespace DistributedEnergySystem.Pages
{
    /// <summary>
    /// 页面基类，提供通用功能
    /// </summary>
    public class BasePage : Page
    {
        public BasePage()
        {
            Loaded += BasePage_Loaded;
            Unloaded += BasePage_Unloaded;
        }

        private void BasePage_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            OnPageActivated();
        }

        private void BasePage_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            OnPageDeactivated();
        }

        /// <summary>
        /// 页面激活时调用，子类可重写
        /// </summary>
        protected virtual void OnPageActivated()
        {
            // 子类可重写此方法
        }

        /// <summary>
        /// 页面失活时调用，子类可重写
        /// </summary>
        protected virtual void OnPageDeactivated()
        {
            // 子类可重写此方法
        }
    }
}