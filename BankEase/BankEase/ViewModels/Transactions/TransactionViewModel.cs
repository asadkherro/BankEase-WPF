using BankEase.Core;
using BankEase.Services.Interfaces;
using BankEase.Views;

namespace BankEase.ViewModels.Transactions
{
    public class TransactionViewModel : BaseViewModel
    {
        public TransactionViewModel(IWindowNavigation nav) : base(nav)
        {
            CurrentView = DisplayView<WithdrawView>();
        }
        public RelayCommand WithdrawCommand => new RelayCommand(param => CurrentView = DisplayView<WithdrawView>());
        public RelayCommand DespositCommand => new RelayCommand(param => CurrentView = DisplayView<DepositView>());
        public RelayCommand FundsTransferCommand => new RelayCommand(param => CurrentView = DisplayView<FundsTransferView>());
        public RelayCommand HistoryCommand => new RelayCommand(param => CurrentView = DisplayView<HistoryView>());
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChange(); }
        }
        private object _currentView;
    }
}
