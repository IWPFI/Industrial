using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Industrial.BLL
{
    /// <summary>
    /// Result transfer class
    /// </summary>
    public class DataResult<T>
    {
        /// <summary>
        /// 状态
        /// </summary>
        /// <remarks>没有异常就返回true</remarks>
        public bool State { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public T Data { get; set; }
    }

    public class DataResult : DataResult<string> { }
}
