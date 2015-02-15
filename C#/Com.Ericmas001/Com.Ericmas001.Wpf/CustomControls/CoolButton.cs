using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Com.Ericmas001.Wpf.CustomControls
{
    public class CoolButton : Button
    {
        public static readonly DependencyProperty ButtonColorProperty =
           DependencyProperty.Register("ButtonColor",
                                       typeof(Color),
                                       typeof(CoolButton),
                                       new FrameworkPropertyMetadata(Colors.Gray));

        public static readonly DependencyProperty ButtonImageProperty =
           DependencyProperty.Register("ButtonImage",
                                       typeof(BitmapImage),
                                       typeof(CoolButton),
                                       new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty ButtonImageSizeProperty =
           DependencyProperty.Register("ButtonImageSize",
                                       typeof(int),
                                       typeof(CoolButton),
                                       new FrameworkPropertyMetadata(0));

        public static readonly DependencyProperty TextImageOrientationProperty =
           DependencyProperty.Register("TextImageRelation",
                                       typeof(Orientation),
                                       typeof(CoolButton),
                                       new FrameworkPropertyMetadata(Orientation.Vertical));

        public static readonly DependencyProperty TextMarginProperty =
           DependencyProperty.Register("TextMargin",
                                       typeof(Thickness),
                                       typeof(CoolButton),
                                       new FrameworkPropertyMetadata(new Thickness(2)));

        public static readonly DependencyProperty ImageMarginProperty =
           DependencyProperty.Register("ImageMargin",
                                       typeof(Thickness),
                                       typeof(CoolButton),
                                       new FrameworkPropertyMetadata(new Thickness(2)));
        public Color ButtonColor
        {
            get { return (Color)GetValue(ButtonColorProperty); }
            set { SetValue(ButtonColorProperty, value); }
        }

        public BitmapImage ButtonImage
        {
            get { return (BitmapImage)GetValue(ButtonImageProperty); }
            set { SetValue(ButtonImageProperty, value); }
        }

        public int ButtonImageSize
        {
            get { return (int)GetValue(ButtonImageSizeProperty); }
            set { SetValue(ButtonImageSizeProperty, value); }
        }

        public Orientation TextImageOrientation
        {
            get { return (Orientation)GetValue(TextImageOrientationProperty); }
            set { SetValue(TextImageOrientationProperty, value); }
        }

        public Thickness TextMargin
        {
            get { return (Thickness)GetValue(TextMarginProperty); }
            set { SetValue(TextMarginProperty, value); }
        }

        public Thickness ImageMargin
        {
            get { return (Thickness)GetValue(ImageMarginProperty); }
            set { SetValue(ImageMarginProperty, value); }
        }
        static CoolButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CoolButton), new FrameworkPropertyMetadata(typeof(CoolButton)));
        }
    }
}
