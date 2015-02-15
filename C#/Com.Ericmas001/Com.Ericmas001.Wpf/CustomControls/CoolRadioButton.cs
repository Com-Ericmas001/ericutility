using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Com.Ericmas001.Wpf.CustomControls
{
    public class CoolRadioButton : RadioButton
    {
        public static readonly DependencyProperty ImageSourceProperty =
           DependencyProperty.Register("ImageSource",
                                       typeof(BitmapImage),
                                       typeof(CoolRadioButton),
                                       new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty ImageSizeProperty =
           DependencyProperty.Register("ImageSize",
                                       typeof(int),
                                       typeof(CoolRadioButton),
                                       new FrameworkPropertyMetadata(0));

        public BitmapImage ImageSource
        {
            get { return (BitmapImage)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        public int ImageSize
        {
            get { return (int)GetValue(ImageSizeProperty); }
            set { SetValue(ImageSizeProperty, value); }
        }

        static CoolRadioButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CoolRadioButton), new FrameworkPropertyMetadata(typeof(CoolRadioButton)));
        }
    }

}
