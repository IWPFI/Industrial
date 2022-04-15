using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Industrial.Base
{
    /// <summary>
    /// The monitor value state.
    /// </summary>
    /// <remarks>监视器值状态</remarks>
    public enum MonitorValueState
    {
        /// <summary>
        /// 超低状态
        /// </summary>
        LoLo = 0,

        /// <summary>
        /// 低状态
        /// </summary>
        Low = 1,

        /// <summary>
        /// 正常
        /// </summary>
        OK = 2,

        /// <summary>
        /// 高状态
        /// </summary>
        High = 3,

        /// <summary>
        /// 超高状态
        /// </summary>
        HiHi = 4
    }
}
