using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace DistributedEnergySystem.Controls
{
    /// <summary>
    /// 简化的背景动画控件 - 使用几何图形创建能源流动效果
    /// </summary>
    public class EnergyFlowBackground : UserControl
    {
        public EnergyFlowBackground()
        {
            IsHitTestVisible = false;
            CreateBackground();
        }

        private void CreateBackground()
        {
            var grid = new Grid();
            Content = grid;

            // 创建多个能源流动圆圈
            for (int i = 0; i < 8; i++)
            {
                var ellipse = new Ellipse
                {
                    Width = 8,
                    Height = 8,
                    Fill = new SolidColorBrush(Color.FromArgb(80, 16, 185, 129)), // 半透明绿色
                    RenderTransform = new TranslateTransform()
                };

                // 创建路径动画
                var storyboard = new Storyboard();

                // X轴移动
                var animationX = new DoubleAnimation
                {
                    From = -50,
                    To = 1350,
                    Duration = TimeSpan.FromSeconds(15 + i * 2),
                    RepeatBehavior = RepeatBehavior.Forever,
                    BeginTime = TimeSpan.FromSeconds(i * 1.5)
                };

                Storyboard.SetTarget(animationX, ellipse);
                Storyboard.SetTargetProperty(animationX, new PropertyPath("(UIElement.RenderTransform).(TranslateTransform.X)"));
                storyboard.Children.Add(animationX);

                // Y轴正弦波动
                var animationY = new DoubleAnimation
                {
                    From = 100 + i * 80,
                    To = 100 + i * 80,
                    Duration = TimeSpan.FromSeconds(3),
                    RepeatBehavior = RepeatBehavior.Forever,
                    AutoReverse = true
                };

                Storyboard.SetTarget(animationY, ellipse);
                Storyboard.SetTargetProperty(animationY, new PropertyPath("(UIElement.RenderTransform).(TranslateTransform.Y)"));
                storyboard.Children.Add(animationY);

                // 透明度动画
                var opacityAnimation = new DoubleAnimation
                {
                    From = 0.2,
                    To = 0.8,
                    Duration = TimeSpan.FromSeconds(2 + i * 0.5),
                    RepeatBehavior = RepeatBehavior.Forever,
                    AutoReverse = true
                };

                Storyboard.SetTarget(opacityAnimation, ellipse);
                Storyboard.SetTargetProperty(opacityAnimation, new PropertyPath("Opacity"));
                storyboard.Children.Add(opacityAnimation);

                grid.Children.Add(ellipse);
                storyboard.Begin();
            }
        }
    }
}