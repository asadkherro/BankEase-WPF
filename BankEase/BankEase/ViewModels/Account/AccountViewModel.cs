using BankEase.Core;
using BankEase.Services.Interfaces;
using BankEase.Views.AccountViews;

namespace BankEase.ViewModels.Account
{
    public class AccountViewModel : BaseViewModel
    {

        public AccountViewModel(IWindowNavigation nav) : base(nav)
        {
            CurrentView = DisplayView<ModifyTypeView>();
        }
        public RelayCommand ModifyTypeCommand => new RelayCommand(param => CurrentView = DisplayView<ModifyTypeView>());
        public RelayCommand ServiceCommand => new RelayCommand(param => CurrentView = DisplayView<ServicesView>());
        public RelayCommand ModifyUsernameCommand => new RelayCommand(param => CurrentView = DisplayView<ChangeUsernameView>());
        public RelayCommand ModifyPasswordCommand => new RelayCommand(param => CurrentView = DisplayView<ChangePasswordView>());
        public RelayCommand CloseAccountCommand => new RelayCommand(param => CurrentView = DisplayView<CloseAccountView>());
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChange(); }
        }
        private object _currentView;
    }
}
