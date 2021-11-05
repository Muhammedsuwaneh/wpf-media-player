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
using System.Windows.Threading;

namespace MediaPlayer.Vinyl
{
    public class Vinyl : Control
    {

        #region Vinyl Properties

        private Storyboard VinylBoard;

        public double VinylWidth
        {
            get { return (double)GetValue(VinylWidthProperty); }
            set { SetValue(VinylWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for VinylWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VinylWidthProperty =
            DependencyProperty.Register("VinylWidth", typeof(double), typeof(Vinyl), new PropertyMetadata(300.0));


        public double VinylHeight
        {
            get { return (double)GetValue(VinylHeightProperty); }
            set { SetValue(VinylHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for VinylHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VinylHeightProperty =
            DependencyProperty.Register("VinylHeight", typeof(double), typeof(Vinyl), new PropertyMetadata(300.0));


        public Brush VinylStroke
        {
            get { return (Brush)GetValue(VinylStrokeProperty); }
            set { SetValue(VinylStrokeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Color VinylStroke.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VinylStrokeProperty =
            DependencyProperty.Register("VinylStroke", typeof(Brush), typeof(Vinyl), 
                new PropertyMetadata((SolidColorBrush)new BrushConverter().ConvertFromString("#67AABB")));




        public Brush VinylFill
        {
            get { return (Brush)GetValue(VinylFillProperty); }
            set { SetValue(VinylFillProperty, value); }
        }

        // Using a DependencyProperty as the backing store for VinylFill.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VinylFillProperty =
            DependencyProperty.Register("VinylFill", typeof(Brush), typeof(Vinyl), new PropertyMetadata(Brushes.Red));


        public double VinylStrokeThickness
        {
            get { return (double)GetValue(VinylStrokeThicknessProperty); }
            set { SetValue(VinylStrokeThicknessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for VinylStrokeThickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VinylStrokeThicknessProperty =
            DependencyProperty.Register("VinylStrokeThickness", typeof(double), typeof(Vinyl), new PropertyMetadata(100.0));


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

        public override void OnApplyTemplate()
        {
            VinylBoard = Template.FindName("PART_VinylStoryBoard", this) as Storyboard;

            base.OnApplyTemplate();
        }
    }
}
