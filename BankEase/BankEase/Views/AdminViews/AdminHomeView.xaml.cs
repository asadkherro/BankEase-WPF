using BankEase.ViewModels.Admin;
using System.Windows;
using System.Windows.Controls;

namespace BankEase.Views.AdminViews
{
    /// <summary>
    /// Interaction logic for AdminHomeView.xaml
    /// </summary>
    public partial class AdminHomeView : UserControl
    {
        public AdminHomeView(AdminHomeViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
