using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using System;
using System.Windows;

namespace NetworkService.Helpers
{
    public class LastMeasureToBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int lastMeasure = (int)value;
          
                if (lastMeasure > 5 || lastMeasure < 1)
                {
                    return Brushes.IndianRed;
                }
                else
                {
                    return Brushes.SkyBlue;
                }
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}