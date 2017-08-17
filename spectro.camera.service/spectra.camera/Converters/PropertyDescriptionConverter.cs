using spectra.camera.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Windows.UI.Xaml.Data;

namespace spectra.camera.Converters
{
    public class PropertyDisplayConverter : IValueConverter
    {
        
        private string GetEnumDescription(object model, string property)
        {
            try
            {
                Type modelType = model.GetType();

                if (modelType != null)
                {
                    var propInfo = modelType.GetProperty(property);
                   
                    
                    if (propInfo == null)
                        return "";

                    DisplayAttribute attrib = propInfo.GetCustomAttributes(false).FirstOrDefault(a => a is DisplayAttribute) as DisplayAttribute;
                    
                    if (attrib == null)
                    {
                        return property;
                    }
                    else
                    {
                        return attrib.Description;
                    }
                }
                return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            try
            {
                if (value == null)
                    return "";
                 
                string description = GetEnumDescription(value, parameter.ToString());
                return description;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            try
            {
                return string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
