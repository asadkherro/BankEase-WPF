using BankEase.Views;
using System.Windows;

namespace BankEase.Services.Interfaces
{
    public interface IWindowNavigation
    {
        void NavigateTo<T>() where T : Window;
        void CloseWindow<T>() where T : Window;
        T DisplayView<T>() where T : class;
    }
}
