using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Data;

namespace Okazuki.TwitterSearch.Client
{
    public class BooleanToVisibilityConverter : IValueConverter
    {

        #region IValueConverter メンバー

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                var b = System.Convert.ToBoolean(value);
                return b ? Visibility.Visible : Visibility.Collapsed;
            }
            catch
            {
                throw new ArgumentException();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
