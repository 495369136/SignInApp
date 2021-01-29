using System;
using System.Windows;
using System.Windows.Controls;

namespace SignInApp.Controls
{
    class TextButton : Button
    {
        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.SetValue(e.Property, e.NewValue);
        }

        /// <summary>
        /// 按钮圆角
        /// </summary>
        public CornerRadius CornerRadiusText
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        public static readonly DependencyProperty CornerRadiusProperty
            = DependencyProperty.Register("CornerRadiusText"
                                         , typeof(CornerRadius)
                                         , typeof(ImageButton)
                                         , new PropertyMetadata(new CornerRadius(0), OnPropertyChanged));
    }
}
