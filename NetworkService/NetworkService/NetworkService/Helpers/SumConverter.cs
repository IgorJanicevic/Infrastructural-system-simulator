using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace NetworkService.Helpers
{
    public class SumConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length < 2 || values[0] == null || values[1] == null)
                return Binding.DoNothing;

            double actualHeight = (double)values[0];
            bool isKeyboardVisible = (bool)values[1];

            if (isKeyboardVisible)
            {
                return Math.Max(500, Math.Min(700, actualHeight));
            }
            else
            {
                return 0; // Visina je 0 kada tastatura nije vidljiva
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class VerticalAlignmentConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length < 2 || values[0] == null || values[1] == null)
                return Binding.DoNothing;

            double actualHeight = (double)values[0];
            bool isKeyboardVisible = (bool)values[1];

            if (isKeyboardVisible)
            {
                double windowHeight = SystemParameters.PrimaryScreenHeight;
                double controlHeight = Math.Max(400, Math.Min(600, actualHeight));
                double topMargin = windowHeight - controlHeight;

                return topMargin;
            }
            else
            {
                return VerticalAlignment.Bottom; // Vertikalno poravnanje na dno kada tastatura nije vidljiva
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
