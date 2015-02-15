using System;
using System.Windows;
using System.Windows.Controls;

namespace Com.Ericmas001.Wpf.CustomControls
{
    /// <summary>
    /// Logique d'interaction pour RangeSlider.xaml
    /// </summary>
    public partial class RangeSlider : UserControl
    {
        public RangeSlider()
        {
            this.InitializeComponent();
            this.LayoutUpdated += new EventHandler(RangeSlider_LayoutUpdated);
        }

        void RangeSlider_LayoutUpdated(object sender, EventArgs e)
        {
            SetProgressBorder();
            SetLowerValueVisibility();
        }

        private void SetProgressBorder()
        {
            double lowerPoint = (this.ActualWidth * (LowerValue - Minimum)) / (Maximum - Minimum);
            double upperPoint = (this.ActualWidth * (UpperValue - Minimum)) / (Maximum - Minimum);
            upperPoint = this.ActualWidth - upperPoint;
            progressBorder.Margin = new Thickness(lowerPoint, 0, upperPoint, 0);
        }

        public void SetLowerValueVisibility()
        {
            if (DisableLowerValue)
            {
                LowerSlider.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                LowerSlider.Visibility = System.Windows.Visibility.Visible;
            }
        }

        public double TickFrequency
        {
            get { return (double)GetValue(TickFrequencyProperty); }
            set { SetValue(TickFrequencyProperty, value); }
        }
        public double Minimum
        {
            get { return (double)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }

        public double Maximum
        {
            get { return (double)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        public double LowerValue
        {
            get { return (double)GetValue(LowerValueProperty); }
            set { SetValue(LowerValueProperty, value); }
        }

        public double UpperValue
        {
            get { return (double)GetValue(UpperValueProperty); }
            set { SetValue(UpperValueProperty, value); }
        }

        public bool DisableLowerValue
        {
            get { return (bool)GetValue(DisableLowerValueProperty); }
            set { SetValue(DisableLowerValueProperty, value); }
        }

        public bool IsSnapToTickEnabled
        {
            get { return (bool)GetValue(IsSnapToTickEnabledProperty); }
            set { SetValue(IsSnapToTickEnabledProperty, value); }
        }

        public static readonly DependencyProperty TickFrequencyProperty = DependencyProperty.Register("TickFrequency", typeof(double), typeof(RangeSlider), new UIPropertyMetadata(0d, new PropertyChangedCallback(PropertyChanged)));
        public static readonly DependencyProperty MinimumProperty = DependencyProperty.Register("Minimum", typeof(double), typeof(RangeSlider), new UIPropertyMetadata(0d, new PropertyChangedCallback(PropertyChanged)));
        public static readonly DependencyProperty LowerValueProperty = DependencyProperty.Register("LowerValue", typeof(double), typeof(RangeSlider), new UIPropertyMetadata(10d, new PropertyChangedCallback(PropertyChanged)));
        public static readonly DependencyProperty UpperValueProperty = DependencyProperty.Register("UpperValue", typeof(double), typeof(RangeSlider), new UIPropertyMetadata(90d, new PropertyChangedCallback(PropertyChanged)));
        public static readonly DependencyProperty MaximumProperty = DependencyProperty.Register("Maximum", typeof(double), typeof(RangeSlider), new UIPropertyMetadata(100d, new PropertyChangedCallback(PropertyChanged)));
        public static readonly DependencyProperty DisableLowerValueProperty = DependencyProperty.Register("DisableLowerValue", typeof(bool), typeof(RangeSlider), new UIPropertyMetadata(false, new PropertyChangedCallback(DisabledLowerValueChanged)));
        public static readonly DependencyProperty IsSnapToTickEnabledProperty = DependencyProperty.Register("IsSnapToTickEnabled", typeof(bool), typeof(RangeSlider), new UIPropertyMetadata(false, new PropertyChangedCallback(PropertyChanged)));

        private static void DisabledLowerValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RangeSlider slider = (RangeSlider)d;
            slider.SetLowerValueVisibility();
        }

        private static void PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RangeSlider slider = (RangeSlider)d;
            if (e.Property == RangeSlider.LowerValueProperty)
            {
                slider.UpperSlider.Value = Math.Max(slider.UpperSlider.Value, slider.LowerSlider.Value);
            }
            else if (e.Property == RangeSlider.UpperValueProperty)
            {
                slider.LowerSlider.Value = Math.Min(slider.UpperSlider.Value, slider.LowerSlider.Value);
            }
            slider.SetProgressBorder();
        }
    }
}
