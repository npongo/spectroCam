using spectra.camera.Models;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace spectra.camera.Converters
{
    public class EnumBooleanConverter : IValueConverter
    {
        #region IValueConverter Members

        Type type = null;

        public object Convert(object value, Type targetType, object parameter, string language)
        {

            string parameterString = parameter as string;
            if (parameterString == null | value == null)
                return DependencyProperty.UnsetValue;

            if (Enum.IsDefined(value.GetType(), value) == false)
                return DependencyProperty.UnsetValue;

            object parameterValue = Enum.Parse(value.GetType(), parameterString);

         
            type = value.GetType();

            return parameterValue.Equals(value);
        }
         

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            try
            {
                string parameterString = parameter as string;
                if (parameterString == null)
                    return DependencyProperty.UnsetValue;

                if (type != null)
                    return Enum.Parse(type, parameterString);
                else
                    return parameterString;
            }
            catch(Exception ex)
            {
                string m = ex.Message;
                return null;
            }
        }
        #endregion
    }


    //public class EnumBooleanConverter2 : IValueConverter
    //{
    //    #region IValueConverter Members
         
    //    Dictionary<string, Type> enums = new Dictionary<string, Type>();

    //    public EnumBooleanConverter2()
    //    {
    //        enums.Add("observationType", typeof(observationType));
    //        enums.Add("M3", typeof(m3)); 
    //        enums.Add("M7", typeof(m7)); 
    //        enums.Add("M8", typeof(m8)); 
    //        enums.Add("M10", typeof(m10));
    //        enums.Add("M11", typeof(m11)); 
    //        enums.Add("D4", typeof(d4));
    //        enums.Add("P3", typeof(p3)); 
    //        enums.Add("P4", typeof(p4)); 
    //        enums.Add("D5", typeof(d5));
    //        enums.Add("D6", typeof(d6)); 
    //        enums.Add("D7", typeof(d7)); 
    //        enums.Add("D8", typeof(d8));
    //        enums.Add("P5", typeof(p5)); 
    //        enums.Add("P6", typeof(p6)); 
    //        enums.Add("P8", typeof(p8)); 
    //        enums.Add("P9", typeof(p9)); 
    //        enums.Add("P11A", typeof(p11)); 
    //        enums.Add("P14A", typeof(p14)); 
    //        enums.Add("P17", typeof(p17));
    //        enums.Add("X4A", typeof(x4a));  
    //        enums.Add("X6", typeof(x6));
    //        enums.Add("DV3", typeof(dv3));
    //        enums.Add("DV4", typeof(dv4)); 
    //        //enums.Add("DV5", typeof(dv5)); 
    //        enums.Add("DV6", typeof(dv6)); 
    //        enums.Add("DV7", typeof(dv7)); 
    //        enums.Add("DV8", typeof(dv8));

    //        //enums.Add("B4", typeof(b4)); 
    //        enums.Add("B6", typeof(b6)); 
    //        enums.Add("B7", typeof(b7)); 
    //        enums.Add("B8", typeof(b8)); 
    //        enums.Add("S4", typeof(s4)); 
    //        enums.Add("K4", typeof(k4)); 
    //        enums.Add("K5", typeof(k5)); 
    //        enums.Add("K8", typeof(k5)); 
    //        enums.Add("K9", typeof(k9)); 
    //        enums.Add("K10", typeof(k10)); 
    //        enums.Add("K11", typeof(k11));  
    //        enums.Add("K13", typeof(k13)); 
    //        enums.Add("K15", typeof(k15)); 
    //        enums.Add("K20", typeof(k20));  
    //        enums.Add("K22", typeof(k22)); 
    //        enums.Add("K24", typeof(k24)); 
    //        enums.Add("K25", typeof(k25));
    //        enums.Add("K26", typeof(k26)); 
    //        enums.Add("K29", typeof(k29));  
    //        enums.Add("K39", typeof(k39));  
    //        enums.Add("K45", typeof(k45)); 
    //        enums.Add("C2", typeof(c2));  
    //        enums.Add("C6", typeof(c6)); 
    //        enums.Add("C7", typeof(c7)); 
    //        enums.Add("C8", typeof(c8));  
    //        enums.Add("A2", typeof(a2)); 
    //        enums.Add("A3", typeof(a3)); 
    //        enums.Add("A4", typeof(a4)); 
    //        enums.Add("A5", typeof(a5)); 
    //        enums.Add("A7", typeof(a7)); 
    //        enums.Add("A8", typeof(a8)); 
    //        enums.Add("A11", typeof(a11));
    //        enums.Add("I3", typeof(i3)); 
    //        enums.Add("I5A", typeof(i5a)); 
    //        enums.Add("I9", typeof(i9)); 
    //        enums.Add("I12", typeof(i12)); 
    //        enums.Add("I14", typeof(i14));  
    //        enums.Add("I15", typeof(i15)); 
    //        enums.Add("I16", typeof(i16)); 
    //        enums.Add("I17", typeof(i17)); 
    //        enums.Add("I18", typeof(i18)); 
    //        enums.Add("I19", typeof(i19)); 
    //        enums.Add("I20", typeof(i20)); 
    //        enums.Add("I21", typeof(i21)); 
    //        enums.Add("I22", typeof(i22)); 
    //        enums.Add("I23", typeof(i23)); 
    //        enums.Add("I24", typeof(i24)); 
    //        enums.Add("I25", typeof(i25)); 
    //        enums.Add("I27", typeof(i27)); 
    //        enums.Add("I27C", typeof(i27c)); 
    //        enums.Add("I28A", typeof(i28a)); 
    //        enums.Add("I29", typeof(i29)); 
    //        enums.Add("I31", typeof(i31)); 
    //        enums.Add("I31C", typeof(i31c)); 
    //        enums.Add("I32A", typeof(i32a)); 
    //        enums.Add("I33", typeof(i33)); 
    //        enums.Add("I34", typeof(i34)); 
    //        enums.Add("I36B", typeof(i36b)); 
    //        enums.Add("I36C", typeof(i36c)); 
    //        enums.Add("I40", typeof(i40)); 
    //        enums.Add("I41", typeof(i41));
    //        enums.Add("L2", typeof(l2)); 
    //        enums.Add("L4A", typeof(l4a));
    //        enums.Add("L4B", typeof(l4b)); 
    //        enums.Add("L9", typeof(l9)); 
    //        enums.Add("V4", typeof(v4)); 
    //        enums.Add("V5", typeof(v5)); 
    //        enums.Add("V7", typeof(v7));  
    //        enums.Add("V8", typeof(v8)); 
    //        enums.Add("V14", typeof(yesNo)); 
    //        enums.Add("R3", typeof(status));
    //        enums.Add("R4", typeof(status));
    //        enums.Add("R5", typeof(r5)); 
    //        enums.Add("R6", typeof(r6)); 
    //        enums.Add("R7", typeof(r7));   
    //        enums.Add("R9", typeof(r9)); 
    //        enums.Add("R10", typeof(r10));  
    //        enums.Add("R11", typeof(education));
    //        enums.Add("EDUCATION", typeof(education));
    //        enums.Add("R12", typeof(occupation));
    //        enums.Add("OCCUPATION", typeof(occupation));
    //        enums.Add("SELECT", typeof(yesNo)); 
    //        enums.Add("R13", typeof(yesNo));  
    //        enums.Add("R17", typeof(r17));
    //        enums.Add("MaritalStatus", typeof(maritalStatus));
    //        enums.Add("yesNo", typeof(yesNo)); 
    //        enums.Add("gender", typeof(gender));
    //        enums.Add("GENDER", typeof(gender));
    //        enums.Add("R1", typeof(r1));
    //        enums.Add("locationStatus", typeof(locationStatus));
    //    }

    //    public object Convert(object value, Type targetType, object parameter, string language)
    //    {
    //        try
    //        {
    //             string parameterString = parameter as string;
    //            if (parameterString == null | value == null)
    //                return DependencyProperty.UnsetValue;

    //            //if (Enum.IsDefined(value.GetType(), value) == false)
    //            //    return DependencyProperty.UnsetValue;

    //            //object parameterValue = Enum.Parse(value.GetType(), parameterString);

    //            //return parameterValue.Equals(value);

    //            var type = enums[language];
    //            if (type != null)
    //                return (int)value == (int)Enum.Parse(type, parameterString, true);
    //            else
    //                return value;
    //        }
    //        catch (Exception ex)
    //        {
    //            string m = ex.Message;
    //            return null;
    //        }
    //    }


    //    public object ConvertBack(object value, Type targetType, object parameter, string language)
    //    {
    //        if (!(bool)value)
    //            return DependencyProperty.UnsetValue;
    //        try
    //        {
    //            string parameterString = parameter as string;
    //            if (parameterString == null)
    //                return DependencyProperty.UnsetValue;

    //            var type = enums[language];

    //            if(type != null)
    //                return (int)Enum.Parse(type, parameterString,true); 
    //            else
    //                return parameterString;
    //        }
    //        catch (Exception ex)
    //        {
    //            string m = ex.Message;
    //            return null;
    //        }
    //    }
    //    #endregion
    //}
}
