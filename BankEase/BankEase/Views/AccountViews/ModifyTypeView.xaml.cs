using BankEase.ViewModels.Account;
using System.Windows.Controls;

namespace BankEase.Views.AccountViews
{
    /// <summary>
    /// Interaction logic for ModifyTypeView.xaml
    /// </summary>
    public partial class ModifyTypeView : UserControl
    {
        public ModifyTypeView(ModifyTypeViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
