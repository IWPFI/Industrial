using Communication;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Industrial.BLL
{
    public class IndustrialBLL
    {
        //获取串口信息
        public DataResult<SerialInfo> InitSerialInfo()
        {
            DataResult<SerialInfo> result = new DataResult<SerialInfo>();
            result.State = false;

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
    }
}
