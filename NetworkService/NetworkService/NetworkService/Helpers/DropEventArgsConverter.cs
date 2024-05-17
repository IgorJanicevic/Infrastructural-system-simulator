using NetworkService.Model;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace NetworkService.Helpers
{
    public class DropEventArgsConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var dragEventArgs = values[0] as DragEventArgs;
            var canvas = values[1] as Canvas;

            
            //Console.WriteLine(canvas.Name);
            //Console.WriteLine(dragEventArgs.ToString());

            if (dragEventArgs != null && canvas != null)
            {
                return new Tuple<DragEventArgs, Canvas>(dragEventArgs, canvas);
            }

            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
