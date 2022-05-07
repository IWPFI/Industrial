using Industrial.Base;
using System;
using System.Reflection;
using System.Windows;

namespace Industrial.ViewModel
{
    public class MainViewModel : NotifyPropertyBase
    {
        private UIElement _mainContent;

        public UIElement MainContent
        {
            get { return _mainContent; }
            set
            {
                Set<UIElement>(ref _mainContent, value);
            }
        }

        public CommandBase TabChangedCommand { get; set; }

        public MainViewModel()
        {
            // 完整方式
            //TabChangedCommand = new CommandBase((o) =>
            //  {
            //      string[] strValues = o.ToString().Split('|');
            //      Assembly assembly = Assembly.LoadFrom(strValues[0]);
            //      Type type = assembly.GetType(strValues[1]);
            //      this.MainContent = (UIElement)Activator.CreateInstance(type);
            //  });
            TabChangedCommand = new CommandBase(OnTabChanged);
            OnTabChanged("Industrial.View.SystemMonitor");//首次启动显示的界面
        }

        private void OnTabChanged(object obj)
        {
            if (obj == null) return;

            // 简化方式，必须在同一个程序集下
            Type type = Type.GetType(obj.ToString());
            this.MainContent = (UIElement)Activator.CreateInstance(type);
        }
    }
}
