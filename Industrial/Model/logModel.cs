using Industrial.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Industrial.Model
{
    public class LogModel
    {

        /// <summary>
        /// Gets or sets the row number.
        /// </summary>
        /// <remarks>行号</remarks>
        public int RowNumber { get; set; }

        /// <summary>
        /// Gets or sets the device name.
        /// </summary>
        /// <remarks>设备名称</remarks>
        public string DeviceName { get; set; }


        /// <summary>
        /// Gets or sets the log info.
        /// </summary>
        /// <remarks>状态</remarks>
        public string LogInfo { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <remarks>详细信息</remarks>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the log type.
        /// </summary>
        /// <remarks>日志类型</remarks>
        public LogType LogType { get; set; }
    }
}
