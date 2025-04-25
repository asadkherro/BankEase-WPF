using BankEase.Models;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace BankEase.Helper.ValueConverter
{
    public class TransactionTypeToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TransactionType transactionType)
            {
                return transactionType switch
                {
                    TransactionType.Deposit => new SolidColorBrush(Color.FromRgb(132, 78, 255)), // #844eff
                    TransactionType.Withdrawl => new SolidColorBrush(Color.FromRgb(78, 153, 254)), // #4E99FE
                    TransactionType.FundsSent => new SolidColorBrush(Color.FromRgb(78, 255, 145)), // #4EFF91
                    TransactionType.FundsReceive => new SolidColorBrush(Color.FromRgb(236, 255, 78)), // #ECFF4E 
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
