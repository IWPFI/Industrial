using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Industrial.Model
{
    public class DeviceModel  : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        /// <summary>
        /// Gets or sets the device i d.
        /// </summary>
        /// <remarks>设备id</remarks>
        public string DeviceID { get; set; }

        /// <summary>
        /// Gets or sets the device name.
        /// </summary>
        /// <remarks>设备名称</remarks>
        public string DeviceName { get; set; }

        private bool _isRunning;
        /// <summary>
        /// Gets or sets a value indicating whether is running.
        /// </summary>
        /// <remarks>是否正常运行</remarks>
        public bool IsRunning
        {
            get { return _isRunning; }
            set { _isRunning = value; this.OnPropertyChanged(); }
        }

        private bool _isWarning=false;
        /// <summary>
        /// Gets or sets a value indicating whether is warning.
        /// </summary>
        /// <remarks>是否是报警</remarks>
        public bool IsWarning
        {
            get { return _isWarning; }
            set { _isWarning = value; this.OnPropertyChanged(); }
        }

        /// <summary>
        /// Gets or sets the warning message.
        /// </summary>
        /// <remarks>警告信息</remarks>
        public ObservableCollection<WarningMessageModel> WarningMessage { get; set; } = new ObservableCollection<WarningMessageModel>();

        /// <summary>
        /// Gets or sets the monitor value list.
        /// </summary>
        /// <remarks>监控数据</remarks>
        public ObservableCollection<MonitorValueModel> MonitorValueList { get; set; } = new ObservableCollection<MonitorValueModel>();
    }
}
