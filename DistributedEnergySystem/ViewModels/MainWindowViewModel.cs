using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DistributedEnergySystem.Models;

namespace DistributedEnergySystem.ViewModels
{
    /// <summary>
    /// 主窗口的ViewModel
    /// </summary>
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private string _currentTime;

        // 能源参数实例
        private WindPowerModel _windPower;
        private SolarPowerModel _solarPower;
        private BatteryModel _battery;
        private GeneratorModel _generator;
        private ACOutputModel _acOutput;

        /// <summary>
        /// 当前时间（UI绑定的属性）
        /// </summary>
        public string CurrentTime
        {
            get => _currentTime;
            set
            {
                _currentTime = value;
                OnPropertyChanged();
            }
        }

        // 能源参数属性
        public WindPowerModel WindPower => _windPower;
        public SolarPowerModel SolarPower => _solarPower;
        public BatteryModel Battery => _battery;
        public GeneratorModel Generator => _generator;
        public ACOutputModel ACOutput => _acOutput;

        // 系统参数
        private string _workMode = "自动模式";
        private float _systemTemperature = 35f;
        private string _faultCode = "无故障";

        public string WorkMode
        {
            get => _workMode;
            set { _workMode = value; OnPropertyChanged(); }
        }

        public float SystemTemperature
        {
            get => _systemTemperature;
            set { _systemTemperature = value; OnPropertyChanged(); OnPropertyChanged(nameof(TemperatureDisplay)); }
        }

        public string FaultCode
        {
            get => _faultCode;
            set { _faultCode = value; OnPropertyChanged(); }
        }

        public string TemperatureDisplay => $"{SystemTemperature:F1}°C";

        /// <summary>
        /// 构造函数 - 初始化所有数据和定时器
        /// </summary>
        public MainWindowViewModel()
        {
            // 初始化能源模型
            _windPower = new WindPowerModel();
            _solarPower = new SolarPowerModel();
            _battery = new BatteryModel();
            _generator = new GeneratorModel();
            _acOutput = new ACOutputModel();

            // 初始化模拟数据
            InitializeMockData();

            // 立即更新一次时间
            UpdateTime();

            // 创建定时器，每秒更新一次
            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();

            // 创建数据更新定时器（模拟实时数据变化）
            var dataTimer = new System.Windows.Threading.DispatcherTimer();
            dataTimer.Interval = TimeSpan.FromSeconds(2);  // 每2秒更新一次数据
            dataTimer.Tick += DataTimer_Tick;
            dataTimer.Start();
        }

        /// <summary>
        /// 初始化模拟数据
        /// </summary>
        private void InitializeMockData()
        {
            // 风电参数
            _windPower.Voltage = 380f;
            _windPower.Current = 15.8f;
            _windPower.Power = 6.0f;
            _windPower.PowerPercentage = 75f;
            _windPower.IsConnected = true;
            _windPower.IsGenerating = true;

            // 光伏参数
            _solarPower.Voltage = 400f;
            _solarPower.Current = 12.5f;
            _solarPower.Power = 5.0f;
            _solarPower.PowerPercentage = 62.5f;
            _solarPower.IsConnected = true;
            _solarPower.IsGenerating = true;

            // 电池参数
            _battery.Voltage = 48.2f;
            _battery.Current = -20.5f;  // 负值表示充电
            _battery.Power = -1.0f;     // 负值表示充电
            _battery.Soc = 85.0f;
            _battery.Temperature = 32.5f;
            _battery.IsCharging = true;
            _battery.IsDischarging = false;
            _battery.HasWarning = false;

            // 发电机参数
            _generator.Voltage = 380f;
            _generator.Current = 0f;
            _generator.Power = 0f;
            _generator.PowerPercentage = 0f;
            _generator.IsRunning = false;

            // AC输出参数
            _acOutput.Voltage = 220f;
            _acOutput.Current = 25.3f;
            _acOutput.Power = 5.5f;
            _acOutput.PowerPercentage = 68.75f;
            _acOutput.IsOutputting = true;
            _acOutput.IsInputting = false;
        }

        /// <summary>
        /// 定时器事件 - 更新时间
        /// </summary>
        private void Timer_Tick(object sender, EventArgs e)
        {
            UpdateTime();
        }

        /// <summary>
        /// 数据更新定时器 - 模拟实时数据变化
        /// </summary>
        private void DataTimer_Tick(object sender, EventArgs e)
        {
            UpdateMockData();
        }

        /// <summary>
        /// 更新时间
        /// </summary>
        private void UpdateTime()
        {
            CurrentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 更新模拟数据（添加一些随机变化）
        /// </summary>
        private void UpdateMockData()
        {
            var random = new Random();

            // 风电参数动态变化
            var windBase = 6.0f + (float)(Math.Sin(DateTime.Now.Minute * Math.PI / 30) * 2f); // 风速周期性变化
            _windPower.Power = Math.Max(0, windBase + (float)((random.NextDouble() - 0.5) * 1.0f));
            _windPower.PowerPercentage = (_windPower.Power / 8.0f) * 100;
            _windPower.Voltage = 380f + (float)((random.NextDouble() - 0.5) * 10f); // 电压波动
            _windPower.Current = (float)(_windPower.Power * 1000 / (_windPower.Voltage * Math.Sqrt(3)));
            _windPower.IsGenerating = _windPower.Power > 1.0f;
            _windPower.IsConnected = random.NextDouble() > 0.02; // 98%的时间在线

            // 光伏参数动态变化（模拟日照变化）
            var hour = DateTime.Now.Hour;
            var solarBase = 0f;
            if (hour >= 6 && hour <= 18)
            {
                // 白天有日照，正午最强
                solarBase = 5.0f * (float)Math.Sin((hour - 6) * Math.PI / 12);
                // 添加云层影响的随机波动
                solarBase += (float)((random.NextDouble() - 0.5) * 1.5f);
            }
            _solarPower.Power = Math.Max(0, solarBase);
            _solarPower.PowerPercentage = (_solarPower.Power / 8.0f) * 100;
            _solarPower.Voltage = 395f + (float)((random.NextDouble() - 0.5) * 10f);
            _solarPower.Current = _solarPower.Voltage > 0 ? (float)(_solarPower.Power * 1000 / (_solarPower.Voltage * Math.Sqrt(3))) : 0;
            _solarPower.IsGenerating = _solarPower.Power > 0.5f;
            _solarPower.IsConnected = hour >= 6 && hour <= 18; // 白天连接

            // 发电机参数（备用电源，偶尔启动）
            if (random.NextDouble() < 0.05) // 5%概率启动发电机
            {
                _generator.IsRunning = true;
                _generator.Power = 3.0f + (float)(random.NextDouble() * 2.0f);
                _generator.PowerPercentage = (_generator.Power / 5.0f) * 100;
                _generator.Voltage = 380f + (float)((random.NextDouble() - 0.5) * 15f);
                _generator.Current = (float)(_generator.Power * 1000 / (_generator.Voltage * Math.Sqrt(3)));
            }
            else if (random.NextDouble() < 0.02) // 2%概率停止
            {
                _generator.IsRunning = false;
                _generator.Power = 0;
                _generator.PowerPercentage = 0;
                _generator.Current = 0;
            }

            // 电池参数动态变化
            // 充放电逻辑
            var totalPower = _windPower.Power + _solarPower.Power + _generator.Power;
            var loadPower = 5.0f; // 假设固定负载
            var netPower = totalPower - loadPower;

            if (netPower > 0 && _battery.Soc < 100) // 有多余能量，充电
            {
                _battery.IsCharging = true;
                _battery.IsDischarging = false;
                _battery.Current = Math.Min(50, netPower * 10); // 充电电流
                _battery.Power = -_battery.Current * _battery.Voltage / 1000; // 负值表示充电
                _battery.Soc = Math.Min(100, _battery.Soc + 0.1f);
            }
            else if (netPower < 0 && _battery.Soc > 20) // 能量不足，放电
            {
                _battery.IsCharging = false;
                _battery.IsDischarging = true;
                _battery.Current = Math.Min(50, -netPower * 10); // 放电电流
                _battery.Power = _battery.Current * _battery.Voltage / 1000;
                _battery.Soc = Math.Max(20, _battery.Soc - 0.15f);
            }
            else
            {
                _battery.IsCharging = false;
                _battery.IsDischarging = false;
                _battery.Current = 0;
                _battery.Power = 0;
            }

            // 电池温度变化
            _battery.Temperature = 30f + (float)(Math.Abs(_battery.Current) * 0.1f) + (float)((random.NextDouble() - 0.5) * 2);
            _battery.HasWarning = _battery.Temperature > 45 || _battery.Soc < 25 || _battery.Soc > 95;

            // AC输出参数（并网输出）
            _acOutput.Voltage = 220f + (float)((random.NextDouble() - 0.5) * 5f);
            _acOutput.Current = totalPower > 0 ? (float)(totalPower * 1000 / (_acOutput.Voltage * Math.Sqrt(3))) : 0;
            _acOutput.Power = totalPower;
            _acOutput.PowerPercentage = Math.Min(100, (totalPower / 10.0f) * 100);
            _acOutput.Frequency = 50.0f + (float)((random.NextDouble() - 0.5) * 0.2f); // 频率微小波动
            _acOutput.IsOutputting = totalPower > 0;
            _acOutput.IsInputting = totalPower < 0; // 电网输入的情况

            // 系统温度微小波动
            SystemTemperature = 35f + (float)((random.NextDouble() - 0.5) * 2);
        }

        #region INotifyPropertyChanged实现

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}