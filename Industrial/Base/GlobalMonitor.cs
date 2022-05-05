using Communication;
using Industrial.BLL;
using Industrial.Modbus;
using Industrial.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Industrial.Base
{
    public class GlobalMonitor
    {
        /// <summary>
        /// Gets or sets the storage list.
        /// </summary>
        /// <remarks>存储区信息</remarks>
        public static List<StorageModel> StorageList { get; set; }

        /// <summary>
        /// Gets or sets the device list.
        /// </summary>
        /// <remarks>设备列表</remarks>
        public static List<DeviceModel> DeviceList { get; set; } = new List<DeviceModel>();

        /// <summary>
        /// Gets or sets the serial info.
        /// </summary>
        /// <remarks>串口信息</remarks>
        public static SerialInfo SerialInfo { get; set; }

        static bool isRunning = true;
        static Task mainTask = null;//清除线程
        static RTU rtuInstance = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="successAction">成功</param>
        /// <param name="faultAction">失败</param>
        public static void Start(Action successAction, Action<string> faultAction)
        {
            mainTask = Task.Run(async () =>
             {
                 IndustrialBLL bll = new IndustrialBLL();
                 // 获取串口配置信息
                 var si = bll.InitSerialInfo();
                 if (si.State)
                 {
                     SerialInfo = si.Data;
                 }
                 else
                 {
                     faultAction(si.Message);
                     return;
                 }

                 // 获取存储区信息
                 var sa = bll.InitStorageArea();
                 if (sa.State)
                 {
                     StorageList = sa.Data;
                 }
                 else
                 {
                     faultAction.Invoke(sa.Message); return;
                 }

                 /// 初始化设备变量集合以及警戒值
                 var dr = bll.InitDevices();
                 if (dr.State)
                 {
                     DeviceList = dr.Data;
                 }
                 else
                 {
                     faultAction.Invoke(dr.Message);
                     return;
                 }

                 //初始化串口通信
                 rtuInstance = RTU.GetInstance(SerialInfo);
                 rtuInstance.ResponseData = new Action<int, List<byte>>(ParsingData);

                 if (rtuInstance.Connection())
                 {
                     successAction();

                     int startAddr = 0;
                     while (isRunning)
                     {
                         //发送

                         foreach (var item in StorageList)
                         {
                             if (item.Length > 100)
                             {
                                 //分段截取
                                 startAddr = item.StartAddress;
                                 int readCount = item.Length / 100;//如果是100就发一次请求，200就发两次
                                 for (int i = 0; i < readCount; i++)
                                 {
                                     int readLen = i == readCount ? item.Length - 100 * i : 100;
                                     await rtuInstance.Send(item.SlaveAddress, (byte)int.Parse(item.FuncCode), startAddr + 100 * i, readLen);
                                     //异步
                                 }
                             }
                             if (item.Length % 100 > 0)//把剩余的在请求一次
                             {
                                 await rtuInstance.Send(item.SlaveAddress, (byte)int.Parse(item.FuncCode), startAddr + 100 * (item.Length / 100), item.Length % 100);
                             }
                         }
                     }
                 }
                 else
                 {
                     faultAction("程序无法启动，串口连接初始化失败！请检查设备是否连接正常。");
                 }
             });
        }

        public static void Dispose()
        {
            isRunning = false;

            if (rtuInstance != null) { rtuInstance.Dispose(); }

            if (mainTask != null) { mainTask.Wait(); }
        }

        /// <summary>
        /// 解析数据
        /// </summary>
        /// <param name="start_addr"></param>
        /// <param name="byteList"></param>
        private static void ParsingData(int start_addr, List<byte> byteList)
        {
            if (byteList != null && byteList.Count > 0)
            {
                // 查找设备监控点位与当前返回报文相关的监控数据列表
                // 根据从站地址、功能码、起始地址
                var mvl = (from q in DeviceList
                           from m in q.MonitorValueList
                           where m.StorageAreaID == (byteList[0].ToString() + byteList[1].ToString("00") + start_addr.ToString())
                           select m
                         ).ToList();

                int startByte;
                byte[] res = null;
                foreach (var item in mvl)
                {
                    switch (item.DataType)
                    {
                        case "Float":
                            startByte = item.StartAddress * 2 + 3;//只需要前面3个
                            if (startByte < start_addr + byteList.Count)
                            {
                                res = new byte[4] { byteList[startByte], byteList[startByte + 1], byteList[startByte + 2],
                                    byteList[startByte + 3] };//解析
                                item.CurrentValue = Convert.ToDouble(res.ByteArrsyToFloat());
                            }
                            break;
                        case "Bool":
                            break;
                    }
                }
            }
        }
    }
}
