﻿using Communication;
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
            mainTask = Task.Run(() =>
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
                 if (rtuInstance.Connection())
                 {
                     successAction();

                     while (isRunning)
                     {

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
    }
}