using BankEase.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
namespace BankEase.Services
{
    public class WindowNavigation : IWindowNavigation
    {
        private readonly IServiceProvider _serviceProvider;
        public WindowNavigation(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public void NavigateTo<T>() where T : Window
        {
            var window = _serviceProvider.GetRequiredService<T>();
            window.Show();
            Application.Current.MainWindow = window;
        }
        public T DisplayView<T>() where T : class
        {
            return _serviceProvider.GetRequiredService<T>();
        }
        public void CloseWindow<T>() where T : Window
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window is T)
                {
                    window.Close();
                    break;
                }
            }
        }
    }
}
