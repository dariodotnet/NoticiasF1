using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace NoticiasF1.Converters
{
    public class BooleanToVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var result = false;
            if (value != null)
            {
                result = (bool)value;
            }
            return result ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}