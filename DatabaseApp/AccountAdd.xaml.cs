using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DatabaseApp
{
    /// <summary>
    /// Interaction logic for AccountAdd.xaml
    /// </summary>
    public partial class AccountAdd : Window
    {
        public AccountAdd(Window parentWindow)
        {
            Owner = parentWindow;
            InitializeComponent();
        }

        //Add new Account
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection("Server=.;Database=dbdemo;Trusted_Connection=SSPI;MultipleActiveResultSets=true;TrustServerCertificate=true");
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();

            tbPassword.Text = pbPassword.Password;

            if (tbEmail.Text == "" || tbUsername.Text == "" || pbPassword.Password == "" || tbRole.Text == "" || tbPhoneNumber.Text == "" || tbGender.Text == "")
            {
                System.Windows.MessageBox.Show("All field have to be filled", "Error", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Error);
            }
            else if (tbUsername.Text == "admin")
            {
                System.Windows.MessageBox.Show("only 1 Username ''admin'' allowed ", "Error", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Error);
            }
            else
            {
                con.Open();
                String query = "INSERT INTO Account VALUES ('" + tbEmail.Text + "','" + tbUsername.Text + "', '" + pbPassword.Password + "', '" + tbRole.Text + "', '" + tbPhoneNumber.Text + "', '" + tbGender.Text + "')";
                cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                con.Close();

                System.Windows.MessageBox.Show("new account added", "Registration Success", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Information);
                this.Close();
            }
        }

        //Clear all textbox
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            tbEmail.Clear();
            tbUsername.Clear();
            pbPassword.Clear();
            tbPassword.Clear();
            tbRole.Clear();
            tbPhoneNumber.Clear();
            tbGender.Clear();
        }

        //Exit this form
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //Action after exit form
        private void Window_Closed(object sender, EventArgs e)
        {
            AccountManagement.index = null;
        }

        //Show/Hide password
        private void cbPassword_Click(object sender, RoutedEventArgs e)
        {
            if (cbPassword.IsChecked == true)
            {
                if (pbPassword.Password == "")
                {
                    System.Windows.MessageBox.Show("password field is blank", "Warning", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Error);
                    cbPassword.IsChecked = false;
                }
                else
                {
                    cbPassword.Content = "Password (show)";
                    tbPassword.Text = pbPassword.Password;
                    pbPassword.Visibility = Visibility.Hidden;
                }
            }
            else
            {
                cbPassword.Content = "Password";
                pbPassword.Password = tbPassword.Text;
                pbPassword.Visibility = Visibility.Visible;
            }
        }
    }
}
