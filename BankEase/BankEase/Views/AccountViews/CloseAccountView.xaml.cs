﻿using BankEase.ViewModels.Account;
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

namespace BankEase.Views.AccountViews
{
    /// <summary>
    /// Interaction logic for CloseAccountView.xaml
    /// </summary>
    public partial class CloseAccountView : UserControl
    {
        public CloseAccountView(CloseAccountViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is CloseAccountViewModel viewModel && sender is PasswordBox pbox)
            {
                viewModel.Password = pbox.Password;
            }
        }
    }
}
