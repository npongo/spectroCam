using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;

namespace spectra.camera.Converters
{

    public sealed class BoolToImageConverter : DependencyObject, IValueConverter
    {
        public static readonly DependencyProperty TrueImageProperty =
        DependencyProperty.Register("TrueImage", typeof(BitmapImage),
        typeof(BoolToImageConverter), null);

        public static readonly DependencyProperty FalseImageProperty =
        DependencyProperty.Register("FalseImage", typeof(BitmapImage),
        typeof(BoolToImageConverter), null);

        public BitmapImage TrueImage
        {

            get { return (BitmapImage)GetValue(TrueImageProperty); }
            set { SetValue(TrueImageProperty, value); }
        }


        public BitmapImage FalseImage
        {
            get { return (BitmapImage)GetValue(FalseImageProperty); }
            set { SetValue(FalseImageProperty, value); }
        }


        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (bool)value ? TrueImage : FalseImage;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return new NotImplementedException();
        }
    }


    public sealed class BoolToImageUriConverter : DependencyObject, IValueConverter
    {
        public static readonly DependencyProperty TrueImageProperty =
        DependencyProperty.Register("TrueImage", typeof(Uri),
        typeof(BoolToImageUriConverter), null);

        public static readonly DependencyProperty FalseImageProperty =
        DependencyProperty.Register("FalseImage", typeof(Uri),
        typeof(BoolToImageUriConverter), null);

        public Uri TrueImage
        {

            get { return (Uri)GetValue(TrueImageProperty); }
            set { SetValue(TrueImageProperty, value); }
        }


        public Uri FalseImage
        {
            get { return (Uri)GetValue(FalseImageProperty); }
            set { SetValue(FalseImageProperty, value); }
        }


        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (bool)value ? TrueImage : FalseImage;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return new NotImplementedException();
        }
    }


}
