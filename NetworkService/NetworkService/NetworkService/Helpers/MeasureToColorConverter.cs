using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace NetworkService.Helpers
{
    public class MeasureToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
                return Binding.DoNothing;

            double measure = System.Convert.ToDouble(value);
            double threshold = System.Convert.ToDouble(parameter);

            if (measure > threshold)
                return Brushes.Red;
            else
                return Brushes.Blue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
