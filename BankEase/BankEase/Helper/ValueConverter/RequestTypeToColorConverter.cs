using BankEase.Models;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace BankEase.Helper.ValueConverter
{
    public class RequestTypeToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is RequestType transactionType)
            {
                return transactionType switch
                {
                    RequestType.AccountClose => new SolidColorBrush(Color.FromRgb(132, 78, 255)), // #844eff
                    RequestType.AccountUpgrade => new SolidColorBrush(Color.FromRgb(78, 153, 254)), // #4EFF91
                    _ => Brushes.Gray,
                };
            }
            return Brushes.Gray;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
