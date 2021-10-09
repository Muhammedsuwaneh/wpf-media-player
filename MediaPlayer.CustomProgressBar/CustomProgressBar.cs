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

namespace MediaPlayer.CustomProgressBar
{
    public class CustomProgressBar : Control
    {
        #region Bar Border Properties 
        public double BarWidth
        {
            get { return (double)GetValue(BarWidthProperty); }
            set { SetValue(BarWidthProperty, value); }
        }

        public static readonly DependencyProperty BarWidthProperty =
            DependencyProperty.Register("BarWidth", typeof(double), typeof(CustomProgressBar), new PropertyMetadata(760.0));


        public double BarHeight
        {
            get { return (double)GetValue(BarHeightProperty); }
            set { SetValue(BarHeightProperty, value); }
        }

        public static readonly DependencyProperty BarHeightProperty =
            DependencyProperty.Register("BarHeight", typeof(double), typeof(CustomProgressBar), new PropertyMetadata(10.0));


        public Brush BarColor
        {
            get { return (Brush)GetValue(BarColorProperty); }
            set { SetValue(BarColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ProgressColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BarColorProperty =
            DependencyProperty.Register("BarColor", typeof(Brush), typeof(CustomProgressBar), new PropertyMetadata(Brushes.White));

        #endregion

        #region Progress


        public double ProgressWidth
        {
            get { return (double)GetValue(ProgressWidthProperty); }
            set { SetValue(ProgressWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ProgressWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProgressWidthProperty =
            DependencyProperty.Register("ProgressWidth", typeof(double), typeof(CustomProgressBar), new PropertyMetadata(0.0));


        public double ProgressHeight
        {
            get { return (double)GetValue(ProgressHeightProperty); }
            set { SetValue(ProgressHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ProgressHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProgressHeightProperty =
            DependencyProperty.Register("ProgressHeight", typeof(double), typeof(CustomProgressBar), new PropertyMetadata(10.0));



        public SolidColorBrush ProgressColor
        {
            get { return (SolidColorBrush)GetValue(ProgressColorProperty); }
            set { SetValue(ProgressColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ProgressColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProgressColorProperty =
            DependencyProperty.Register("ProgressColor", typeof(SolidColorBrush), typeof(CustomProgressBar), 
                new PropertyMetadata((SolidColorBrush)new BrushConverter().ConvertFromString("#0DCCFE")));

        #endregion

        static CustomProgressBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomProgressBar), new FrameworkPropertyMetadata(typeof(CustomProgressBar)));
        }
    }
}
