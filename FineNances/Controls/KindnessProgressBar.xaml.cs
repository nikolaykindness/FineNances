using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace FineNances.Controls
{
    /// <summary>
    /// Interaction logic for KindnessProgressBar.xaml
    /// </summary>
    public partial class KindnessProgressBar : UserControl
    {
        public KindnessProgressBar()
        {
            InitializeComponent();
        }

        //static KindnessProgressBar()
        //{
        //    DefaultStyleKeyProperty.OverrideMetadata(typeof(KindnessProgressBar), new FrameworkPropertyMetadata(typeof(KindnessProgressBar)));
        //}

        public static readonly DependencyProperty IndicatorBrushProperty = 
            DependencyProperty.Register("IndicatorBrush", typeof(Brush), typeof(KindnessProgressBar));
        public static readonly DependencyProperty BackgroundBrushProperty =
            DependencyProperty.Register("BackgroundBrush", typeof(Brush), typeof(KindnessProgressBar));
        public static readonly DependencyProperty ArcThicknessProperty =
            DependencyProperty.Register("ArcThickness", typeof(int), typeof(KindnessProgressBar));
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(int), typeof(KindnessProgressBar));


        public Brush IndicatorBrush
        {
            get => (Brush)GetValue(IndicatorBrushProperty);
            set => SetValue(IndicatorBrushProperty, value); 
        }

        public Brush BackgroundBrush
        {
            get => (Brush)GetValue(BackgroundBrushProperty);
            set => SetValue(BackgroundBrushProperty, value);
        }

        public int ArcThickness
        {
            get => (int)GetValue(ArcThicknessProperty);
            set => SetValue(ArcThicknessProperty, value);
        }

        public int Value
        {
            get => (int)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }
    }

    [ValueConversion(typeof(int), typeof(double))]
    public class ValueToAngleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double)(((int)value * 0.01) * 360);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)((double)value * 360) * 100;
        }
    }
}
