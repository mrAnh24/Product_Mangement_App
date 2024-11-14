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

namespace DatabaseApp.View.UserControls
{
    /// <summary>
    /// Interaction logic for GuestFooter.xaml
    /// </summary>
    public partial class GuestFooter : UserControl
    {
        public GuestFooter()
        {
            InitializeComponent();
            string accountName = Login.passText;
            if (accountName != "Guest account")
            {
                Visibility= Visibility.Collapsed;
            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            new Register().Show();
            Application.Current.Windows[0].Close();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            new Login().Show();
            Application.Current.Windows[0].Close();
        }
    }
}
