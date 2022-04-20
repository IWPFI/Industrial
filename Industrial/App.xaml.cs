using Industrial.Base;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Industrial
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //启动全局监控
            GlobalMonitor.Start(
                () =>
                {
                    // 串口打开成功时回调，打开主窗口
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        new MainWindow().Show();
                    });
                },
                // 串口打开失败时回调，错误消息提醒，并退出程序
                (msg) =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        MessageBox.Show(msg, "系统启动失败");
                        Application.Current.Shutdown();
                    });
                });
        }
        protected override void OnExit(ExitEventArgs e)
        {
            GlobalMonitor.Dispose();//应用退出时销毁

            base.OnExit(e);
        }
    }
}
