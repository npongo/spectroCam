using System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace spectra.camera.Converters
{

    public sealed class BoolToPlusMinusConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (bool)value?'-':'+';
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public sealed class BoolToParameterConverter : IValueConverter
    { 

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (parameter == value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return (bool)value ? parameter : null;
        }
    } 
}
