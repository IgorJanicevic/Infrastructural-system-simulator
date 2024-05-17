using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace NetworkService.Helpers
{
    public class DropEventArgsConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return new Tuple<DragEventArgs, object>((DragEventArgs)values[0], values[1]);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
