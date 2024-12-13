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
using System.Windows.Shapes;

namespace DatabaseApp
{
    /// <summary>
    /// Interaction logic for AccountActivity.xaml
    /// </summary>
    public partial class AccountActivity : Window
    {
        public AccountActivity(Window parentWindow)
        {
            InitializeComponent();
            Owner = parentWindow;
            tbUsername.Text = Login.passText;
            tbRole.Text = Login.GetRole;
            tbAction.Text = Account.index;
            tbCategory.Text = Account.type;
            tbTimeStamp.Text = Account.time;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Account.index = null;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
