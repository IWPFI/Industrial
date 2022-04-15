using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Industrial.Model
{
    public class StorageModel
    {

        /// <summary>
        /// Gets or sets the storage id.
        /// </summary>
        /// <remarks>存储id</remarks>
        public string StorageID { get; set; }

        /// <summary>
        /// Gets or sets the slave address.
        /// </summary>
        /// <remarks>存储地址</remarks>
        public int SlaveAddress { get; set; }

        /// <summary>
        /// Gets or sets the func code.
        /// </summary>
        /// <remarks>功能码</remarks>
        public string FuncCode { get; set; }

        /// <summary>
        /// Gets or sets the storage name.
        /// </summary>
        /// <remarks></remarks>
        public string StorageName { get; set; }

        /// <summary>
        /// Gets or sets the start address.
        /// </summary>
        /// <remarks>起始地址</remarks>
        public int StartAddress { get; set; }

        /// <summary>
        /// Gets or sets the length.
        /// </summary>
        /// <remarks>长度</remarks>
        public int Length { get; set; }
    }
}
