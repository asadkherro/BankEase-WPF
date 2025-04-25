using BankEase.ViewModels.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BankEase.Views.AdminViews
{
    /// <summary>
    /// Interaction logic for AdminTransactionView.xaml
    /// </summary>
    public partial class AdminTransactionView : UserControl
    {
        public AdminTransactionView(AdminTransactionViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
