using System.Globalization;

namespace NationalParks.Converters
{
    public class BoolToTrueFalseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isTrue = (bool)value;

            //return isTrue;
            return isTrue ? "True" : "False";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var strVal = value.ToString();

            return (strVal == "True");
            //return (strVal == "True");
        }
    }
}
