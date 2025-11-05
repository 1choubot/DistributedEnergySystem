using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace DistributedEnergySystem.Controls
{
    /// <summary>
    /// 能源粒子动画控件 - 创建漂浮的能源粒子效果
    /// </summary>
    public class EnergyParticleCanvas : Canvas
    {
        private readonly List<Ellipse> _particles = new List<Ellipse>();
        private readonly Random _random = new Random();
        private readonly DispatcherTimer _animationTimer;

        public EnergyParticleCanvas()
        {
            IsHitTestVisible = false; // 不拦截鼠标事件
            _animationTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(50) // 20 FPS
            };
            _animationTimer.Tick += AnimationTimer_Tick;

            Loaded += EnergyParticleCanvas_Loaded;
            SizeChanged += EnergyParticleCanvas_SizeChanged;
            Unloaded += EnergyParticleCanvas_Unloaded;
        }

        private void EnergyParticleCanvas_Loaded(object sender, RoutedEventArgs e)
        {
            CreateParticles();
            _animationTimer.Start();
        }

        private void EnergyParticleCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Width > 0 && e.NewSize.Height > 0)
            {
                UpdateParticlesBounds();
            }
        }

        /// <summary>
        /// 创建粒子
        /// </summary>
        private void CreateParticles()
        {
            // 清除现有粒子
            foreach (var particle in _particles)
            {
                Children.Remove(particle);
            }
            _particles.Clear();

            // 根据画布大小创建合适数量的粒子
            var particleCount = Math.Max(15, (int)(ActualWidth * ActualHeight / 15000));

            for (int i = 0; i < particleCount; i++)
            {
                var particle = CreateParticle();
                _particles.Add(particle);
                Children.Add(particle);
            }
        }

        /// <summary>
        /// 创建单个粒子
        /// </summary>
        private Ellipse CreateParticle()
        {
            var particle = new Ellipse
            {
                Width = _random.Next(2, 6),
                Height = _random.Next(2, 6),
                Opacity = _random.NextDouble() * 0.3 + 0.1 // 0.1-0.4 透明度
            };

            // 设置粒子颜色（能源主题色）
            var colors = new[]
            {
                Color.FromRgb(16, 185, 129),   // 绿色 #10B981
                Color.FromRgb(59, 130, 246),   // 蓝色 #3B82F6
                Color.FromRgb(245, 158, 11),   // 橙色 #F59E0B
                Color.FromRgb(139, 92, 246),   // 紫色 #8B5CF6
                Color.FromRgb(249, 115, 22)    // 橙红 #F97316
            };

            particle.Fill = new SolidColorBrush(colors[_random.Next(colors.Length)]);

            // 添加发光效果
            particle.Effect = new DropShadowEffect
            {
                Color = ((SolidColorBrush)particle.Fill).Color,
                BlurRadius = _random.Next(5, 15),
                ShadowDepth = 0,
                Opacity = _random.NextDouble() * 0.5 + 0.3
            };

            // 初始位置
            Canvas.SetLeft(particle, _random.NextDouble() * ActualWidth);
            Canvas.SetTop(particle, _random.NextDouble() * ActualHeight);

            // 创建随机运动参数
            var moveX = (_random.NextDouble() - 0.5) * 2; // -1 到 1
            var moveY = (_random.NextDouble() - 0.5) * 2; // -1 到 1
            var duration = _random.NextDouble() * 10 + 5; // 5-15秒

            // 创建动画
            var storyboard = new Storyboard();

            // X轴移动动画
            var doubleAnimationX = new DoubleAnimation
            {
                From = Canvas.GetLeft(particle),
                To = Canvas.GetLeft(particle) + moveX * 100,
                Duration = TimeSpan.FromSeconds(duration),
                RepeatBehavior = RepeatBehavior.Forever,
                AutoReverse = true
            };
            Storyboard.SetTarget(doubleAnimationX, particle);
            Storyboard.SetTargetProperty(doubleAnimationX, new PropertyPath("(Canvas.Left)"));
            storyboard.Children.Add(doubleAnimationX);

            // Y轴移动动画
            var doubleAnimationY = new DoubleAnimation
            {
                From = Canvas.GetTop(particle),
                To = Canvas.GetTop(particle) + moveY * 100,
                Duration = TimeSpan.FromSeconds(duration),
                RepeatBehavior = RepeatBehavior.Forever,
                AutoReverse = true
            };
            Storyboard.SetTarget(doubleAnimationY, particle);
            Storyboard.SetTargetProperty(doubleAnimationY, new PropertyPath("(Canvas.Top)"));
            storyboard.Children.Add(doubleAnimationY);

            // 透明度脉冲动画
            var doubleAnimationOpacity = new DoubleAnimation
            {
                From = particle.Opacity,
                To = particle.Opacity * 0.3,
                Duration = TimeSpan.FromSeconds(_random.NextDouble() * 3 + 2),
                RepeatBehavior = RepeatBehavior.Forever,
                AutoReverse = true
            };
            Storyboard.SetTarget(doubleAnimationOpacity, particle);
            Storyboard.SetTargetProperty(doubleAnimationOpacity, new PropertyPath("Opacity"));
            storyboard.Children.Add(doubleAnimationOpacity);

            // 缓放动画
            var scaleTransform = new ScaleTransform(1.0, 1.0);
            particle.RenderTransform = scaleTransform;
            particle.RenderTransformOrigin = new Point(0.5, 0.5);

            var doubleAnimationScale = new DoubleAnimation
            {
                From = 1.0,
                To = _random.NextDouble() * 0.5 + 0.8, // 0.8-1.3
                Duration = TimeSpan.FromSeconds(_random.NextDouble() * 4 + 3),
                RepeatBehavior = RepeatBehavior.Forever,
                AutoReverse = true
            };
            Storyboard.SetTarget(doubleAnimationScale, scaleTransform);
            Storyboard.SetTargetProperty(doubleAnimationScale, new PropertyPath("ScaleX"));
            storyboard.Children.Add(doubleAnimationScale);

            var doubleAnimationScaleY = new DoubleAnimation
            {
                From = 1.0,
                To = _random.NextDouble() * 0.5 + 0.8,
                Duration = TimeSpan.FromSeconds(_random.NextDouble() * 4 + 3),
                RepeatBehavior = RepeatBehavior.Forever,
                AutoReverse = true
            };
            Storyboard.SetTarget(doubleAnimationScaleY, scaleTransform);
            Storyboard.SetTargetProperty(doubleAnimationScaleY, new PropertyPath("ScaleY"));
            storyboard.Children.Add(doubleAnimationScaleY);

            // 启动动画
            storyboard.Begin();

            // 存储动画以便后续控制
            particle.Tag = storyboard;

            return particle;
        }

        /// <summary>
        /// 更新粒子边界（当画布大小改变时）
        /// </summary>
        private void UpdateParticlesBounds()
        {
            foreach (var particle in _particles)
            {
                var currentLeft = Canvas.GetLeft(particle);
                var currentTop = Canvas.GetTop(particle);

                // 确保粒子在新的边界内
                if (currentLeft > ActualWidth) Canvas.SetLeft(particle, ActualWidth - particle.Width);
                if (currentTop > ActualHeight) Canvas.SetTop(particle, ActualHeight - particle.Height);
            }
        }

        /// <summary>
        /// 动画定时器事件 - 定期刷新粒子状态
        /// </summary>
        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            // 随机改变一些粒子的亮度
            foreach (var particle in _particles)
            {
                if (_random.NextDouble() < 0.05) // 5%概率改变亮度
                {
                    var currentOpacity = particle.Opacity;
                    var targetOpacity = _random.NextDouble() * 0.3 + 0.1;

                    var opacityAnimation = new DoubleAnimation
                    {
                        From = currentOpacity,
                        To = targetOpacity,
                        Duration = TimeSpan.FromSeconds(_random.NextDouble() * 2 + 1),
                        AutoReverse = true
                    };

                    particle.BeginAnimation(Ellipse.OpacityProperty, opacityAnimation);
                }
            }
        }

        /// <summary>
        /// 清理资源
        /// </summary>
        private void EnergyParticleCanvas_Unloaded(object sender, RoutedEventArgs e)
        {
            _animationTimer?.Stop();

            foreach (var particle in _particles)
            {
                if (particle.Tag is Storyboard storyboard)
                {
                    storyboard.Stop();
                }
            }
        }
    }
}