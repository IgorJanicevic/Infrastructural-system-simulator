using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using System;

public class LastMeasureToBackgroundConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is double lastMeasure)
        {
            if (lastMeasure > 5 || lastMeasure < 1)
            {
                return Brushes.Red;
            }
            else
            {
                return Brushes.SkyBlue;
            }
        }
        return Brushes.Transparent;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
