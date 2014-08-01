using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace zold.TimeBuzzer.Frontend.Converter
{
    public class InvertBooleanToVisibilityConverter : IValueConverter
    {
        private BooleanToVisibilityConverter _booleanToVisibilityConverter;

        public InvertBooleanToVisibilityConverter()
        {
            _booleanToVisibilityConverter = new BooleanToVisibilityConverter();
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Visibility result =(Visibility)(_booleanToVisibilityConverter.Convert(value, targetType, parameter, culture));

            return result == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Visibility result = (Visibility)(_booleanToVisibilityConverter.ConvertBack(value, targetType, parameter, culture));

            return result == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;
        }
    }
}
