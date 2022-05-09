using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Controls.Components
{
    public class ComponentBase : UserControl
    {
        private bool _isSelected;

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;

                if (value)
                {
                    var parent = VisualTreeHelper.GetParent(this) as Grid;
                    if (parent != null)
                    {
                        foreach (var item in parent.Children)
                        {
                            if (item is ComponentBase)
                                (item as ComponentBase).IsSelected = false;
                        }
                    }
                }

                VisualStateManager.GoToState(this, value ? "SelectedState" : "UnselectedState", false);
            }
        }

        public bool IsRunning
        {
            get { return (bool)GetValue(IsRunningProperty); }
            set { SetValue(IsRunningProperty, value); }
        }
        public static readonly DependencyProperty IsRunningProperty =
            DependencyProperty.Register("IsRunning", typeof(bool), typeof(ComponentBase), new PropertyMetadata(default(bool), new PropertyChangedCallback(OnRunningStateChanged)));

        private static void OnRunningStateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            bool state = (bool)e.NewValue;
            VisualStateManager.GoToState(d as ComponentBase, state ? "RunState" : "StopState", false);
        }

        public bool IsFault
        {
            get { return (bool)GetValue(IsFaultProperty); }
            set { SetValue(IsFaultProperty, value); }
        }
        public static readonly DependencyProperty IsFaultProperty =
            DependencyProperty.Register("IsFault", typeof(bool), typeof(ComponentBase), new PropertyMetadata(default(bool), new PropertyChangedCallback(OnFaultStateChanged)));

        private static void OnFaultStateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            VisualStateManager.GoToState(d as ComponentBase, (bool)e.NewValue ? "FaultState" : "NormalState", false);
        }

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(ComponentBase), new PropertyMetadata(default(ICommand)));

        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }
        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register("CommandParameter", typeof(object), typeof(ComponentBase), new PropertyMetadata(default(object)));


        public ComponentBase()
        {
            this.PreviewMouseLeftButtonDown += ComponentBase_PreviewMouseLeftButtonDown;
        }

        private void ComponentBase_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.IsSelected = !this.IsSelected;

            this.Command?.Execute(this.CommandParameter);

            e.Handled = true;
        }
    }
}
