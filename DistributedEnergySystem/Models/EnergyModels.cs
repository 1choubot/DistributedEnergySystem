using System.ComponentModel;

namespace DistributedEnergySystem.Models
{
    /// <summary>
    /// 风电参数模型
    /// </summary>
    public class WindPowerModel : INotifyPropertyChanged
    {
        private float _voltage;
        private float _current;
        private float _power;
        private float _powerPercentage;
        private bool _isConnected;
        private bool _isGenerating;

        public float Voltage
        {
            get => _voltage;
            set { _voltage = value; OnPropertyChanged(nameof(Voltage)); OnPropertyChanged(nameof(VoltageDisplay)); }
        }

        public float Current
        {
            get => _current;
            set { _current = value; OnPropertyChanged(nameof(Current)); OnPropertyChanged(nameof(CurrentDisplay)); }
        }

        public float Power
        {
            get => _power;
            set
            {
                _power = value;
                OnPropertyChanged(nameof(Power));
                OnPropertyChanged(nameof(PowerDisplay));
            }
        }

        public float PowerPercentage
        {
            get => _powerPercentage;
            set { _powerPercentage = value; OnPropertyChanged(nameof(PowerPercentage)); }
        }

        public bool IsConnected
        {
            get => _isConnected;
            set { _isConnected = value; OnPropertyChanged(nameof(IsConnected)); OnPropertyChanged(nameof(StatusText)); }
        }

        public bool IsGenerating
        {
            get => _isGenerating;
            set { _isGenerating = value; OnPropertyChanged(nameof(IsGenerating)); OnPropertyChanged(nameof(StatusText)); }
        }

        // 计算属性 - 不需要单独存储，每次访问时计算
        public string PowerDisplay => $"{Power:F1} kW";
        public string VoltageDisplay => $"{Voltage:F1} V";
        public string CurrentDisplay => $"{Current:F1} A";
        public string StatusText => IsConnected ? (IsGenerating ? "运行中" : "已连接") : "未连接";

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    /// <summary>
    /// 光伏参数模型
    /// </summary>
    public class SolarPowerModel : INotifyPropertyChanged
    {
        private float _voltage;
        private float _current;
        private float _power;
        private float _powerPercentage;
        private bool _isConnected;
        private bool _isGenerating;

        public float Voltage
        {
            get => _voltage;
            set { _voltage = value; OnPropertyChanged(nameof(Voltage)); OnPropertyChanged(nameof(VoltageDisplay)); }
        }

        public float Current
        {
            get => _current;
            set { _current = value; OnPropertyChanged(nameof(Current)); OnPropertyChanged(nameof(CurrentDisplay)); }
        }

        public float Power
        {
            get => _power;
            set
            {
                _power = value;
                OnPropertyChanged(nameof(Power));
                OnPropertyChanged(nameof(PowerDisplay));
            }
        }

        public float PowerPercentage
        {
            get => _powerPercentage;
            set { _powerPercentage = value; OnPropertyChanged(nameof(PowerPercentage)); }
        }

        public bool IsConnected
        {
            get => _isConnected;
            set { _isConnected = value; OnPropertyChanged(nameof(IsConnected)); OnPropertyChanged(nameof(StatusText)); }
        }

        public bool IsGenerating
        {
            get => _isGenerating;
            set { _isGenerating = value; OnPropertyChanged(nameof(IsGenerating)); OnPropertyChanged(nameof(StatusText)); }
        }

        public string PowerDisplay => $"{Power:F1} kW";
        public string VoltageDisplay => $"{Voltage:F1} V";
        public string CurrentDisplay => $"{Current:F1} A";
        public string StatusText => IsConnected ? (IsGenerating ? "发电中" : "已连接") : "未连接";

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    /// <summary>
    /// 电池参数模型
    /// </summary>
    public class BatteryModel : INotifyPropertyChanged
    {
        private float _voltage;
        private float _current;
        private float _power;
        private float _soc;  // State of Charge - 电量百分比
        private float _temperature;
        private bool _isCharging;
        private bool _isDischarging;
        private bool _hasWarning;

        public float Voltage
        {
            get => _voltage;
            set { _voltage = value; OnPropertyChanged(nameof(Voltage)); OnPropertyChanged(nameof(VoltageDisplay)); }
        }

        public float Current
        {
            get => _current;
            set { _current = value; OnPropertyChanged(nameof(Current)); OnPropertyChanged(nameof(CurrentDisplay)); }
        }

        public float Power
        {
            get => _power;
            set
            {
                _power = value;
                OnPropertyChanged(nameof(Power));
                OnPropertyChanged(nameof(PowerDisplay));
                OnPropertyChanged(nameof(PowerType));
            }
        }

        public float Soc
        {
            get => _soc;
            set { _soc = value; OnPropertyChanged(nameof(Soc)); OnPropertyChanged(nameof(SocDisplay)); OnPropertyChanged(nameof(SocColor)); }
        }

        public float Temperature
        {
            get => _temperature;
            set { _temperature = value; OnPropertyChanged(nameof(Temperature)); OnPropertyChanged(nameof(TemperatureDisplay)); }
        }

        public bool IsCharging
        {
            get => _isCharging;
            set { _isCharging = value; OnPropertyChanged(nameof(IsCharging)); OnPropertyChanged(nameof(StatusText)); OnPropertyChanged(nameof(StatusColor)); }
        }

        public bool IsDischarging
        {
            get => _isDischarging;
            set { _isDischarging = value; OnPropertyChanged(nameof(IsDischarging)); OnPropertyChanged(nameof(StatusText)); OnPropertyChanged(nameof(StatusColor)); }
        }

        public bool HasWarning
        {
            get => _hasWarning;
            set { _hasWarning = value; OnPropertyChanged(nameof(HasWarning)); OnPropertyChanged(nameof(StatusColor)); }
        }

        // 显示属性
        public string VoltageDisplay => $"{Voltage:F1} V";
        public string CurrentDisplay => $"{Current:F1} A";
        public string PowerDisplay => $"{Math.Abs(Power):F1} kW";
        public string SocDisplay => $"{Soc:F1}%";
        public string TemperatureDisplay => $"{Temperature:F1}°C";

        // 颜色属性
        public string SocColor
        {
            get
            {
                if (Soc > 60) return "#4CAF50";  // 绿色 - 电量充足
                if (Soc > 20) return "#FFC107";  // 黄色 - 电量中等
                return "#F44336";                // 红色 - 电量不足
            }
        }

        public string StatusColor
        {
            get
            {
                if (HasWarning) return "#F44336";  // 红色 - 有警告
                if (IsCharging) return "#2196F3";  // 蓝色 - 充电中
                if (IsDischarging) return "#4CAF50"; // 绿色 - 放电中
                return "#757575";                   // 灰色 - 待机
            }
        }

        public string BreathingColor
        {
            get
            {
                if (HasWarning) return "#F44336";  // 红色 - 有警告
                if (IsCharging) return "#2196F3";  // 蓝色 - 充电中
                if (IsDischarging) return "#4CAF50"; // 绿色 - 放电中
                return "#10B981";                   // 绿色 - 正常待机
            }
        }

        public string StatusText
        {
            get
            {
                if (IsCharging) return "充电中";
                if (IsDischarging) return "放电中";
                return "待机";
            }
        }

        public string PowerType
        {
            get
            {
                if (Power > 0) return "放电";
                if (Power < 0) return "充电";
                return "待机";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    /// <summary>
    /// 发电机参数模型
    /// </summary>
    public class GeneratorModel : INotifyPropertyChanged
    {
        private float _voltage;
        private float _current;
        private float _power;
        private float _powerPercentage;
        private bool _isRunning;

        public float Voltage
        {
            get => _voltage;
            set { _voltage = value; OnPropertyChanged(nameof(Voltage)); OnPropertyChanged(nameof(VoltageDisplay)); }
        }

        public float Current
        {
            get => _current;
            set { _current = value; OnPropertyChanged(nameof(Current)); OnPropertyChanged(nameof(CurrentDisplay)); }
        }

        public float Power
        {
            get => _power;
            set
            {
                _power = value;
                OnPropertyChanged(nameof(Power));
                OnPropertyChanged(nameof(PowerDisplay));
            }
        }

        public float PowerPercentage
        {
            get => _powerPercentage;
            set { _powerPercentage = value; OnPropertyChanged(nameof(PowerPercentage)); }
        }

        public bool IsRunning
        {
            get => _isRunning;
            set { _isRunning = value; OnPropertyChanged(nameof(IsRunning)); OnPropertyChanged(nameof(StatusText)); }
        }

        public string VoltageDisplay => $"{Voltage:F1} V";
        public string CurrentDisplay => $"{Current:F1} A";
        public string PowerDisplay => $"{Power:F1} kW";
        public string StatusText => IsRunning ? "运行中" : "已停止";

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    /// <summary>
    /// AC输出参数模型
    /// </summary>
    public class ACOutputModel : INotifyPropertyChanged
    {
        private float _voltage;
        private float _current;
        private float _power;
        private float _powerPercentage;
        private float _frequency;  // 频率
        private bool _isOutputting;
        private bool _isInputting;

        public float Voltage
        {
            get => _voltage;
            set { _voltage = value; OnPropertyChanged(nameof(Voltage)); OnPropertyChanged(nameof(VoltageDisplay)); }
        }

        public float Current
        {
            get => _current;
            set { _current = value; OnPropertyChanged(nameof(Current)); OnPropertyChanged(nameof(CurrentDisplay)); }
        }

        public float Power
        {
            get => _power;
            set
            {
                _power = value;
                OnPropertyChanged(nameof(Power));
                OnPropertyChanged(nameof(PowerDisplay));
                OnPropertyChanged(nameof(PowerType));
            }
        }

        public float PowerPercentage
        {
            get => _powerPercentage;
            set { _powerPercentage = value; OnPropertyChanged(nameof(PowerPercentage)); }
        }

        public float Frequency
        {
            get => _frequency;
            set { _frequency = value; OnPropertyChanged(nameof(Frequency)); OnPropertyChanged(nameof(FrequencyDisplay)); }
        }

        public bool IsOutputting
        {
            get => _isOutputting;
            set { _isOutputting = value; OnPropertyChanged(nameof(IsOutputting)); OnPropertyChanged(nameof(StatusText)); }
        }

        public bool IsInputting
        {
            get => _isInputting;
            set { _isInputting = value; OnPropertyChanged(nameof(IsInputting)); OnPropertyChanged(nameof(StatusText)); }
        }

        public string VoltageDisplay => $"{Voltage:F1} V";
        public string CurrentDisplay => $"{Math.Abs(Current):F1} A";
        public string PowerDisplay => $"{Math.Abs(Power):F1} kW";
        public string FrequencyDisplay => $"{Frequency:F1} Hz";

        // 新增双向功率显示属性
        public string WorkMode
        {
            get
            {
                if (Power > 0) return "并网输出";
                if (Power < 0) return "电网充电";
                return "待机";
            }
        }

        public string GridVoltageDisplay => $"{Voltage:F1} V";
        public string CurrentTypeText => Power > 0 ? "输出" : Power < 0 ? "输入" : "无";
        public string CurrentTypeDisplay => $"{CurrentTypeText}:";
        public string PowerLabelText => Power > 0 ? "输出功率:" : Power < 0 ? "输入功率:" : "功率:";
        public string PowerFlowText => Power > 0 ? "→ 电网" : Power < 0 ? "← 电网" : "无流向";
        public string MaxPowerDisplay => "5.0kW";

        public string StatusText
        {
            get
            {
                if (IsOutputting) return "输出中";
                if (IsInputting) return "输入中";
                return "待机";
            }
        }

        public string PowerType
        {
            get
            {
                if (Power > 0) return "输出";
                if (Power < 0) return "输入";
                return "无";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}