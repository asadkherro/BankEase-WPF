using BankEase.Core;
using BankEase.Services.Interfaces;

namespace BankEase.ViewModels
{
    public abstract class BaseViewModel : ObservableObject 
    {
        protected readonly IWindowNavigation NavigationService;
        public BaseViewModel(IWindowNavigation navigationService)
        {
            NavigationService = navigationService;
        }
        protected T DisplayView<T>() where T : class
        {
            return NavigationService.DisplayView<T>();
        }

    }
}
