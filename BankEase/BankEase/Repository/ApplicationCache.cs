using BankEase.Models.Transactions;
using BankEase.Models.User;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BankEase.Repository
{
    public class ApplicationCache : INotifyPropertyChanged
    {
        public ApplicationCache()
        {
            Transactions = new();
            AppUser = new();
        }
        public UserAccountModel AppUser { get; set; }
        public ObservableCollection<TransactionModel> Transactions { get; set; }

        public void Logout() 
        {
            AppUser = new();
            Transactions = new();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
