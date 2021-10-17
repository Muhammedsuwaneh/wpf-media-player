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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MediaPlayer.Vinyl
{
    public class Vinyl : Control
    {

        #region Outer Vinyl Cover 


        public double OuterVinylWidth
        {
            get { return (double)GetValue(OuterVinylWidthProperty); }
            set { SetValue(OuterVinylWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OuterVinylWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OuterVinylWidthProperty =
            DependencyProperty.Register("OuterVinylWidth", typeof(double), typeof(Vinyl), new PropertyMetadata(300.0));


        public double OuterVinylHeight
        {
            get { return (double)GetValue(OuterVinylHeightProperty); }
            set { SetValue(OuterVinylHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OuterVinylHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OuterVinylHeightProperty =
            DependencyProperty.Register("OuterVinylHeight", typeof(double), typeof(Vinyl), new PropertyMetadata(300.0));


        public Brush OuterVinylStroke
        {
            get { return (Brush)GetValue(OuterVinylStrokeProperty); }
            set { SetValue(OuterVinylStrokeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Color OuterVinylStroke.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OuterVinylStrokeProperty =
            DependencyProperty.Register("OuterVinylStroke", typeof(Brush), typeof(Vinyl), 
                new PropertyMetadata((SolidColorBrush)new BrushConverter().ConvertFromString("#67AABB")));




        public Brush OuterVinylFill
        {
            get { return (Brush)GetValue(OuterVinylFillProperty); }
            set { SetValue(OuterVinylFillProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OuterVinylFill.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OuterVinylFillProperty =
            DependencyProperty.Register("OuterVinylFill", typeof(Brush), typeof(Vinyl), new PropertyMetadata(Brushes.Red));


        public double OuterVinylStrokeThickness
        {
            get { return (double)GetValue(OuterVinylStrokeThicknessProperty); }
            set { SetValue(OuterVinylStrokeThicknessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OuterVinylStrokeThickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OuterVinylStrokeThicknessProperty =
            DependencyProperty.Register("OuterVinylStrokeThickness", typeof(double), typeof(Vinyl), new PropertyMetadata(100.0));


        public double Diameter
        {
            get { return (double)GetValue(DiameterProperty); }
            set { SetValue(DiameterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DiameterProperty =
            DependencyProperty.Register("Diameter", typeof(double), typeof(Vinyl), new PropertyMetadata(100.0));


        #endregion



        static Vinyl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Vinyl), new FrameworkPropertyMetadata(typeof(Vinyl)));
        }
    }
}
