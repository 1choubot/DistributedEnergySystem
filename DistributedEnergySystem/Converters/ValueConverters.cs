using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace DistributedEnergySystem.Converters
{
    /// <summary>
    /// 将布尔值转换为颜色的转换器
    /// </summary>
    public class BoolToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return boolValue ? new SolidColorBrush(Color.FromRgb(76, 175, 80)) : new SolidColorBrush(Color.FromRgb(158, 158, 158));
            }
            return new SolidColorBrush(Color.FromRgb(158, 158, 158));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 根据SOC值返回不同颜色的转换器
    /// </summary>
    public class SocToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is float soc)
            {
                if (soc > 60)
                    return new SolidColorBrush(Color.FromRgb(76, 175, 80));  // 绿色
                else if (soc > 30)
                    return new SolidColorBrush(Color.FromRgb(255, 193, 7)); // 黄色
                else
                    return new SolidColorBrush(Color.FromRgb(244, 67, 54)); // 红色
            }
            return new SolidColorBrush(Color.FromRgb(158, 158, 158));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}