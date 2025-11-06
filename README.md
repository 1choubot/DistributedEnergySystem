# 分布式能源管理系统

[![.NET](https://img.shields.io/badge/.NET-8.0-purple.svg)](https://dotnet.microsoft.com/)
[![WPF](https://img.shields.io/badge/WPF-C%23-blue.svg)](https://github.com/dotnet/wpf)
[![License](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)

一个基于WPF + C# (.NET 8)的现代化分布式能源管理系统，专为工业级10英寸触摸屏设备设计。

## ✨ 特性

- 🎨 **现代化UI设计** - 玻璃态效果与专业工业风格
- 📱 **触摸优化** - 完美适配工业触摸屏操作
- 🔄 **实时监控** - 风力、光伏、储能、发电机组等设备实时数据
- 📊 **数据可视化** - 基于LiveCharts的动态图表展示
- 🧭 **智能导航** - 单例服务驱动的页面导航系统
- ⚡ **高性能** - 优化的数据绑定和UI渲染

## 🏗️ 系统架构

### 核心组件
- **MainWindow** - 主窗口与导航栏
- **NavigationService** - 页面导航服务
- **EnergyModels** - 能源设备数据模型
- **CustomControls** - 自定义控件库

### 支持的能源设备
- 💨 风力发电机组
- ☀️ 光伏发电系统
- 🔋 储能系统
- ⚡ 发电机组
- 🔄 逆变器系统

## 🚀 快速开始

### 系统要求
- Windows 7.0 或更高版本
- .NET 8.0 Desktop Runtime
- 10英寸触摸屏（推荐）

### 安装与运行
```bash
# 克隆仓库
git clone https://github.com/yourusername/distributed-energy-system.git
cd distributed-energy-system

# 运行项目
cd DistributedEnergySystem
dotnet run
```

## 📸 屏幕截图

![主界面](screenshots/main-interface.png)
*现代化的玻璃态设计界面*

![数据监控](screenshots/data-monitoring.png)
*实时能源数据监控*

## 🎯 主要功能

### 系统总览
- 所有设备状态一览
- 实时功率数据
- 系统运行统计

### 数据分析
- 历史数据趋势图
- 能效分析报告
- 设备性能对比

### 系统设置
- 设备参数配置
- 网络连接设置
- 用户权限管理

### 运行记录
- 操作日志记录
- 故障历史查询
- 数据导出功能

## 🛠️ 技术栈

| 技术 | 版本 | 描述 |
|------|------|------|
| .NET | 8.0 | 应用框架 |
| WPF | - | UI框架 |
| C# | 12.0 | 编程语言 |
| LiveCharts | 0.9.7 | 图表库 |
| MVVM | - | 设计模式 |

## 📁 项目结构

```
DistributedEnergySystem/
├── Pages/                    # 页面文件
│   ├── HomePage.xaml        # 系统总览
│   ├── SettingsPage.xaml   # 系统设置
│   ├── DataAnalysisPage.xaml # 数据分析
│   ├── RunRecordsPage.xaml # 运行记录
│   └── HelpPage.xaml       # 帮助中心
├── Services/                # 服务层
│   └── NavigationService.cs # 导航服务
├── ViewModels/              # 视图模型
│   └── MainWindowViewModel.cs
├── Models/                  # 数据模型
│   └── EnergyModels.cs
├── Views/                   # 自定义视图
│   └── CustomStatusBar.xaml
└── Controls/                # 自定义控件
    └── EnergyParticleCanvas.cs
```

## 🔧 开发指南

### 添加新页面
1. 在`Pages/`目录创建新页面
2. 继承`BasePage`基类
3. 在`NavigationService`中注册页面
4. 添加导航按钮

### 数据绑定示例
```xml
<TextBlock Text="{Binding CurrentTime}" />
<ProgressBar Value="{Binding BatteryLevel}" />
```

### 自定义样式
```xml
<Style x:Key="CustomButton" TargetType="Button">
    <Setter Property="Background" Value="#10B981"/>
    <Setter Property="Foreground" Value="White"/>
</Style>
```

## 🐛 已知问题

- [ ] 部分动画效果在低性能设备上可能卡顿
- [ ] 长时间运行可能出现内存泄漏（正在修复）

## 🤝 贡献指南

欢迎提交Issue和Pull Request！

1. Fork 本仓库
2. 创建特性分支 (`git checkout -b feature/AmazingFeature`)
3. 提交更改 (`git commit -m 'Add some AmazingFeature'`)
4. 推送到分支 (`git push origin feature/AmazingFeature`)
5. 打开 Pull Request

## 📝 更新日志

### v1.0.0 (2025-11-06)
- ✨ 实现基础UI框架
- 🎨 完成导航栏美化
- 🐛 修复按钮垂直居中问题
- 📱 优化触摸屏体验

## 📄 许可证

本项目采用 MIT 许可证 - 查看 [LICENSE](LICENSE) 文件了解详情

## 👥 作者

- **Claude AI Assistant** - *初始开发* - [GitHub](https://github.com/anthropics)

## 🙏 致谢

- [Microsoft WPF团队](https://github.com/dotnet/wpf)
- [LiveCharts贡献者](https://github.com/Live-Charts/Live-Charts)
- [MVVM Community Toolkit](https://github.com/CommunityToolkit/dotnet)

---

⭐ 如果这个项目对你有帮助，请给它一个星标！