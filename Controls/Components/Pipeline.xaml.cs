using System;
using System.Collections.Generic;
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

namespace Controls.Components
{
    /// <summary>
    /// Pipeline.xaml 的交互逻辑
    /// </summary>
    public partial class Pipeline : UserControl
    {

        /// <summary>
        /// Gets or sets the direction.
        /// </summary>
        /// <remarks>液体流向，接受1和2两个值</remarks>
        public int Direction
        {
            get { return (int)GetValue(DirectionProperty); }
            set { SetValue(DirectionProperty, value); }
        }
        public static readonly DependencyProperty DirectionProperty =
            DependencyProperty.Register("Direction", typeof(int), typeof(Pipeline), 
                new PropertyMetadata(default(int), new PropertyChangedCallback(OnDirectionChanged)));

        private static void OnDirectionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            int value = int.Parse(e.NewValue.ToString());
            VisualStateManager.GoToState(d as Pipeline, value == 1 ? "WEFlowState" : "EWFlowState", false);
        }

        /// <summary>
        /// Gets or sets the liquid color.
        /// </summary>
        /// <remarks>液体颜色</remarks>
        public Brush LiquidColor
        {
            get { return (Brush)GetValue(LiguidColorProperty); }
            set { SetValue(LiguidColorProperty, value); }
        }
        public static readonly DependencyProperty LiguidColorProperty =
            DependencyProperty.Register("LiquidColor", typeof(Brush), typeof(Pipeline), 
                new PropertyMetadata(Brushes.Orange));

        /// <summary>
        /// Gets or sets the cap radius.
        /// </summary>
        /// <remarks>封口半径</remarks>
        public int CapRadius
        {
            get { return (int)GetValue(CapRadiusProperty); }
            set { SetValue(CapRadiusProperty, value); }
        }
        public static readonly DependencyProperty CapRadiusProperty =
            DependencyProperty.Register("CapRadius", typeof(int), typeof(Pipeline), new PropertyMetadata(default(int)));



        public Pipeline()
        {
            InitializeComponent();
        }
    }
}
