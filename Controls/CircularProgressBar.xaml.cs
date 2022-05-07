using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Controls
{
    /// <summary>
    /// CircularProgressBar.xaml 的交互逻辑
    /// </summary>
    public partial class CircularProgressBar : UserControl
    {
        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(CircularProgressBar), new PropertyMetadata(default(double), new PropertyChangedCallback(OnPropertyChanged)));

        public Brush BackColor
        {
            get { return (Brush)GetValue(BackColorProperty); }
            set { SetValue(BackColorProperty, value); }
        }
        public static readonly DependencyProperty BackColorProperty =
            DependencyProperty.Register("BackColor", typeof(Brush), typeof(CircularProgressBar), new PropertyMetadata(Brushes.LightGray));

        public Brush ForeColor
        {
            get { return (Brush)GetValue(ForeColorProperty); }
            set { SetValue(ForeColorProperty, value); }
        }
        public static readonly DependencyProperty ForeColorProperty =
            DependencyProperty.Register("ForeColor", typeof(Brush), typeof(CircularProgressBar), new PropertyMetadata(Brushes.Orange));


        public CircularProgressBar()
        {
            InitializeComponent();

            this.SizeChanged += CircularProgressBar_SizeChanged;
        }

        private void CircularProgressBar_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.UpdateValue();
        }

        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as CircularProgressBar).UpdateValue();
        }

        private void UpdateValue()
        {
            this.layout.Width = Math.Min(this.RenderSize.Width, this.RenderSize.Height);//设置最小宽度和高度使其永远在设定的范围内--动态改变
            double radius = Math.Min(this.RenderSize.Width, this.RenderSize.Height) / 2;
            if (radius <= 0) return;

            double newValue = this.Value % 100.0;
            double newX = 0.0, newY = 0.0;

            newX = radius + (radius - 3) * Math.Cos((newValue * 3.6 - 90) * Math.PI / 180);
            newY = radius + (radius - 3) * Math.Sin((newValue * 3.6 - 90) * Math.PI / 180);

            //<Path Name="path" StrokeThickness="4" Data="M75 3A75 75 0 0 1 147 75" StrokeStartLineCap = "Round" StrokeEndLineCap = "Round" />
            //M75 3A75 75 0 0 1 147 75
            string pathDataStr = "M{0} 3A{1} {1} 0 {4} 1 {2} {3}";
            pathDataStr = string.Format(pathDataStr,
                radius + 0.01, //+0.01：防止但半径为100时出现重叠现象
                radius - 3,
                newX, 
                newY,
                newValue < 50 && newValue > 0 ? 0 : 1//设置进度⚪跟随整体宽/高进行调整
                );
            var converter = TypeDescriptor.GetConverter(typeof(Geometry));
            this.path.Data = (Geometry)converter.ConvertFrom(pathDataStr);
        }
    }
}
