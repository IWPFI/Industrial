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
using System.Windows;

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

        /// <summary>
        /// Inits the devices.
        /// </summary>
        /// <remarks></remarks>
        public DataResult<List<DeviceModel>> InitDevices()
        {
            DataResult<List<DeviceModel>> result = new DataResult<List<DeviceModel>>();
            //result.State = false;

            try
            {
                var devices = dataAccess.GetDevices();
                var monitorValues = dataAccess.GetMonitorValues();

                List<DeviceModel> deviceList = new List<DeviceModel>();
                // 设备
                foreach (var d in devices.AsEnumerable())
                {
                    DeviceModel dModel = new DeviceModel();
                    deviceList.Add(dModel);

                    dModel.DeviceID = d.Field<string>("d_id");
                    dModel.DeviceName = d.Field<string>("d_name");
                    //dModel.IsWarning = false;
                    //dModel.IsRunning = true;

                    // 点位
                    foreach (var mv in monitorValues.AsEnumerable().Where(m => m.Field<string>("d_id") == dModel.DeviceID))
                    {
                        MonitorValueModel mvm = new MonitorValueModel();
                        dModel.MonitorValueList.Add(mvm);

                        mvm.ValueId = mv.Field<string>("value_id");
                        mvm.ValueName = mv.Field<string>("value_name");
                        mvm.StorageAreaID = mv.Field<string>("s_area_id");
                        mvm.StartAddress = mv.Field<Int32>("address");
                        mvm.DataType = mv.Field<string>("data_type");
                        mvm.IsAlarm = mv.Field<Int32>("is_alarm") == 1;
                        mvm.ValueDesc = mv.Field<string>("description");
                        mvm.Unit = mv.Field<string>("unit");
                        mvm.ValueState = Base.MonitorValueState.OK;

                        // 警戒值
                        var column = mv.Field<string>("alarm_lolo");
                        mvm.LoLoAlarm = column == null ? 0.0 : double.Parse(column);
                        column = mv.Field<string>("alarm_low");
                        mvm.LowAlarm = column == null ? 0.0 : double.Parse(column);
                        column = mv.Field<string>("alarm_high");
                        mvm.HighAlarm = column == null ? 0.0 : double.Parse(column);
                        column = mv.Field<string>("alarm_hihi");
                        mvm.HiHiAlarm = column == null ? 0.0 : double.Parse(column);

                        mvm.ValueStateChanged = (state, msg, value_id) =>
                         {
                             try
                             {
                                 Application.Current?.Dispatcher.Invoke(() =>
                                 {
                                     var index = dModel.WarningMessage.ToList().FindIndex(w => w.ValueId == value_id);
                                     if (index > -1)
                                         dModel.WarningMessage.RemoveAt(index);

                                     if (state != Base.MonitorValueState.OK)
                                     {
                                         dModel.IsWarning = true;
                                         dModel.WarningMessage.Add(new WarningMessageModel { ValueId = value_id, Message = msg });
                                     }
                                 });

                                 var ss = dModel.WarningMessage.Count > 0;
                                 if (dModel.IsWarning != ss)
                                 {
                                     dModel.IsWarning = ss;
                                 }
                             }
                             catch { }
                         };

                    }
                }

                result.State = true;
                result.Data = deviceList;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }
    }
}
