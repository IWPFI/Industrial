using Communication;
using Industrial.DAL;
using Industrial.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Industrial.BLL
{
    public class IndustrialBLL
    {
        DataAccess dataAccess = DataAccess.GetInstance();

        /// <summary>
        /// 获取串口信息
        /// </summary>
        /// <returns></returns>
        public DataResult<SerialInfo> InitSerialInfo()
        {
            DataResult<SerialInfo> result = new DataResult<SerialInfo>();
            //result.State = false;

            try
            {
                SerialInfo SerialInfo = new SerialInfo();
                SerialInfo.PortName = ConfigurationManager.AppSettings["port"];
                SerialInfo.BaudRate = int.Parse(ConfigurationManager.AppSettings["baud"].ToString());
                SerialInfo.DataBit = int.Parse(ConfigurationManager.AppSettings["data_bit"].ToString());
                SerialInfo.Parity = (System.IO.Ports.Parity)Enum.Parse(typeof(System.IO.Ports.Parity), ConfigurationManager.AppSettings["parity"].ToString(), true);
                SerialInfo.StopBits = (System.IO.Ports.StopBits)Enum.Parse(typeof(System.IO.Ports.StopBits), ConfigurationManager.AppSettings["stop_bit"].ToString(), true);

                result.State = true;
                result.Data = SerialInfo;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        /// <summary>
        /// Inits the storage area.
        /// </summary>
        /// <remarks>初始化存储器</remarks>
        public DataResult<List<StorageModel>> InitStorageArea()
        {
            DataResult<List<StorageModel>> result = new DataResult<List<StorageModel>>();
            //result.State = false;

            try
            {
                var sa = dataAccess.GetStorageArea();

                result.Data = (from q in sa.AsEnumerable()
                               select new StorageModel
                               {
                                   StorageID = q.Field<string>("id"),
                                   SlaveAddress = q.Field<Int32>("slave_add"),
                                   FuncCode = q.Field<string>("func_code"),
                                   StorageName = q.Field<string>("s_area_name"),
                                   StartAddress = int.Parse(q.Field<string>("start_reg")),
                                   Length = int.Parse(q.Field<string>("length"))
                               }).ToList();
                result.State = true;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }
    }
}
