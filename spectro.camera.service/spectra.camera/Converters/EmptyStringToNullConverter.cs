using System;
using Windows.UI.Xaml.Data;

namespace spectra.camera.Converters
{
    public sealed class EmptyStringToNullConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
                return "";
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter,string language)
        {
            if (value == null)
                return null;

            if (string.IsNullOrWhiteSpace(value.ToString()))
                return null;

            return value;
        }
    }

    public sealed class EmptyStringToNullIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            try
            {
                if (value == null)
                    return "";
                return value;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        { 
            try
            {
                int d;
                if (string.IsNullOrEmpty((string)value)) return null;
                else
                {
                    if (int.TryParse((string)value, out d)) return d;
                    else return null;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public sealed class EmptyStringToNullDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            try
            {
                if (value == null)
                    return DateTimeOffset.Now.Add(new TimeSpan(1, 0, 0, 0));
                return value;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {

            ////if (!(value is DateTimeOffset))
            ////    return null;
            //else
            //{
            try
            { 
                if ((DateTimeOffset)value > DateTimeOffset.Now)
                    return null;
                else 
                    return (DateTimeOffset)value;
            }
            catch(Exception ex)
            {
                throw ex;
            }
    // }
}
    }


    public sealed class EmptyStringToNullTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            try
            { 

                if (value == null)
                    return new TimeSpan();

                double x = (double)value;
                double d = Math.Floor(x);
                return new TimeSpan((int)x, (int)((x - d) * 60), 0);
            }
            catch(Exception ex)
            {
                throw ex;
            }

    //return new TimeSpan(((DateTimeOffset)value).Hour, ((DateTimeOffset)value).Minute,((DateTimeOffset)value).Second);
}

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {

            //if ( !(value is TimeSpan))
            //    return null;
            //else
            try
            {
                return ((TimeSpan)value).TotalHours;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }


    public sealed class EmptyStringToNullDoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            try
            { 
                if (value == null)
                    return "";
                return value;

            }
            catch (Exception ex)
            {
                throw ex;
            }

            //return new TimeSpan(((DateTimeOffset)value).Hour, ((DateTimeOffset)value).Minute,((DateTimeOffset)value).Second);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            try
            {
                double d;
                if (string.IsNullOrEmpty((string)value)) return null;
                else
                {
                    if (double.TryParse((string)value, out d)) return d;
                    else return null;
                 }
                 

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }



    public class NullConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, string language)
            { return value; }

            public object ConvertBack(object value, Type targetType, object parameter, string language)
            {
                int temp;
                if (string.IsNullOrEmpty((string)value) || !int.TryParse((string)value, out temp)) return null;
                else return temp;
            }
        }
}
