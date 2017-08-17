using System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace spectra.camera.Converters
{ 

    public sealed class BoolToTextConverter : DependencyObject, IValueConverter
    {
        public static readonly DependencyProperty TrueTextProperty =
        DependencyProperty.Register("TrueText", typeof(string),
        typeof(BoolToTextConverter), new PropertyMetadata("Disconnect"));

        public static readonly DependencyProperty FalseTextProperty =
        DependencyProperty.Register("FalseText", typeof(string),
        typeof(BoolToTextConverter), new PropertyMetadata("Connect"));

        public string TrueText
        {
            get { return (string)GetValue(TrueTextProperty); }
            set { SetValue(TrueTextProperty, value); }
        }


        public string FalseText
        {
            get { return (string)GetValue(FalseTextProperty); }
            set { SetValue(FalseTextProperty, value); }
        }



        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (bool)value ? TrueText : FalseText;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return new NotImplementedException();
        }
    }

    public sealed class BoolToSymbolConverter : DependencyObject, IValueConverter
    {
        public static readonly DependencyProperty TrueSymbolProperty =
        DependencyProperty.Register("TrueSymbol", typeof(Symbol),
        typeof(BoolToSymbolConverter), new PropertyMetadata(Symbol.Add));

        public static readonly DependencyProperty FalseSymbolProperty =
        DependencyProperty.Register("FalseSymbol", typeof(Symbol),
        typeof(BoolToSymbolConverter), new PropertyMetadata(Symbol.Remove));

        public Symbol TrueSymbol
        {
            get { return (Symbol)GetValue(TrueSymbolProperty); }
            set { SetValue(TrueSymbolProperty, value); }
        }


        public Symbol FalseSymbol
        {
            get { return (Symbol)GetValue(FalseSymbolProperty); }
            set { SetValue(FalseSymbolProperty, value); }
        }



        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (bool)value ? TrueSymbol : FalseSymbol;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return new NotImplementedException();
        }
    }

     
    public sealed class BoolToColorConverter : DependencyObject, IValueConverter
    { 
        public static readonly DependencyProperty TrueColorProperty =
        DependencyProperty.Register("TrueColor", typeof(Color),
        typeof(BoolToColorConverter), new PropertyMetadata(Colors.Gray));

        public static readonly DependencyProperty FalseColorProperty =
        DependencyProperty.Register("FalseColor", typeof(Color),
        typeof(BoolToColorConverter), new PropertyMetadata(Colors.Transparent));

        public Color TrueColor
        {
            get { return (Color)GetValue(TrueColorProperty); }
            set { SetValue(TrueColorProperty, value); }
        }


        public Color FalseColor
        {
            get { return (Color)GetValue(FalseColorProperty); }
            set { SetValue(FalseColorProperty, value); }
        }



        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (bool)value ? TrueColor : FalseColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return new NotImplementedException();
        }
    }

    public sealed class BoolToBrushConverter :DependencyObject, IValueConverter
    {


        public static readonly DependencyProperty TrueColorProperty =
        DependencyProperty.Register("TrueColor", typeof(Color),
        typeof(BoolToColorConverter), new PropertyMetadata(Colors.Gray));

        public static readonly DependencyProperty FalseColorProperty =
        DependencyProperty.Register("FalseColor", typeof(Color),
        typeof(BoolToColorConverter), new PropertyMetadata(Colors.Transparent));
         
        public Color TrueColor
        {
            get { return (Color)GetValue(TrueColorProperty); }
            set { SetValue(TrueColorProperty, value); }
        }


        public Color FalseColor
        {
            get { return (Color)GetValue(FalseColorProperty); }
            set { SetValue(FalseColorProperty, value); }
        }

         

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (bool)value ? new SolidColorBrush(TrueColor) : new SolidColorBrush(FalseColor);
        }

        public object ConvertBack(object value, Type targetType, object parameter,string language)
        {
            return new NotImplementedException();
        }
    }
     
    public sealed class BoolToBrushConverter2 : DependencyObject, IValueConverter
    {


        public static readonly DependencyProperty TrueBrushProperty =
        DependencyProperty.Register("TrueBrush", typeof(Brush),
        typeof(BoolToBrushConverter2), new PropertyMetadata(new SolidColorBrush(Colors.Gray)));

        public static readonly DependencyProperty FalseColorProperty =
        DependencyProperty.Register("FalseBrush", typeof(Brush),
        typeof(BoolToBrushConverter2), new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));

        public Brush TrueBrush
        {
            get { return (Brush)GetValue(TrueBrushProperty); }
            set { SetValue(TrueBrushProperty, value); }
        } 

        public Brush FalseBrush
        {
            get { return (Brush)GetValue(FalseColorProperty); }
            set { SetValue(FalseColorProperty, value); }
        }
         

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (bool)value ? TrueBrush : FalseBrush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return new NotImplementedException();
        }
    }
}
