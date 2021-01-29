using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SignInApp.Controls
{
    public class ImageButton : Button
    {
        //static ImageButton()
        //{
        //    DefaultStyleKeyProperty.OverrideMetadata(typeof(ImageButton), new FrameworkPropertyMetadata(typeof(ImageButton)));
        //}

        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.SetValue(e.Property, e.NewValue);
        }

        private static void ImageSourceChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            Application.GetResourceStream(new Uri("pack://application:,,," + (string)e.NewValue));
        }

        /// <summary>
        /// 按钮圆角
        /// </summary>
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        public static readonly DependencyProperty CornerRadiusProperty
            = DependencyProperty.Register("CornerRadius"
                                         , typeof(CornerRadius)
                                         , typeof(ImageButton)
                                         , new PropertyMetadata(new CornerRadius(0), OnPropertyChanged));

        /// <summary>
        /// 按钮图片
        /// </summary>
        public ImageSource ImagePath
        {
            get { return (ImageSource)GetValue(ImagePathProperty); }
            set { SetValue(ImagePathProperty, value); }
        }
        public static readonly DependencyProperty ImagePathProperty
            = DependencyProperty.Register("ImagePath"
                                         , typeof(ImageSource)
                                         , typeof(ImageButton)
                                         , new PropertyMetadata(null));
    }
}
