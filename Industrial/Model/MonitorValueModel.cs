using Industrial.Base;
using LiveCharts;
using LiveCharts.Defaults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Industrial.Model
{
    public class MonitorValueModel : NotifyPropertyBase
    {
        /// <summary>
        /// Gets or sets the value state changed.
        /// </summary>
        public Action<MonitorValueState, string, string> ValueStateChanged { get; set; }

        /// <summary>
        /// Gets or sets the value id.
        /// </summary>
        public string ValueId { get; set; }

        /// <summary>
        /// Gets or sets the value name.
        /// </summary>
        public string ValueName { get; set; }

        /// <summary>
        /// Gets or sets the storage area id.
        /// </summary>
        public string StorageAreaID { get; set; }

        /// <summary>
        /// Gets or sets the start address.
        /// </summary>
        /// <remarks>起始地址</remarks>
        public int StartAddress { get; set; }

        /// <summary>
        /// Gets or sets the data type.
        /// </summary>
        public string DataType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is alarm.
        /// </summary>
        /// <remarks>是否报警</remarks>
        public bool IsAlarm { get; set; }

        /// <summary>
        /// Very low alarm
        /// </summary>
        /// <remarks>极低报警</remarks>
        public double LoLoAlarm { get; set; }

        /// <summary>
        /// Gets or sets the low alarm.
        /// </summary>
        /// <remarks>低警报值</remarks>
        public double LowAlarm { get; set; }

        /// <summary>
        /// Gets or sets the high alarm.
        /// </summary>
        ///  <remarks>高警报值</remarks>
        public double HighAlarm { get; set; }

        /// <summary>
        /// Ultra high alarm
        /// </summary>
        /// <remarks>超高报警</remarks>
        public double HiHiAlarm { get; set; }

        /// <summary>
        /// Gets or sets the unit.
        /// </summary>
        /// <remarks>单位</remarks>
        public string Unit { get; set; }

        /// <summary>
        /// Gets or sets the value desc.
        /// </summary>
        public string ValueDesc { get; set; }

        /// <summary>
        /// Gets or sets the value state.
        /// </summary>
        /// <remarks>当前状态</remarks>
        public MonitorValueState ValueState { get; set; }

        private double _currentValue;
        /// <summary>
        /// Gets or sets the current value.
        /// </summary>
        /// <remarks>当前值</remarks>
        public double CurrentValue
        {
            get { return _currentValue; }
            set
            {
                _currentValue = value;
                this.RaisePropertyChanged();

                if (IsAlarm)
                {
                    string msg = ValueDesc;
                    this.ValueState = MonitorValueState.OK;
                    if (value < LoLoAlarm)
                    {
                        ValueState = MonitorValueState.LoLo;
                        msg += "极低";
                    }
                    else if (value < LowAlarm)
                    {
                        ValueState = MonitorValueState.Low;
                        msg += "过低";
                    }
                    else if (value > HiHiAlarm)
                    {
                        ValueState = MonitorValueState.HiHi;
                        msg += "极高";
                    }
                    else if (value > HighAlarm)
                    {
                        ValueState = MonitorValueState.High;
                        msg += "过高";
                    }
                    ValueStateChanged?.Invoke(ValueState, msg + "。当前值：" + value, ValueId);
                }
                Values.Add(new ObservableValue(value));
                if (Values.Count > 60) Values.RemoveAt(0);
            }
        }

        public ChartValues<ObservableValue> Values { get; set; } = new ChartValues<ObservableValue>();
    }
}
