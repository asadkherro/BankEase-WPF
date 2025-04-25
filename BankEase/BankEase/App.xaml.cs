using BankEase.Helper;
using BankEase.Models.Account;
using BankEase.Models.Transactions;
using BankEase.Models.User;
using BankEase.Repository;
using BankEase.Repository.Interfaces;
using BankEase.Services;
using BankEase.Services.Interfaces;
using BankEase.ViewModels.Account;
using BankEase.ViewModels.Admin;
using BankEase.ViewModels.Auth;
using BankEase.ViewModels.Main;
using BankEase.ViewModels.Transactions;
using BankEase.Views;
using BankEase.Views.AccountViews;
using BankEase.Views.AdminViews;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace BankEase
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var services = ConfigureServices();
            SeedAdmin(services.GetRequiredService<IUserRepository>(), services.GetRequiredService<IAccountRepository>());
            var window = services.GetRequiredService<LoginView>();
            window.Show();
        }
        private void SeedAdmin(IUserRepository userRepo, IAccountRepository accountRepo) 
        {
            var adminModel = new UserModel() 
            {
                Username = "admin",
                Password = "admin",
                Role = Models.UserRole.Admin,
                AccountNumber = Common.GenerateAccountNumber(),
                Name = "BankEase Admin"
            };
            var result = userRepo.Register(adminModel);
            if (result.Success) 
            {
                accountRepo.CreateAccount(adminModel, Models.AccountType.Current);
            }
        }
        private ServiceProvider ConfigureServices() 
        {
            return new ServiceCollection()
                // Services        
                .AddSingleton<IXmlService<UserModel>, XmlService<UserModel>>(param => new XmlService<UserModel>($"C:\\XML"))
                .AddSingleton<IXmlService<AccountModel>, XmlService<AccountModel>>(param => new XmlService<AccountModel>($"C:\\XML"))
                .AddSingleton<IXmlService<TransactionModel>, XmlService<TransactionModel>>(param => new XmlService<TransactionModel>($"C:\\XML"))
                .AddSingleton<IXmlService<AccountRequestModel>, XmlService<AccountRequestModel>>(param => new XmlService<AccountRequestModel>($"C:\\XML"))
                .AddSingleton<IWindowNavigation, WindowNavigation>()
                        
                //Repositories
                .AddTransient<IUserRepository, UserRepository>()
                .AddTransient<IAccountRepository, AccountRepository>()
                .AddTransient<ITransactionRepository, TransactionRepository>()
                .AddTransient<IRequestRepository, RequestRepository>()

                // ViewModel
                .AddTransient<RegisterViewModel>()
                .AddTransient<LoginViewModel>()
                
                .AddTransient<MainViewModel>()
                .AddTransient<HomeViewModel>()
                .AddTransient<WithdrawViewModel>()
                .AddTransient<DepositViewModel>()
                .AddTransient<AccountViewModel>()
                .AddTransient<TransactionViewModel>()
                .AddTransient<HistoryViewModel>()
                .AddTransient<FundsTransferViewModel>()
                .AddTransient<ModifyTypeViewModel>()
                .AddTransient<ServicesViewModel>()
                .AddTransient<ChangeUsernameViewModel>()
                .AddTransient<ChangePasswordViewModel>()
                .AddTransient<CloseAccountViewModel>()
                .AddTransient<AdminHomeViewModel>()
                .AddTransient<AdminAccountViewModel>()
                .AddTransient<AdminTransactionViewModel>()

                //Views
                .AddTransient<Register>()
                .AddTransient<LoginView>()
                .AddTransient<MainWindow>()
                .AddTransient<HomeView>()
                .AddTransient<WithdrawView>()
                .AddTransient<DepositView>()
                .AddTransient<AccountView>()
                .AddTransient<TransactionView>()
                .AddTransient<HistoryView>()
                .AddTransient<FundsTransferView>()
                .AddTransient<ModifyTypeView>()
                .AddTransient<ServicesView>()
                .AddTransient<ChangeUsernameView>()
                .AddTransient<ChangePasswordView>()
                .AddTransient<CloseAccountView>()
                .AddTransient<AdminHomeView>()
                .AddTransient<AdminTransactionView>()
                .AddTransient<AdminAccountView>()

                .AddSingleton<ApplicationCache>()

                .BuildServiceProvider();
        }
    }

}
