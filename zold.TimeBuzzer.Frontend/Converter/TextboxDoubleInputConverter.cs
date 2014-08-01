using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace zold.TimeBuzzer.Frontend.Converter
{
    /// <summary>
    /// Class ignores the decimal separator point or comma. It converts the entered sign in the culture configured separator.
    /// </summary>
    public class TextboxDoubleInputConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ConvertToDecimalSeparator(value, culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ConvertToDecimalSeparator(value, culture);
        }

        private string ConvertToDecimalSeparator(object value, System.Globalization.CultureInfo culture)
        {
            string inputValue = value.ToString();

            if (inputValue.Contains(culture.NumberFormat.NumberDecimalSeparator))
                return inputValue;

            if (culture.NumberFormat.NumberDecimalSeparator.Equals("."))
                return inputValue.Replace(",", culture.NumberFormat.NumberDecimalSeparator);

            if (culture.NumberFormat.NumberDecimalSeparator.Equals(","))
                return inputValue.Replace(".", culture.NumberFormat.NumberDecimalSeparator);

            return inputValue;
        }
    }
}
