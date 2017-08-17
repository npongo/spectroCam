using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace spectra.camera.Converters
{
    public sealed class DateToStringConverter : DependencyObject, IValueConverter
    {
        private static readonly DependencyProperty DateFormatProperty =
       DependencyProperty.Register("DateFormat", typeof(string),
       typeof(DateToStringConverter), new PropertyMetadata("MM-YYYY"));

    

        public string DateFormat
        {
            get { return (string)GetValue(DateFormatProperty); }
            set { SetValue(DateFormatProperty, value); }
        }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
                return null;

            if (parameter == null)
                return value;

            return string.Format(new System.Globalization.DateTimeFormatInfo(),DateFormat,value);
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            string language)
        {
            throw new NotImplementedException();
        }
    }
}
