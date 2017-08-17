using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using System.Windows;


namespace spectra.camera.Converters
{
    public  class NullToVisiblityConverter : IValueConverter
    {
        #region IValueConverter Members

        Type type = null;

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value == null ? Visibility.Collapsed : Visibility.Visible;
        } 

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
        #endregion
    }


    public class BoolToVisiblity2Converter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (bool)value ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return (Visibility)value == Visibility.Collapsed;
        }
        #endregion
    }


    public class BoolToVisiblityConverter : IValueConverter
    {
        #region IValueConverter Members
        
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (bool)value ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return (Visibility)value == Visibility.Visible;
        }
        #endregion
    }


    public class VisiblityToBoolConverter : IValueConverter
    {
        #region IValueConverter Members

        Type type = null;

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (Visibility)value == Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
             
            return (bool)value ? Visibility.Collapsed : Visibility.Visible;
        }
        #endregion
    }


    public class InverseVisiblityToBoolConverter : IValueConverter
    {
        #region IValueConverter Members

        Type type = null;

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (Visibility)value == Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {

            return (bool)value ? Visibility.Visible : Visibility.Collapsed;
        }
        #endregion
    }
}
