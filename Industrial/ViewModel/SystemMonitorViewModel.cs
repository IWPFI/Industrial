using Industrial.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Industrial.ViewModel
{
    public class SystemMonitorViewModel
    {
        public ObservableCollection<LogModel> LogList { get; set; } = new ObservableCollection<LogModel>();

        public SystemMonitorViewModel()
        {
            InitLogInfo();
        }

        void InitLogInfo()
        {
            this.LogList.Add(new LogModel { RowNumber = 1, DeviceName = "冷却塔 1#", LogInfo = "已启动", LogType = Base.LogType.Info });
            this.LogList.Add(new LogModel { RowNumber = 2, DeviceName = "冷却塔 2#", LogInfo = "已启动", LogType = Base.LogType.Info });
            this.LogList.Add(new LogModel { RowNumber = 3, DeviceName = "冷却塔 3#", LogInfo = "液位极低", LogType = Base.LogType.Warn });
            this.LogList.Add(new LogModel { RowNumber = 4, DeviceName = "循环水泵 1#", LogInfo = "频率过大", LogType = Base.LogType.Warn });
            this.LogList.Add(new LogModel { RowNumber = 5, DeviceName = "循环水泵 2#", LogInfo = "已启动", LogType = Base.LogType.Info });
            this.LogList.Add(new LogModel { RowNumber = 6, DeviceName = "循环水泵 3#", LogInfo = "已启动", LogType = Base.LogType.Info });
            this.LogList.Add(new LogModel { RowNumber = 7, DeviceName = "循环水泵 4#", LogInfo = "异常", LogType = Base.LogType.Fault });
        }
    }
}
