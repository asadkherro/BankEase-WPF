using System.Windows;

namespace BankEase.Helper
{
    public static class ShowDialog
    {
        public static void Success(string? message = null, string? caption = null) 
        {
            MessageBox.Show(
                !string.IsNullOrEmpty(message) ? message : "Success",
                !string.IsNullOrEmpty(caption) ? caption : "Operation Succeeded",
                MessageBoxButton.OK,
                MessageBoxImage.Information,
                MessageBoxResult.OK);
        }
        public static void Error(string? message = null, string? caption = null)
        {
            MessageBox.Show(
                !string.IsNullOrEmpty(message) ? message : "A problem occured",
                !string.IsNullOrEmpty(caption) ? caption : "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error,
                MessageBoxResult.OK);
        }
        public static void Info(string? message = null, string? caption = null)
        {
            MessageBox.Show(
                !string.IsNullOrEmpty(message) ? message : "Success",
                !string.IsNullOrEmpty(caption) ? caption : "Operation Succeeded",
                MessageBoxButton.OK,
                MessageBoxImage.Exclamation,
                MessageBoxResult.OK);
        }
    }
}
